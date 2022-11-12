<script lang="ts">
import { defineComponent, PropType, RenderFunction } from 'vue';

export default defineComponent({
  props: {
    func: {
      type: Function as unknown as PropType<RenderFunction | null>,
      required: true,
    },
  },
  setup(props) {
    return props.func;
  },
});
</script>
