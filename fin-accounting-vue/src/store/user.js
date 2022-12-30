/* eslint-disable no-unused-vars */
class User {
    constructor({ id, email, name }) {
        this.id = id;
        this.email = email;
        this.name = name;
    }
}

import { loginUserEndpoint, registerNewUserEndpoint } from '../Configs/finApi';
import { isJwtExpired } from 'jwt-check-expiration';
import Vue from 'vue'

export default {
    state: {
        user: null,
        token: null
    },
    mutations: {
        setUser(state, payload) {
            state.user = payload;

        },
        setAccessToken(state, payload) {
            state.token = payload;
        }
    },
    actions: {
        logout ({commit}){
            commit('setUser',null);
            commit('setAccessToken', null);
            Vue.prototype.$cookie.delete("auth");
        },
        async registerUser({ commit }, { user }) {
            const result = await registerNewUserEndpoint(user);

            if(result.status === 200){
                commit("loginUser", user);
            }

            return result;
        },
        async loginUser({ commit }, { user }) {
            const result = await loginUserEndpoint(user);

            if (result.status == 200) {
                const data = result.data;

                commit("setUser", new User(data))
                commit("setAccessToken", data.accessToken)
                Vue.prototype.$cookie.set("auth", JSON.stringify(data));
            }

            return result;
        },
        async silentLogin({ commit }) {
            try{
                const auth = JSON.parse(Vue.prototype.$cookie.get("auth"));
                console.log(auth);
                if (!isJwtExpired(auth.accessToken)) {
                    commit("setUser", new User(auth));
                    commit("setAccessToken", auth.accessToken);
                }
                else{
                    Vue.prototype.$cookie.delete("auth");
                }
            }
            // eslint-disable-next-line no-empty
            catch(ex){}
        },
        getProfile({ commit }) {
            var token = this.$cookie.get("auth");

            console.log(token);
        }

    },
    getters: {
        user(state) {
            return state.user;
        },
        isLogged(state) {
            return state.user !== null;
        }
    }
}