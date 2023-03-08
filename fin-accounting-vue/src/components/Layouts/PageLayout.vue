<template>
  <v-app>
    <v-main>
      <div class="main-grid">
        <div class="d-flex">
          <v-card class="pos-fixed z-index-medium vh-100">
            <v-navigation-drawer permanent expand-on-hover>
              <v-list v-if="isLoggedIn">
                <v-list-item class="px-2">
                  <v-img src="https://randomuser.me/api/portraits/women/85.jpg"></v-img>
                </v-list-item>

                <v-list-item link :to="'/profile'">
                  <v-list-item-title class="text-h6">
                    {{ user.name }}
                  </v-list-item-title>
                  <v-list-item-subtitle>{{ user.email }}</v-list-item-subtitle>
                </v-list-item>
              </v-list>

              <v-divider></v-divider>

              <v-list v-if="isLoggedIn" nav dense>
                <v-list-item link :to="'/resources'">
                  <v-icon>mdi-folder</v-icon>
                  <v-list-item-title>Resources</v-list-item-title>
                </v-list-item>
                <v-list-item @click="logout" link :to="'/'">
                  <v-icon>mdi-logout</v-icon>
                  <v-list-item-title>Logout</v-list-item-title>
                </v-list-item>
              </v-list>

              <v-list v-if="!isLoggedIn" nav dense>
                <v-list-item link :to="'/login'">
                  <v-icon>mdi-account</v-icon>
                  <v-list-item-title>Login</v-list-item-title>
                </v-list-item>
                <v-list-item link :to="'/registration'">
                  <v-icon>mdi-account-plus</v-icon>
                  <v-list-item-title>Registration</v-list-item-title>
                </v-list-item>
              </v-list>
            </v-navigation-drawer>
          </v-card>
        </div>

        <div class="">
          <router-view></router-view>
        </div>
      </div>
    </v-main>
  </v-app>
</template>
<script>
export default {
  data() {
    return {
      drawer: false,
      navLinks: [],
      avatar: "../../images/unknownUser.jpg",
    };
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    }
  },
  components: {},
  computed: {
    isLoggedIn() {
      return this.$store.getters.isLogged;
    },
    user() {
      return this.$store.getters.user;
    },
  },
};
</script>


<style scoped></style>