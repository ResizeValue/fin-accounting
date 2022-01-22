<template>
  <v-app>
    <div>
      <v-toolbar
        dark
        prominent
        src="https://cdn.vuetifyjs.com/images/backgrounds/vbanner.jpg"
      >
        <v-app-bar-nav-icon @click="drawer = !drawer"></v-app-bar-nav-icon>
        <v-toolbar-title>Vuetify</v-toolbar-title>
        <v-spacer></v-spacer>
        <v-btn icon>
          <v-icon>mdi-export</v-icon>
        </v-btn>
      </v-toolbar>
    </div>
    <v-navigation-drawer v-model="drawer" temporary app> </v-navigation-drawer>
    <v-main>
      <router-view>
        
      </router-view>
      <resource-page></resource-page>
    </v-main>
  </v-app>
</template>

<script>
import ResourcePage from "./components/Resource.vue";

export default {
  name: "App",

  data: () => ({
    drawer: false,
    currentUser: null,
    isLogin: false,
    accessTokenExpired: false,
  }),
  components: {
    'resource-page': ResourcePage
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
          //console.log(res.data);
          this.$cookie.set("auth", JSON.stringify(res.data));
        })
        .catch((err) => {
          console.log(err.message);
        });
    },
  },
};
</script>

