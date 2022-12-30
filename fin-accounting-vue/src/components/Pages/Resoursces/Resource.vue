<template>
  <div v-if="isLoading">
    <v-progress-circular indeterminate color="primary"></v-progress-circular>
  </div>
  <div v-else class="pa-10 pt-5">
    <div class="d-flex">
      <v-btn class="ml-auto" :to="toParent">To parent</v-btn>
    </div>

    <v-row class="mb-10">
      <v-col>
        <img class="img-profile" :src="resource.image" />
      </v-col>
      <v-col>
        <h1 class="text-center mb-4">{{ resource.name }}</h1>
        <p>
          <span>Cost: </span><strong>{{ resource.cost }}</strong>
        </p>
        <p class="mt-10">{{ resource.description }}</p>
      </v-col>
    </v-row>

    <div>
      <v-btn
        color="warning"
        class="manage-btn"
        :to="`/edit-resource/${resource.id}`"
        >Edit</v-btn
      >
      <v-btn color="error" @click="onDelete" class="manage-btn">Delete resource</v-btn>
    </div>

    <div class="mt-10 mb-15">
      <v-btn :to="`/new-ownership-cost/${resource.id}`"
        >Add ownership cost</v-btn
      >

      <v-data-table
        :headers="headers"
        :items="resource.ownershipCost"
        :items-per-page="5"
        class="elevation-1"
      >
        <template v-slot:item="{ item }">
          <tr :style="getColor(item.cost)">
            <td>
              {{ item.name > 0 ? `+${item.name}` : item.name }}
            </td>
            <td>
              {{ item.cost > 0 ? `+${item.cost}` : item.cost }}
            </td>
            <td>
              {{
                item.periodicity > 0 ? `+${item.periodicity}` : item.periodicity
              }}
            </td>
          </tr>
        </template>
      </v-data-table>
    </div>

    <div class="d-flex flex-row align-center resource-header">
      <v-row class="pa-4">
        <v-col class="border-right" cols="12" lg="2" md="2" xs="2">
          Image
        </v-col>
        <v-col class="border-right" cols="12" lg="8" md="6" xs="6">
          Name
        </v-col>
        <v-col class="border-right" cols="12" lg="2" md="2" xs="2">
          Cost
        </v-col>
      </v-row>
      <v-btn
        class="mx-2"
        fab
        dark
        color="indigo"
        :to="`/new-resource/${resource.id}`"
      >
        <v-icon dark> mdi-plus </v-icon>
      </v-btn>
    </div>

    <v-expansion-panels>
      <v-expansion-panel v-for="(item, i) in resource.resources" :key="i">
        <v-expansion-panel-header>
          <v-row no-gutters>
            <v-col cols="12" lg="2" md="2">
              <img :src="item.image" />
            </v-col>
            <v-col cols="12" lg="8" md="6">
              {{ item.name }}
            </v-col>
            <v-col cols="12" lg="2" md="2">
              {{ item.cost }}
            </v-col>
          </v-row>
        </v-expansion-panel-header>
        <v-expansion-panel-content>
          <div class="d-flex flex-row mb-5">
            <v-btn class="ml-auto" :to="`resource/${item.id}`"
              >To resource</v-btn
            >
          </div>
          Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
          eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad
          minim veniam, quis nostrud exercitation ullamco laboris nisi ut
          aliquip ex ea commodo consequat.
        </v-expansion-panel-content>
      </v-expansion-panel>
    </v-expansion-panels>
  </div>
</template>

<script>
import {
  deleteResourceEndpoint,
  getUserResourceById,
} from "../../../Configs/finApi";

export default {
  props: ["id"],
  data() {
    return {
      resource: null,

      headers: [
        { text: "Name", sortable: true, value: "name" },
        { text: "Cost", sortable: true, value: "cost" },
        { text: "Periodicity", sortable: true, value: "periodicity" },
      ],
    };
  },
  computed: {
    isLoading() {
      return this.$store.getters.isLoading;
    },
    toParent() {
      const id = this.resource.parentId;
      if (!id) {
        return `/resources`;
      } else {
        return `resource/${id}`;
      }
    },
  },
  methods: {
    getColor(value) {
      if (value > 0) {
        return "background-color: rgba(115, 255, 115, 0.3)";
      } else {
        return "color: red";
      }
    },

    async onDelete() {
      try {
        await deleteResourceEndpoint(this.resource.id);
        this.$router.push("/resources");
      } catch (error) {
        console.log(error);
      }
    },

    async getResource() {
      const response = await getUserResourceById(this.id);

      console.log(response);

      if (response.status === 200) {
        this.resource = response.data;
        console.log(this.resource);
        console.log(this.resource);
        this.$store.dispatch("setLoading", false);
      }
    },
  },
  created() {
    this.$store.dispatch("setLoading", true);
    this.getResource();
  },
};
</script>

<style scoped>
.img-profile {
  max-height: 350px;
}

.manage-btn {
  width: 160px;
}
</style>