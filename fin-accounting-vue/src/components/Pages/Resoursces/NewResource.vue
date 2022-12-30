<template>
  <div class="d-flex justify-center align-center flex-column mt-10 mb-10">
    <h1 class="mb-10">Create new product</h1>
    <div class="form-container">
      <form enctype="multipart/form-data">
        <v-text-field
          v-model="name"
          :error-messages="nameErrors"
          :counter="10"
          label="Name"
          required
          @input="$v.name.$touch()"
          @blur="$v.name.$touch()"
        ></v-text-field>
        <v-text-field
          label="Cost"
          value="10.00"
          v-model="cost"
          required
          prefix="$"
        ></v-text-field>

        <v-textarea
          class="mt-10"
          outlined
          name="input-7-4"
          label="Description"
          :v-model="description"
        ></v-textarea>
        <v-file-input
          id="img_input"
          label="File input"
          @change="setImage"
          filled
          prepend-icon="mdi-camera"
        ></v-file-input>
        <div class="d-flex image-container mw-100 justify-center mb-5">
          <img id="img_load" class="mw-100"/>
        </div>
        <div class="d-flex flex-row justify-center mt-15">
          <v-btn class="mr-4" @click="submit"> submit </v-btn>
          <v-btn @click="clear"> clear </v-btn>
        </div>
      </form>
    </div>
  </div>
</template>
<script>
import { validationMixin } from "vuelidate";
import { required, maxLength } from "vuelidate/lib/validators";
// eslint-disable-next-line no-unused-vars
import { createResourceEndpoint } from "../../../Configs/finApi";

export default {
  mixins: [validationMixin],
  props: ['id'],

  validations: {
    name: { required, maxLength: maxLength(60) },
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
  },

  methods: {
    async submit() {
      this.$v.$touch();
      console.log(this.$store.getters.user.id);

      if (this.description.length < 1) {
        this.description = "No description.";
      }

      if(!this.id){
        this.id = null;
      }

      const formData = new FormData();

      formData.append("userId", this.$store.getters.user.id);
      formData.append("name", this.name);
      formData.append("description", this.description);
      formData.append("image", this.image);
      formData.append("cost", this.cost);
      formData.append("resourceId", this.id);

      const response = await createResourceEndpoint(formData);

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
      document.getElementById('img_load').setAttribute("src", '')
    },
  },
};
</script>

<style>
.form-container {
  width: 50%;
}

#img_load{
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