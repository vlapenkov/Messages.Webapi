<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <app-page title="Эксперименты">
    <div>
      <prime-card title="Inputs">
        <div class="p-fluid grid mt-3">
          <prime-input-text class="col-4" label="Текст" v-model="text"></prime-input-text>

          <prime-input-text
            class="col-4"
            label="Иконка слева"
            icon="pi pi-search"
            size="small"
            v-model="text"
          ></prime-input-text>

          <prime-input-text
            class="col-4"
            label="Иконка справа"
            icon-placement="right"
            icon="pi pi-search"
            v-model="text"
          ></prime-input-text>

          <prime-input-number class="col-4" label="Число" v-model="number"></prime-input-number>
        </div>
      </prime-card>
      <prime-card no-padding v-for="i in 10" :key="i" class="mt-2">
        <!-- <template #header>header</template> -->
        <template #title>title</template>
        <template #subtitle>subtitle</template>
        <template #footer>footer</template>
        <div>
          Lorem ipsum dolor sit amet consectetur adipisicing elit. Aliquid, quaerat dignissimos!
          Natus, quo cum nam suscipit, quidem vitae recusandae amet consequatur harum veritatis
          similique voluptatibus ut magnam a? Delectus, iure alias sint autem quae quaerat
          consectetur necessitatibus ratione molestiae fugiat corrupti ab aspernatur numquam
          temporibus explicabo eum, cum facilis dolores.
        </div>
      </prime-card>
    </div>
  </app-page>
</template>
<script lang="ts">
import { defineComponent, ref } from 'vue';
import { InputSize } from '../base/presentational/prime/inputs/@types/input-size';

export default defineComponent({
  setup() {
    const text = ref('some text');
    const number = ref(666);
    const size = ref<InputSize>('middle');
    return { text, number, size };
  },
});
</script>
