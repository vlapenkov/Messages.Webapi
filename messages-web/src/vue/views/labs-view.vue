<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <app-page>
    <div>
      <card v-for="i in 50" :key="i" class="mt-2 shadow-3">
        <template #content>
          <div>
            Lorem ipsum dolor sit amet consectetur adipisicing elit. Aliquid, quaerat dignissimos!
            Natus, quo cum nam suscipit, quidem vitae recusandae amet consequatur harum veritatis
            similique voluptatibus ut magnam a? Delectus, iure alias sint autem quae quaerat
            consectetur necessitatibus ratione molestiae fugiat corrupti ab aspernatur numquam
            temporibus explicabo eum, cum facilis dolores.
          </div>
        </template>
      </card>
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
