<template>
  <v-container fluid fill-height>
    <v-layout align-center justify-center>
      <v-container xs12 sm8 md6 lg6>
        <v-card class="elevation-12">
          <v-toolbar dark color="secondary">
            <v-toolbar-title>Registration</v-toolbar-title>
          </v-toolbar>
          <v-card-text>
            <v-form v-model="valid" ref="form" lazy-validation>
              <v-text-field prepend-icon="mdi-email" name="email" label="Email" type="email" v-model="email"
                :rules="emailRules"></v-text-field>
              <v-text-field prepend-icon="mdi-badge-account" name="Full name" label="Full name" type="text"
                v-model="fullName" :rules="fullNameRules"></v-text-field>
              <v-row>
                <v-col>
                  <v-text-field prepend-icon="mdi-lock" name="password" label="Password" type="password" :counter="6"
                    v-model="password" :rules="passwordRules"></v-text-field>
                </v-col>
                <v-col>
                  <v-text-field name="confirm-password" label="Confirm Password" type="password" :counter="6"
                    v-model="confirmPassword" :rules="confirmPasswordRules"></v-text-field>
                </v-col>
              </v-row>
            </v-form>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="primary" @click="onSubmit" :disabled="!valid">Create account!</v-btn>
          </v-card-actions>
        </v-card>
      </v-container>
    </v-layout>
  </v-container>
</template>

<script>
const emailRegex = /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/;

export default {
  data() {
    return {
      email: "",
      fullName: "",
      password: "",
      confirmPassword: "",
      valid: false,
      emailRules: [
        (v) => !!v || "E-mail is required",
        (v) => emailRegex.test(v) || "E-mail must be valid",
      ],
      passwordRules: [
        (v) => !!v || "Password is required",
        (v) =>
          (v && v.length >= 6) ||
          "Password must be equal or more than 6 characters",
      ],
      confirmPasswordRules: [
        (v) => !!v || "Password is required",
        (v) => v === this.password || "Password should match",
      ],
      fullNameRules: [(v) => v.length <= 50 || "Name is too long"],
    };
  },
  methods: {
    async onSubmit() {
      if (this.$refs.form.validate()) {
        const user = {
          email: this.email,
          name: this.fullName,
          password: this.password,
        };

        try {
          await this.$store.dispatch("registerUser", { user });
          this.$router.push('/resources')
        } catch {
          console.log("");
        }
      }
    },
  },
};
</script>
