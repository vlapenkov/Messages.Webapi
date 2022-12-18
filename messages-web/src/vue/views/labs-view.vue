<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <app-page title="Эксперименты">
    <div class="flex flex-column gap-2">
      <prime-card title="Карточка" shadow="default" shadow-hover="none">
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
import { defineComponent } from 'vue';

export default defineComponent({
  setup() {
    return {};
  },
});
</script>
