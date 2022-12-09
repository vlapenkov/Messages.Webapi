import { Plugin } from 'vue';
import PrimeVue from 'primevue/config';
import 'primeflex/primeflex.css';
import 'primeicons/primeicons.css';
import Tooltip from 'primevue/tooltip';
import BadgeDirective from 'primevue/badgedirective';
import ToastService from 'primevue/toastservice';

export const primeVuePlugin: Plugin = {
  install(app) {
    app.directive('tooltip', Tooltip);
    app.directive('badge', BadgeDirective);
    app.use(ToastService);
    app.use(PrimeVue, { ripple: true });
  },
};
