import Vue from 'vue'
import Router from 'vue-router'
import ResourcesRoot from '../components/Pages/Resoursces/ResourcesRoot.vue'
import Resoursce from '../components/Pages/Resoursces/Resource.vue'
import NewResource from '../components/Pages/Resoursces/NewResource.vue'
import EditResource from '../components/Pages/Resoursces/EditResource.vue'
import NewOwnershipCost from '../components/Pages/Resoursces/Ownership/NewOwnershipCost.vue'
import Profile from '../components/Pages/Profile.vue'
import Login from '../components/Pages/Auth/Login.vue'
import Registration from '../components/Pages/Auth/Registration.vue'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'main',
      component: ResourcesRoot
    },
    {
      path: '/resources',
      name: 'resources',
      component: ResourcesRoot
    },
    {
      path: '/resources/:id',
      props: true,
      name: 'details-resource',
      component: Resoursce
    },
    {
      path: '/new-resource',
      name: 'new-resource',
      component: NewResource
    },
    {
      path: '/new-ownership-cost/:id',
      name: 'new-ownership-cost',
      props: true,
      component: NewOwnershipCost
    },
    {
      path: '/edit-resource/:id',
      name: 'edit-resource',
      props: true,
      component: EditResource
    },
    {
      path: '/new-resource/:id',
      props: true,
      name: 'new-resource',
      component: NewResource
    },
    {
      path: '/profile',
      name: 'profile',
      component: Profile
    },
    {
      path: '/login',
      name: 'login',
      component: Login
    },
    {
      path: '/registration',
      name: 'registration',
      component: Registration
    },
  ],
  mode: 'history'
})
