import { Plugin } from 'vue';
import PrimeVue from 'primevue/config';
import 'primeflex/primeflex.css';
import 'primeicons/primeicons.css';
import Tooltip from 'primevue/tooltip';
import BadgeDirective from 'primevue/badgedirective';
import ToastService from 'primevue/toastservice';

import 'primevue/resources/themes/lara-light-blue/theme.css';
import 'primevue/resources/primevue.min.css';

export const primeVuePlugin: Plugin = {
  install(app) {
    app.directive('tooltip', Tooltip);
    app.directive('badge', BadgeDirective);
    app.use(ToastService);
    app.use(PrimeVue, { ripple: true });
  },
};
