import { createApp } from 'vue';
import { createHead } from '@vueuse/head';
import AppRoot from './vue/app-root.vue';
import { harlemState } from './plugins/harlem.plugin';
import router from './router';
import './assets/styles/main.scss';
import { primeVuePlugin } from './plugins/prime-vue.plugin';

createApp(AppRoot).use(router).use(harlemState).use(primeVuePlugin).use(createHead()).mount('#app');
