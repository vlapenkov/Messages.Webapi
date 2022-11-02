<script lang="ts">
import { defineComponent, h } from 'vue';
import Button, { ButtonProps } from 'primevue/button';

export default defineComponent<ButtonProps>({
  setup(props) {
    return () => h(Button, { ...props });
  },
});
</script>
