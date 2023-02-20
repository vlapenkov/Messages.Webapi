<script lang="ts">
import { defineComponent, h, PropType } from 'vue';
import { ymapMarker } from 'vue-yandex-maps';
import markerImage from '@/assets/icons/marker.png';

interface IIcon {
  layout: string;
  imageHref: string;
  imageSize: number[];
  imageOffset: number[];
}

export default defineComponent({
  props: {
    ...ymapMarker.props,
    icon: {
      type: Object as PropType<IIcon>,
      default: null,
    },
  },
  setup(props, { slots }) {
    const defaultIcon = {
      layout: 'default#image',
      imageHref: markerImage,
      imageSize: [22, 35],
      imageOffset: [0, 0],
    };
    return () => h(ymapMarker, { ...props, icon: props.icon ?? defaultIcon }, { ...slots });
  },
});
</script>

<style scoped></style>
