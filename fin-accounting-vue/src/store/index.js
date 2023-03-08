import { createStore } from 'vuex'
import user from './user'
import status from './status'

export default new createStore( {
    modules: {
        user, status
    },
});