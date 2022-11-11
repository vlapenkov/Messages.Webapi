import PrimeVue from 'primevue/config';
import { createApp } from 'vue';
import { createHead } from '@vueuse/head';
import App from './vue/App.vue';
import { harlemState } from './plugins/harlem.plugin';
import router from './router';
import 'primeflex/primeflex.css';
import 'primeicons/primeicons.css';
import './assets/styles/main.scss';
import { initKeycloak } from './app/core/services/keycloak/keycloak.service';

initKeycloak().then(() => {
  createApp(App)
    .use(router)
    .use(harlemState)
    .use(PrimeVue, { ripple: true })
    .use(createHead())
    .mount('#app');
});
