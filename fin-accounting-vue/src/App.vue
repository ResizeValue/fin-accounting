<template>
  <page-layout></page-layout>
</template>

<script>
import { useCookies } from "vue3-cookies";
import PageLayout from "./components/layouts/PageLayout.vue";

const { cookies } = useCookies();

export default {
  name: "App",

  data: () => ({
    drawer: false,
    currentUser: null,
    isLogin: false,
    accessTokenExpired: false,
  }),
  components: {
    PageLayout,
  },

  methods: {
    username() {
      return this.currentUser;
    },

    login() {
      this.$store.dispatch("getProfile", {});
    },

    logout() {
      console.log("object");
    },

    sendRequest() {
      console.log("Try to send request");

      this.$store
        .dispatch("logginUser", {})
        .then((res) => {
          console.log("Success");
          cookies.set("auth", JSON.stringify(res.data));
        })
        .catch((err) => {
          console.log(err.message);
        });
    },
  },
  created() {
    this.$store.dispatch("silentLogin");
  },
};
</script>

