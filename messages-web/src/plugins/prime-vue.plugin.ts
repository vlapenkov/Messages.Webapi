import { Plugin } from 'vue';
import PrimeVue from 'primevue/config';
import 'primeflex/primeflex.css';
import 'primeicons/primeicons.css';
import Tooltip from 'primevue/tooltip';

export const primeVuePlugin: Plugin = {
  install(app) {
    app.directive('tooltip', Tooltip);
    app.use(PrimeVue, { ripple: true });
  },
};
