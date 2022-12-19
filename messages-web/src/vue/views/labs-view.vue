<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <app-page title="Эксперименты">
    <div class="flex flex-column gap-3">
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
      <prime-card no-padding class>
        <!-- <template #header>header</template> -->
        <template #title>
          <app-text mode="header">Header</app-text>
        </template>
        <template #subtitle>
          <app-text mode="subheader">SubHeader</app-text>
        </template>
        <template #footer>
          <app-text mode="primary">Primary</app-text>
          <app-text mode="weak">Weak</app-text>
          <app-text mode="weaker">Weaker</app-text>
        </template>
        <app-text mode="default">
          Lorem ipsum dolor sit amet consectetur adipisicing elit. Aliquid, quaerat dignissimos!
          Natus, quo cum nam suscipit, quidem vitae recusandae amet consequatur harum veritatis
          similique voluptatibus ut magnam a? Delectus, iure alias sint autem quae quaerat
          consectetur necessitatibus ratione molestiae fugiat corrupti ab aspernatur numquam
          temporibus explicabo eum, cum facilis dolores.
        </app-text>
      </prime-card>
      <prime-card title="Кнопки" shadow="default" shadow-hover="none">
        <template #content>
          <div class="flex flex-row gap-2"></div>
          <div class="flex flex-row flex-wrap gap-2">
            <prime-button label="Primary Button"></prime-button>
            <prime-button icon="pi pi-plus" label="With icon"></prime-button>
            <prime-button icon="pi pi-plus"></prime-button>
            <prime-button icon="pi pi-plus" class="p-button-rounded"></prime-button>
          </div>
          <div class="flex flex-row flex-wrap gap-2">
            <prime-button label="Primary Button" class="p-button-sm"></prime-button>
            <prime-button icon="pi pi-plus" class="p-button-sm" label="With icon"></prime-button>
            <prime-button icon="pi pi-plus" class="p-button-sm"></prime-button>
            <prime-button icon="pi pi-plus" class="p-button-rounded p-button-sm"></prime-button>
          </div>
        </template>
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
