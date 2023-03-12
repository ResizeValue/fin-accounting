<template>
  <div class="d-flex justify-center align-center flex-column mt-10 mb-10">
    <h1 class="mb-10">New ownership cost product</h1>
    <div class="form-container">
      <form enctype="multipart/form-data">
        <v-text-field v-model="name" :error-messages="nameErrors" :counter="10" label="Name" required
          @input="$v.name.$touch()" @blur="$v.name.$touch()"></v-text-field>
        <v-text-field label="Cost" v-model="cost" value="10" required prefix="$"></v-text-field>
        <v-select :items="periods" item-text="text" item-value="id" @change="handlePeriodicityChange" label="Periodicity"
          solo></v-select>
        <v-textarea class="mt-10" outlined name="input-7-4" label="Description" v-model="description"></v-textarea>
        <div class="d-flex flex-row justify-center mt-15">
          <v-btn class="mr-4" @click="submit"> submit </v-btn>
          <v-btn @click="clear"> clear </v-btn>
        </div>
      </form>
    </div>
  </div>
</template>
<script>
import { useVuelidate } from '@vuelidate/core'
import { required, maxLength } from '@vuelidate/validators'
import { addOwnershipCostEndpoint } from "../../../../Configs/finApi";

export default {
  props: ["id"],

  validations: {
    name: { required, maxLength: maxLength(60) },
  },

  setup() {
    return { v$: useVuelidate() }
  },

  data: () => ({
    name: "",
    description: "",
    periodicity: 0,
    periods: [
      { text: "Day", id: 0 },
      { text: "Month", id: 1 },
      { text: "Three months", id: 2 },
      { text: "Half-year", id: 3 },
      { text: "Year", id: 4 },
    ],
    cost: 0,
  }),

  computed: {
    nameErrors() {
      const errors = [];
      if (!this.$v.name.$dirty) return errors;
      !this.$v.name.maxLength &&
        errors.push("Name must be at most 10 characters long");
      !this.$v.name.required && errors.push("Name is required.");
      return errors;
    },
  },

  methods: {
    handlePeriodicityChange(id) {
      this.periodicity = id;
    },

    async submit() {
      this.$v.$touch();

      const formData = new FormData();

      formData.append("name", this.name);
      formData.append("periodicity", this.periodicity);
      formData.append("description", this.description);
      formData.append("cost", this.cost);
      formData.append("resourceId", this.id);

      console.log(this.name);
      console.log(this.periodicity);
      console.log(this.description);
      console.log(this.cost);
      console.log(this.id);

      const response = await addOwnershipCostEndpoint(formData);

      if (response.status === 200) {
        this.$router.push("/resources/" + this.id);
      }
    },

    clear() {
      this.$v.$reset();
      this.name = "";
      this.description = "";
      this.cost = 0;
    },
  },
};
</script>
<style lang="">
</style>