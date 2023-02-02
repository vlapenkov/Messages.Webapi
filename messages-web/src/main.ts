import { createApp } from 'vue';
import { createHead } from '@vueuse/head';
import AppRoot from './vue/app-root.vue';
import { harlemState } from './plugins/harlem.plugin';
import router from './router';
import './assets/styles/main.scss';
import { initKeycloak } from './app/core/services/keycloak/keycloak.service';
import { primeVuePlugin } from './plugins/prime-vue.plugin';
import { initReloader } from './tools/reload-debounced';

const reload = initReloader({
  maxReloads: 10,
  debounceInSeconds: 15,
});

initKeycloak()
  .then(() => {
    createApp(AppRoot)
      .use(router)
      .use(harlemState)
      .use(primeVuePlugin)
      .use(createHead())
      .mount('#app');
  })
  .catch((_) => {
    reload();
  });
