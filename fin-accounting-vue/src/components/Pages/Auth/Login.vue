<template>
  <v-container fluid fill-height>
    <v-layout align-center justify-center>
      <v-container xs12 sm8 md6 lg6>
        <v-card class="elevation-12">
          <v-toolbar dark color="secondary">
            <v-toolbar-title>Login</v-toolbar-title>
          </v-toolbar>
          <v-card-text>
            <v-form v-model="valid" class="container" ref="form" validation>
              <v-text-field prepend-icon="mdi-email" name="email" label="Email" type="email" v-model="email"
                :rules="emailRules"></v-text-field>
              <v-text-field prepend-icon="mdi-lock" name="password" label="Password" type="password" :counter="6"
                v-model="password" :rules="passwordRules"></v-text-field>
            </v-form>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="primary" @click="onSubmit" :disabled="!valid">Login</v-btn>
          </v-card-actions>
        </v-card>
      </v-container>
    </v-layout>
  </v-container>
</template>

<script>
const emailRegex = /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/

export default {
  data() {
    return {
      email: '',
      password: '',
      valid: false,
      emailRules: [
        v => !!v || 'E-mail is required',
        v => emailRegex.test(v) || 'E-mail must be valid'
      ],
      passwordRules: [
        v => !!v || 'Password is required',
        v => (v && v.length >= 6) || 'Password must be equal or more than 6 characters'
      ]
    }
  },
  methods: {
    async onSubmit() {
      if (this.$refs.form.validate()) {
        const user = {
          email: this.email,
          password: this.password
        }

        await this.$store.dispatch("loginUser", { user })
          .then(() => { this.$router.push('/resources'); })
          .catch((error) => { console.log('Error', error); });
      }
    }
  }
}
</script>
