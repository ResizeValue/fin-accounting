<template>
  <div v-if="isLoading">
    <v-progress-circular indeterminate color="primary"></v-progress-circular>
  </div>
  <div v-else class="d-flex justify-center align-center flex-column mt-10 mb-10">
    <h1 class="mb-10">Edit resource</h1>
    <div class="form-container">
      <form enctype="multipart/form-data">
        <v-text-field v-model="name" :error-messages="nameErrors" :counter="10" label="Name" required
          @input="$v.name.$touch()" @blur="$v.name.$touch()"></v-text-field>
        <v-text-field label="Cost" value="10.00" v-model="cost" required prefix="$"></v-text-field>

        <v-textarea class="mt-10" outlined name="input-7-4" label="Description" :v-model="description"></v-textarea>
        <v-file-input id="img_input" label="File input" @change="setImage" filled
          prepend-icon="mdi-camera"></v-file-input>
        <div class="d-flex image-container mw-100 justify-center mb-5">
          <img id="img_load" :src="image" class="mw-100" />
        </div>
        <div class="d-flex flex-row justify-center mt-15">
          <v-btn class="mr-4" @click="submit"> submit </v-btn>
        </div>
      </form>
    </div>
  </div>
</template>
<script>
import { useVuelidate } from '@vuelidate/core'
import { required, maxLength } from '@vuelidate/validators'
// eslint-disable-next-line no-unused-vars
import {
  getUserResourceById,
  updateResourceEndpoint,
} from "../../../Configs/finApi";

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
    cost: 0,
    image: null,
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
    isLoading() {
      return this.$store.getters.isLoading;
    },
  },

  methods: {
    async getResource() {
      const response = await getUserResourceById(this.id);

      const resource = response.data;
      // eslint-disable-next-line no-unused-vars
      this.name = resource.name;
      this.resourceId = resource.resourceId;
      this.image = resource.image;
      this.cost = resource.cost;
      this.description = resource.description;
      this.$store.dispatch("setLoading", false);
    },

    async submit() {
      this.$v.$touch();

      if (!this.description) {
        this.description = "No description.";
      }

      console.log(this.description);

      if (!this.id) {
        this.id = null;
      }

      const formData = new FormData();

      formData.append("userId", this.$store.getters.user.id);
      formData.append("id", this.id);
      formData.append("name", this.name);
      formData.append("description", this.description);
      formData.append("image", this.image);
      formData.append("cost", this.cost);
      formData.append("resourceId", this.id);

      const response = await updateResourceEndpoint(formData);

      if (response.status === 200) {
        this.$router.push("/resources");
      }
    },

    setImage() {
      const img_input = document.getElementById("img_input");

      if (img_input.files && img_input.files[0]) {
        var reader = new FileReader();

        reader.onload = (e) => {
          document
            .getElementById("img_load")
            .setAttribute("src", e.target.result);

          const file = img_input.files[0];
          this.image = file;
          console.log(this.image);
        };

        reader.readAsDataURL(img_input.files[0]);
      }
    },

    clear() {
      this.$v.$reset();
      this.name = "";
      this.description = "";
      this.cost = 0;
      this.imagePath = "";
      document.getElementById("img_load").setAttribute("src", "");
    },
  },

  created() {
    this.$store.dispatch("setLoading", true);
    this.getResource();
  },
};
</script>

<style>
.form-container {
  width: 50%;
}

#img_load {
  justify-self: center;
}

.image-container {
  justify-items: center;
}

@media screen and (max-width: 900px) {
  .form-container {
    width: 80% !important;
  }
}
</style>