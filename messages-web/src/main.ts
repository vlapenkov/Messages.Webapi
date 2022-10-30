import PrimeVue from 'primevue/config';
import { createApp } from 'vue';
import { createHead } from '@vueuse/head';
import App from './App.vue';
import { harlemState } from './plugins/harlem.plugin';
import router from './router';
import 'primeflex/primeflex.css';
import 'primeicons/primeicons.css';
import './assets/styles/main.scss';

createApp(App).use(router).use(harlemState).use(PrimeVue).use(createHead()).mount('#app');
