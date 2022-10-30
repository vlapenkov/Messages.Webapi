import PrimeVue from 'primevue/config';
import { createApp } from 'vue';
import App from './App.vue';
import { harlemState } from './plugins/harlem.plugin';
import router from './router';
import 'primeflex/primeflex.css';
import './assets/styles/main.scss';
import 'primevue/resources/themes/lara-dark-indigo/theme.css';

createApp(App).use(router).use(harlemState).use(PrimeVue).mount('#app');
