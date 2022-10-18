import PrimeVue from 'primevue/config';
import { createApp } from 'vue';
import App from './App.vue';
import { harlemState } from './plugins/harlem.plugin';
import router from './router';

createApp(App).use(router).use(harlemState).use(PrimeVue).mount('#app');
