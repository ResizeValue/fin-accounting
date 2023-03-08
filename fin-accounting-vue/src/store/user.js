/* eslint-disable no-unused-vars */
class User {
  constructor({ id, email, name }) {
    this.id = id;
    this.email = email;
    this.name = name;
  }
}

import { loginUserEndpoint, registerNewUserEndpoint } from "../Configs/finApi";
import { useCookies } from "vue3-cookies";

const { cookies } = useCookies();
export default {
  state: {
    user: null,
    token: null,
  },
  mutations: {
    setUser(state, payload) {
      state.user = payload;
    },
    setAccessToken(state, payload) {
      state.token = payload;
    },
  },
  actions: {
    logout({ commit }) {
      commit("setUser", null);
      commit("setAccessToken", null);
      cookies.remove("auth");
    },
    async registerUser({ commit }, { user }) {
      const result = await registerNewUserEndpoint(user);

      if (result.status === 200) {
        commit("loginUser", user);
      }

      return result;
    },
    async loginUser({ commit }, { user }) {
      const result = await loginUserEndpoint(user);

      if (result.status == 200) {
        const data = result.data;

        commit("setUser", new User(data));
        commit("setAccessToken", data.accessToken);
        cookies.set("auth", JSON.stringify(data));
      }

      return result;
    },
    async silentLogin({ commit }) {
      console.log("auth", cookies.get("auth"));
      try {
        const auth = cookies.get("auth");

        if (auth.accessToken) {
          commit("setUser", new User(auth));
          commit("setAccessToken", auth.accessToken);
        } else {
          cookies.delete("auth");
        }
      } catch (ex) {
        // eslint-disable-next-line no-empty
      }
    },
    getProfile({ commit }) {
      var token = cookies.get("auth");

      console.log(token);
    },
  },
  getters: {
    user(state) {
      return state.user;
    },
    isLogged(state) {
      return state.user !== null;
    },
  },
};
