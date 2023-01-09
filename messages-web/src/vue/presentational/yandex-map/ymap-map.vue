<script lang="ts">
import { defineComponent, h, PropType } from 'vue';
import { yandexMap } from 'vue-yandex-maps';

interface IYandexMapSettings {
  apiKey: string;
  lang: string;
  coordorder: string;
  version: string;
}

export default defineComponent({
  props: {
    ...yandexMap.props,
    settings: {
      type: Object as PropType<IYandexMapSettings>,
      default: null,
    },
  },
  setup(props, { slots }) {
    const defaultSettings: IYandexMapSettings = {
      apiKey: process.env.VUE_APP_YANDEX_MAP_API_KEY,
      lang: process.env.VUE_APP_YANDEX_MAP_LANG,
      coordorder: process.env.VUE_APP_YANDEX_MAP_COORDORDER,
      version: process.env.VUE_APP_YANDEX_MAP_VERSION,
    };
    return () =>
      h(yandexMap, { ...props, settings: props.settings ?? defaultSettings }, { ...slots });
  },
});
</script>

<style scoped></style>
