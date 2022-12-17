<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <app-page title="Эксперименты">
    <div>
      <prime-card no-padding v-for="i in 10" :key="i" class="mt-2">
        <!-- <template #header>header</template> -->
        <template #title>title</template>
        <template #subtitle>subtitle</template>
        <template #footer>footer</template>
        <template #content>
          <div>
            Lorem ipsum dolor sit amet consectetur adipisicing elit. Aliquid, quaerat dignissimos!
            Natus, quo cum nam suscipit, quidem vitae recusandae amet consequatur harum veritatis
            similique voluptatibus ut magnam a? Delectus, iure alias sint autem quae quaerat
            consectetur necessitatibus ratione molestiae fugiat corrupti ab aspernatur numquam
            temporibus explicabo eum, cum facilis dolores.
          </div>
        </template>
      </prime-card>
    </div>
  </app-page>
</template>
<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  setup() {
    return {};
  },
});
</script>
