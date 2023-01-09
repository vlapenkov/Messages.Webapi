<script lang="ts">
import { defineComponent, h } from 'vue';
import { yandexMap } from 'vue-yandex-maps';

interface IYandexMapSettings {
  apiKey: string;
  lang: string;
  coordorder: string;
  version: string;
}

export default defineComponent({
  setup(props, { slots }) {
    const settings: IYandexMapSettings = {
      apiKey: process.env.VUE_APP_YANDEX_MAP_API_KEY,
      lang: process.env.VUE_APP_YANDEX_MAP_LANG,
      coordorder: process.env.VUE_APP_YANDEX_MAP_COORDORDER,
      version: process.env.VUE_APP_YANDEX_MAP_VERSION,
    };
    return () => h(yandexMap, { ...props, settings }, { ...slots });
  },
});
</script>

<style scoped></style>
