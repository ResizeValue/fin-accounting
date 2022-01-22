/* eslint-disable no-unused-vars */
class User {
    constructor(id) {
        this.id = id;
    }
}

import axios from 'axios'

export default {
    state: {
        user: null
    },
    mutations: {
        setUser(state, payload) {
            state.user = payload;
        }
    },
    actions: {
        registerUser({ commit }, { email, password }) {

        },
        async logginUser({ commit }, { email, password }) {
            var user = {
                Email: "asd@gmail.com",
                Password: "qwerty1234!",
            };

            var res = await axios.post("https://localhost:7101/Account/Login", user);

            return res;
        },
        getProfile({ commit }){
            var token = this.$cookie.get("auth");

            console.log(token);
        }
        
    },
    getters: {
        user(state) {
            return state.user;
        }
    }
}