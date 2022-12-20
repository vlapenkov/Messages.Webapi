import { DataMode } from '@/app/core/services/harlem/tools/not-valid-data';
import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';

declare module 'vue-router' {
  interface RouteMeta {
    mode?: DataMode;
    requiresAuth: boolean;
  }
}

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    component: () =>
      import(/* webpackChunkName: "searchPanel" */ '../vue/views/search-panel-view.vue'),
    children: [
      {
        path: '',
        name: 'home',
        component: () => import(/* webpackChunkName: "home" */ '../vue/views/home-view.vue'),
        meta: { requiresAuth: false },
      },
      {
        path: '/catalog',
        name: 'catalog',
        component: () => import(/* webpackChunkName: "sections" */ '../vue/views/catalog-view.vue'),
        meta: { requiresAuth: false },
      },
      {
        path: '/products',
        name: 'products',
        component: () =>
          import(/* webpackChunkName: "products" */ '../vue/views/products-manager-view.vue'),
        meta: { requiresAuth: true },
      },
      {
        path: '/organizations',
        name: 'organizations',
        component: () =>
          import(/* webpackChunkName: "organizations" */ '../vue/views/organizations-view.vue'),
        meta: { requiresAuth: true },
      },
      {
        path: '/org-products',
        name: 'org-products',
        component: () =>
          import(
            /* webpackChunkName: "org-products" */ '../vue/views/products-org-manager-view.vue'
          ),
        meta: { requiresAuth: true },
      },
    ],
  },
  {
    path: '/labs',
    name: 'labs',
    component: () => import(/* webpackChunkName: "about" */ '../vue/views/labs-view.vue'),
    meta: { requiresAuth: true },
  },
  {
    path: '/product/:id',
    name: 'product',
    component: () => import(/*  webpackChunkName: "product" */ '../vue/views/product-view.vue'),
    meta: { requiresAuth: false },
  },
  {
    path: '/edit-product/:id?',
    name: 'edit-product',
    component: () =>
      import(/*  webpackChunkName: "edit-product" */ '../vue/views/product-edit.vue'),
    meta: {
      requiresAuth: true,
    },
    props: {
      productionType: 'product',
      dataMode: 'edit',
    },
  },
  {
    path: '/edit-product-work/:id?',
    name: 'edit-product-work',
    component: () =>
      import(/*  webpackChunkName: "edit-product" */ '../vue/views/product-edit.vue'),
    meta: {
      requiresAuth: true,
    },
    props: {
      productionType: 'work',
      dataMode: 'edit',
    },
  },
  {
    path: '/edit-product-service/:id?',
    name: 'edit-product-service',
    component: () =>
      import(/*  webpackChunkName: "edit-product" */ '../vue/views/product-edit.vue'),
    meta: {
      requiresAuth: true,
    },
    props: {
      productionType: 'service',
      dataMode: 'edit',
    },
  },
  {
    path: '/create-product',
    name: 'create-product',
    component: () =>
      import(/*  webpackChunkName: "edit-product" */ '../vue/views/product-edit.vue'),
    meta: { requiresAuth: true },
    props: {
      productionType: 'product',
      dataMode: 'create',
    },
  },
  {
    path: '/create-service',
    name: 'create-service',
    component: () =>
      import(/*  webpackChunkName: "edit-product" */ '../vue/views/product-edit.vue'),
    meta: { requiresAuth: true },
    props: {
      productionType: 'service',
      dataMode: 'create',
    },
  },
  {
    path: '/create-work',
    name: 'create-work',
    component: () =>
      import(/*  webpackChunkName: "edit-product" */ '../vue/views/product-edit.vue'),
    meta: { mode: 'create', requiresAuth: true },
    props: {
      productionType: 'work',
      dataMode: 'create',
    },
  },
  {
    path: '/categories',
    name: 'categories',
    component: () =>
      import(/*  webpackChunkName: "edit-product" */ '../vue/views/categories-view.vue'),
    meta: { requiresAuth: true },
  },
  {
    path: '/orders',
    name: 'orders',
    component: () => import(/* webpackChunkName: "orders" */ '../vue/views/orders-view.vue'),
    meta: { requiresAuth: true },
  },
  {
    path: '/incoming-orders',
    name: 'incoming-orders',
    component: () =>
      import(/* webpackChunkName: "orders" */ '../vue/views/incoming-orders-view.vue'),
  },
  {
    path: '/shopping-cart',
    name: 'shopping-cart',
    component: () =>
      import(/* webpackChunkName: "shopping-cart" */ '../vue/views/shopping-cart-view.vue'),
    meta: { requiresAuth: true },
  },
  {
    path: '/reports',
    name: 'reports',
    component: () => import(/* webpackChunkName: "reports" */ '../vue/views/reports-view.vue'),
    meta: { requiresAuth: true },
  },
  {
    path: '/organization/:id',
    name: 'organization',
    component: () =>
      import(/* webpackChunkName: "organization" */ '../vue/views/organization-view.vue'),
    meta: { requiresAuth: true },
  },
  {
    path: '/organization-add',
    name: 'organization-add',
    component: () =>
      import(/* webpackChunkName: "organization-add" */ '../vue/views/organization-add-view.vue'),
    meta: { requiresAuth: true },
  },
  {
    path: '/order/:id',
    name: 'order',
    component: () => import(/* webpackChunkName: "order" */ '../vue/views/order-view.vue'),
    meta: { requiresAuth: true },
  },
  {
    path: '/production-geo',
    name: 'production-geo',
    component: () =>
      import(/* webpackChunkName: "production-geo" */ '../vue/views/production-geo-view.vue'),
    meta: { requiresAuth: true },
  },
  {
    path: '/register',
    name: 'register',
    component: () => import(/* webpackChunkName: "register" */ '../vue/views/register-view.vue'),
    meta: { requiresAuth: false },
  },
  {
    path: '/organization-reports',
    name: 'organization-reports',
    component: () =>
      import(/* webpackChunkName: "register" */ '../vue/views/organization-reports-view.vue'),
    meta: { requiresAuth: true },
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
