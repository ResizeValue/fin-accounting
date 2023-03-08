import { loadFonts } from './webfontloader'
import vuetify from './vuetify'
import router from '../router'
import store from '../store'
import VueCookies from 'vue3-cookies'

console.log(VueCookies);

export function registerPlugins (app) {
  loadFonts()
  app
    .use(store)
    .use(vuetify)
    .use(router)
}
