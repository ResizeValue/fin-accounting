export default {
    state: {
        isLoading: false,
    },
    mutations: {
        setLoading(state, payload) {
            state.isLoading = payload;
        },
    },
    actions: {
        setLoading({commit}, status) {
            commit("setLoading", status);
        },
    },
    getters: {
        isLoading(state) {
            return state.isLoading;
        }
    }
}