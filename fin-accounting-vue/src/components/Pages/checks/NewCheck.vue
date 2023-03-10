<template>
  <v-container>
    <v-card class="mb-5">
      <v-card-title>Create Payment Check</v-card-title>
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
                        :rules="nameRules"
                        required
                      ></v-text-field>
                    </v-col>
                    <v-col class="ml-1">
                      <v-text-field
                        v-model="product.cost"
                        label="Product Cost"
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
                    <v-card-text
                      ><v-chip
                        v-for="(tag, tagIndex) in product.tags"
                        :key="tagIndex"
                        color="primary"
                        >{{ tag }}</v-chip
                      ></v-card-text
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
        <v-btn @click="submit" :disabled="!valid">Submit</v-btn>
        <v-btn @click="reset">Reset</v-btn>
      </v-card-actions>
    </v-card>
  </v-container>
</template>
<script>
export default {
  data() {
    return {
      valid: false,
      name: "",
      nameRules: [(v) => !!v || "Name is required"],
      priceRules: [(v) => !!v || "Name is required"],
      products: [{ name: "", cost: 0, tags: [], tagInput: "" }],
      tagList: ["tag1", "tag2", "tag3"],
    };
  },
  methods: {
    onPriceInput(event, product) {
      const value = parseFloat(event.target.value);

      if (!isNaN(value)) {
        product.price = value;
      }
      
      event.target.value = product.price;
    },
    addProduct() {
      this.products.push({ name: "", cost: 0, tags: [] });
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
    submit() {
      if (this.$refs.form.validate()) {
        const paymentCheck = {
          name: this.name,
          products: this.products.map((product) => ({
            name: product.name,
            cost: product.cost,
            tags: product.tags,
          })),
        };
        console.log(paymentCheck);
      }
    },
    reset() {
      this.valid = false;
      this.name = "";
      this.products = [{ name: "", cost: "", tags: [] }];
      this.tagInput = "";
      this.$refs.form.reset();
    },
  },
};
</script>
