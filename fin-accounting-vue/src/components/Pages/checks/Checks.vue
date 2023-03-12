<template>
    <div v-if="isLoading" class="d-flex w-100 h-100 align-center justify-center">
        <v-progress-circular indeterminate color="primary"></v-progress-circular>
    </div>
    <div v-else class="pa-10 pt-5">
        <div class="d-flex flex-row align-center resource-header">
            <v-row class="pa-4">
                <v-col class="border-right" cols="12" lg="2" md="2" xs="2">
                    Name
                </v-col>
                <v-col class="border-right" cols="12" lg="8" md="6" xs="6">
                    Date
                </v-col>
                <v-col class="border-right" cols="12" lg="2" md="2" xs="2">
                    Cost
                </v-col>
            </v-row>
            <v-btn class="mx-2" fab dark color="indigo" :to="`/new-check`">
                <v-icon dark> mdi-plus </v-icon>
            </v-btn>
        </div>

        <v-expansion-panels>
            <v-expansion-panel v-for="(check, i) in checks" :key="i">
                <v-expansion-panel-title>
                    <v-row no-gutters>
                        <v-col cols="12" lg="2" md="2">
                            {{ check.name }}
                        </v-col>
                        <v-col cols="12" lg="8" md="6">
                            {{ check.date }}
                        </v-col>
                        <v-col cols="12" lg="2" md="2">
                            0
                        </v-col>
                    </v-row>
                </v-expansion-panel-title>
                <v-expansion-panel-text>
                    <div class="d-flex flex-row mb-5">
                        <v-btn class="ml-auto" color="warning" :to="`edit-check/${check.id}`">Edit</v-btn>
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
  
<script lang="js">
import {
    deleteResourceEndpoint,
    getUserChecks,
} from "../../../Configs/finApi";

export default {
    data() {
        return {
            checks: null,

            headers: [
                { text: "Name", sortable: true, value: "name" },
                { text: "Date", sortable: true, value: "date" },
                { text: "Price", sortable: true, value: "price" },
            ],
        };
    },
    computed: {
        isLoading() {
            return this.$store.getters.isLoading;
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

        async getPaymentChecks() {
            const id = this.$store.getters.user?.id;
            const response = await getUserChecks(id);

            if (response.status === 200) {
                this.checks = response.data;
                console.log(this.checks);
                this.$store.dispatch("setLoading", false);
            }
        },
    },
    created() {
        this.$store.dispatch("setLoading", true);
        this.getPaymentChecks();
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