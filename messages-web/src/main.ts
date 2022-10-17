import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
import { harlemState } from './tools/plugins/harlem.plugin';

createApp(App).use(router).use(harlemState).mount('#app');
