import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';
import HomeView from '../vue/views/HomeView.vue';

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    redirect: () => ({ name: 'home' }),
  },
  {
    path: '/home',
    name: 'home',
    component: HomeView,
  },
  {
    path: '/about',
    name: 'about',
    component: () => import(/* webpackChunkName: "about" */ '../vue/views/AboutView.vue'),
  },
  {
    path: '/sections',
    name: 'sections',
    component: () => import(/* webpackChunkName: "sections" */ '../vue/views/SectionsView.vue'),
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
