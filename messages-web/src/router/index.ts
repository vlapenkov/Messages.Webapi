import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';
import HomeView from '../vue/views/home-view.vue';

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    redirect: () => ({ name: 'sections' }),
  },
  {
    path: '/home',
    name: 'home',
    component: HomeView,
  },
  {
    path: '/labs',
    name: 'labs',
    component: () => import(/* webpackChunkName: "about" */ '../vue/views/labs-view.vue'),
  },
  {
    path: '/catalog',
    name: 'sections',
    component: () => import(/* webpackChunkName: "sections" */ '../vue/views/catalog-view.vue'),
  },
  {
    path: '/product/:id',
    name: 'product',
    component: () => import(/*  webpackChunkName: "product" */ '../vue/views/product-view.vue'),
  },
  {
    path: '/edit-product',
    name: 'edit-product',
    component: () => import(/*  webpackChunkName: "product" */ '../vue/views/product-edit.vue'),
  },
  {
    path: '/orders',
    name: 'orders',
    component: () => import(/* webpackChunkName: "orders" */ '../vue/views/orders-view.vue'),
  },
  {
    path: '/shopping-cart',
    name: 'shopping-cart',
    component: () => import(/* webpackChunkName: "orders" */ '../vue/views/shopping-cart-view.vue'),
  },
  {
    path: '/reports',
    name: 'reports',
    component: () => import(/* webpackChunkName: "reports" */ '../vue/views/reports-view.vue'),
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
