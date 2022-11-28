import { Plugin } from 'vue';
import PrimeVue from 'primevue/config';
import 'primeflex/primeflex.css';
import 'primeicons/primeicons.css';
import Tooltip from 'primevue/tooltip';
import ToastService from 'primevue/toastservice';

export const primeVuePlugin: Plugin = {
  install(app) {
    app.directive('tooltip', Tooltip);
    app.use(ToastService);
    app.use(PrimeVue, { ripple: true });
  },
};
