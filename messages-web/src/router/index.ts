import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    redirect: () => ({ name: 'home' }),
  },
  {
    path: '/',
    name: 'home',
    component: () => import(/* webpackChunkName: "home" */ '../vue/views/home-view.vue'),
  },
  {
    path: '/labs',
    name: 'labs',
    component: () => import(/* webpackChunkName: "about" */ '../vue/views/labs-view.vue'),
  },
  {
    path: '/catalog/:id?',
    name: 'sections',
    component: () => import(/* webpackChunkName: "sections" */ '../vue/views/catalog-view.vue'),
  },
  {
    path: '/product/:id',
    name: 'product',
    component: () => import(/*  webpackChunkName: "product" */ '../vue/views/product-view.vue'),
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
