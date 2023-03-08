<template>
  <div class="pa-10 pt-5">
    <h1 class="text-center mb-4">My resources</h1>
    <div class="d-flex flex-row align-center resource-header">
      <v-row class="pa-4">
        <v-col class="border-right" cols="12" lg="2" md="2" xs="2">
          Image
        </v-col>
        <v-col class="border-right" cols="12" lg="8" md="6" xs="6"> Name </v-col>
        <v-col class="border-right" cols="12" lg="2" md="2" xs="2"> Cost </v-col>
      </v-row>
      <v-btn class="mx-2" fab dark color="indigo" link :to="'/new-resource-s'">
        <v-icon dark> mdi-plus </v-icon>
      </v-btn>
    </div>

    <v-expansion-panels>
      <v-expansion-panel v-for="(item, i) in resources" :key="i">
        <v-expansion-panel-title>
          <v-row class="resource-row" no-gutters>
            <v-col cols="12" lg="2" md="2">
              <img class="resource-image" :src="item.image" />
            </v-col>
            <v-col cols="12" lg="8" md="6">
              {{ item.name }}
            </v-col>
            <v-col cols="12" lg="2" md="2">
              {{ item.cost }}
            </v-col>
          </v-row>
        </v-expansion-panel-title>
        <v-expansion-panel-text>
          <div class="d-flex flex-row mb-5">
            <v-btn class="ml-auto" :to="`resources/${item.id}`">To resource</v-btn>
          </div>
          Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
          eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad
          minim veniam, quis nostrud exercitation ullamco laboris nisi ut
          aliquip ex ea commodo consequat.
        </v-expansion-panel-text>
      </v-expansion-panel>
    </v-expansion-panels>
  </div>
</template>

<script>
import { getUserResourcesRoot } from "../../../configs/finApi";
export default {
  data() {
    return {
      resources: [],
    };
  },
  methods: {
    async getResources() {
      //console.log(this.$store.getters.user);
      const id = this.$store.getters.user?.id;

      if (!id) return;

      const response = await getUserResourcesRoot(id);

      console.log('response', response);

      if (response.status === 200) {
        console.log(response.data);
        this.resources = response.data;
      }
    },
  },
  created() {
    this.getResources();
  },
};
</script>

<style>
.resource-image {
  max-width: 100%;
  height: 100px;
}

.resource-row {
  height: 100px;
}

.resource-header {
  border: 1px solid gray;
}

.border-left {
  border-left: 1px solid gray;
}

.border-right {
  border-right: 1px solid gray;
}
</style>