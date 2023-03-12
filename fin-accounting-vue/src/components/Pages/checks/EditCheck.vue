<template>
  <v-container>
    <v-card class="mb-5">
      <v-card-title>Edit Payment Check</v-card-title>
      <v-card-text>
        <v-form ref="form" v-model="valid">
          <v-text-field v-model="name" label="Name"></v-text-field>
          <v-card class="my-5">
            <v-card-title>Products</v-card-title>
            <v-card-text>
              <div class="d-flex flex-row justify-space-between">
                <v-btn @click="addProduct">Add Product</v-btn>
                <v-btn
                  @click="removeProduct(index)"
                  color="error"
                  variant="plain"
                  >clear</v-btn
                >
              </div>
              <v-divider
                :thickness="1"
                class="border-opacity-25 my-4"
              ></v-divider>
              <v-list>
                <v-list-item v-for="(product, index) in products" :key="index">
                  <div class="d-flex justify-end">
                    <v-btn
                      @click="removeProduct(index)"
                      color="error"
                      variant="plain"
                      >Remove</v-btn
                    >
                  </div>
                  <v-row>
                    <v-col class="mr-1">
                      <v-text-field
                        v-model="product.name"
                        label="Product Name"
                        required
                      ></v-text-field>
                    </v-col>
                    <v-col class="ml-1">
                      <v-text-field
                        v-model="product.price"
                        label="Product Price"
                        @input="onPriceInput($event, product)"
                        required
                      ></v-text-field>
                    </v-col>
                  </v-row>

                  <v-card class="border">
                    <v-card-title class="px-0 py-0">
                      <v-autocomplete
                        ref="tagInputRef"
                        v-model="product.tagInput"
                        :items="tagList"
                        label="Tags"
                        @keydown.enter.prevent="
                          addTag(product);
                          $event.target.blur();
                        "
                        @input="
                          product.tagInput = $event.target.value.toLowerCase()
                        "
                      ></v-autocomplete
                    ></v-card-title>
                    <v-card-text>
                      <v-chip
                        v-for="(tag, tagIndex) in product.tags"
                        :key="tagIndex"
                        color="primary"
                      >
                        <div>
                          {{ tag }}
                          <button>
                            <v-icon
                              class="text-red"
                              @click="removeTag(product, tag)"
                              >mdi-close</v-icon
                            >
                          </button>
                        </div>
                      </v-chip></v-card-text
                    >
                  </v-card>

                  <v-divider
                    :thickness="1"
                    class="border-opacity-25 my-4"
                  ></v-divider>
                </v-list-item>
              </v-list>
            </v-card-text>
          </v-card>
        </v-form>
      </v-card-text>
      <v-card-actions>
        <v-btn @click="submit" :disabled="!valid">Update</v-btn>
        <v-btn @click="reset">Reset</v-btn>
      </v-card-actions>
    </v-card>
  </v-container>
</template>
<script>
import {
  getChecksById,
  getUserTags,
  updatePaymentCheck,
} from "../../../Configs/finApi";

export default {
  data() {
    return {
      valid: false,
      name: "",
      date: null,
      nameRules: [(v) => !!v || "Name is required"],
      products: [],
      tagList: [],
    };
  },
  created() {
    this.loadPaymentCheck();
    this.loadTagList();
  },
  methods: {
    async loadPaymentCheck() {
      const checkId = this.$route.params.id;

      try {
        const paymentCheck = await getChecksById(checkId);

        console.log("paymentCheck", paymentCheck.data);

        this.name = paymentCheck.data.name;
        this.date = paymentCheck.data.date;
        this.products = paymentCheck.data.products.map((product) => ({
          name: product.name,
          price: product.price,
          tags: product.tags.map((tag) => tag.name),
          tagInput: "",
        }));
      } catch (error) {
        console.log("Failed to load payment check", error);
      }

      this.valid = true;
    },
    async loadTagList() {
      try {
        const response = await getUserTags(this.$store.getters.user.id);

        this.tagList = response.data.map((tag) => tag.name);
      } catch (error) {
        console.log("Failed to load tags");
      }
    },
    onPriceInput(event, product) {
      const value = parseFloat(event.target.value);

      if (!isNaN(value)) {
        product.price = value;
      }

      event.target.value = product.price;
    },
    addProduct() {
      this.products.push({
        name: "",
        price: 0,
        tags: [],
        tagInput: "",
      });
    },
    removeProduct(index) {
      this.products.splice(index, 1);
    },
    addTag(product) {
      const tag = product.tagInput.trim();
      if (tag && !product.tags.includes(tag)) product.tags.push(tag);
      if (tag && !this.tagList.includes(tag)) this.tagList.push(tag);

      product.tagInput = null;
    },
    removeTag(product, tag) {
      const index = product.tags.findIndex((t) => t === tag);
      product.tags.splice(index, 1);
    },
    async submit() {
      if (!this.$refs.form.validate()) return;

      const paymentCheck = {
        id: this.$route.params.id,
        name: this.name,
        products: this.products.map((product) => ({
          name: product.name,
          price: product.price,
          tags: product.tags,
        })),
        userId: this.$store.getters.user.id,
      };

      try {
        await updatePaymentCheck(paymentCheck);

        this.$router.push("/checks");
      } catch (error) {
        console.log("Failed to update payment check", error);
      }
    },
    reset() {
      this.valid = false;
      this.name = "";
      this.products = [{ name: "", price: "", tags: [], tagInput: "" }];
      this.$refs.form.reset();
    },
  },
};
</script>
