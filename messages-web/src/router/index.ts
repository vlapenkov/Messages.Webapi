import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';
import HomeView from '../vue/views/home-view.vue';

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
    component: () => import(/* webpackChunkName: "about" */ '../vue/views/about-view.vue'),
  },
  {
    path: '/sections',
    name: 'sections',
    component: () => import(/* webpackChunkName: "sections" */ '../vue/views/sections-view.vue'),
    children: [
      {
        path: 'products/:sectionId',
        name: 'section-products',
        component: () =>
          import(/* webpackChunkName: "products" */ '../vue/views/products-view.vue'),
      },
      {
        path: '/',
        redirect: {
          name: 'section-products',
        },
      },
    ],
  },
  {
    path: '/orders',
    name: 'orders',
    component: () => import(/* webpackChunkName: "orders" */ '../vue/views/orders-view.vue'),
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
