import Vue from 'vue'
import App from './App.vue'
import store from './store'
import router from './router'
import vuetify from './plugins/vuetify'
import VueCookie from 'vue-cookie'
import './index.css'

Vue.config.productionTip = false
Vue.use(VueCookie)

new Vue({
  vuetify,
  router,
  store,
  render: h => h(App),
  created() {
    //var token = JSON.parse(this.$cookie.get('auth'));

    //console.log(token);
  }
}).$mount('#app')
