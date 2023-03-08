<template>
  <div class="d-flex justify-center align-center flex-column mt-10 mb-10">
    <h1 class="mb-10">Add check</h1>
    <div class="form-container">
      <form enctype="multipart/form-data">
        <div class="d-flex flex-column justify-center mb-5">
          <div
            class="d-flex w-100 justify-space-between"
            v-for="(product, i) in products"
            :key="i"
          >
            <div class="">{{ product.name }}</div>
            <div class="">{{ product.price }}</div>
            <v-btn @click="deleteProduct(i)">Delete</v-btn>
          </div>
        </div>
        <v-text-field label="Name" v-model="name" :counter="10" required />
        <v-text-field
          label="Price"
          model-value="10.00"
          v-model="price"
          required
          prefix="UAH"
        />
        <v-btn @click="addProduct">Add</v-btn>

        <div class="d-flex flex-row justify-center mt-15">
          <v-btn class="mr-4" @click="submit"> submit </v-btn>
          <v-btn @click="clear"> clear </v-btn>
        </div>
      </form>
    </div>
  </div>
</template>
<script>
import { required, maxLength } from "@vuelidate/validators";

export default {
  props: ["id"],

  validations() {
    return {
      form: {
        name: {
          required,
        },
        cost: {
          required,
        },
        description: {},
        image: {},
        password: {
          required,
          max: maxLength(6),
        },
      },
    };
  },

  data: () => ({
    name: "",
    price: 0,
    products: [],
  }),

  methods: {
    addProduct() {
      this.products.push({
        name: this.name,
        price: this.price,
      });
      this.name = "";
      this.price = 0;
    },

    deleteProduct(index) {
      this.products.splice(index, 1);
    },

    async submit() {
      // TO DO
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
      this.name = "";
      this.cost = 0;
      this.products = [];
    },
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
