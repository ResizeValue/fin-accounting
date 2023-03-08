// Composables
import { createRouter, createWebHistory } from 'vue-router'
import ResourcesRoot from '../components/pages/resoursces/ResourcesRoot.vue'
import Resoursce from '../components/pages/resoursces/Resource.vue'
import NewResource from '../components/pages/resoursces/NewResource.vue'
import EditResource from '../components/pages/resoursces/EditResource.vue'
import NewOwnershipCost from '../components/pages/resoursces/ownership/NewOwnershipCost.vue'
import Profile from '../components/pages/Profile.vue'
import Login from '../components/pages/auth/Login.vue'
import Registration from '../components/pages/auth/Registration.vue'

const routes = [
  {
    path: '/',
    component: () => import('@/layouts/default/Default.vue'),
    children: [
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
        path: '/new-resource-s',
        name: 'new-resource-p',
        component: NewResource
      },
      // {
      //   path: '/new-resource-s/:id',
      //   props: true,
      //   name: 'new-resource-p',
      //   component: NewResource
      // },
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
  },
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
})

export default router
