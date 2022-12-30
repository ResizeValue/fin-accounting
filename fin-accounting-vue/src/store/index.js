import Vue from 'vue'
import Vuex from 'vuex'
import user from './user'
import status from './status'

Vue.use(Vuex);

export default new Vuex.Store( {
    modules: {
        user, status
    },
});