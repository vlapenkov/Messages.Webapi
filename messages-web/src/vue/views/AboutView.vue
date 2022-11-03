<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <div>
    <h1>This is an about page</h1>
    <div class="p-fluid grid">
      <div class="field col-12 md:col-4">
        <span class="p-float-label">
          <input-text id="inputtext" type="text" v-model="value1" />
          <label for="inputtext">Input Text</label>
        </span>
      </div>
    </div>
    <card class="mt-3 shadow-4" v-for="i in 5" :key="i">
      <template #content>
        Lorem ipsum dolor sit amet consectetur adipisicing elit. Itaque repudiandae quae distinctio
        non ipsa. Earum vel a eum debitis fuga enim accusantium omnis ad voluptatem repudiandae
        unde, atque et ex, saepe sint incidunt. Eaque, quae animi. Sapiente dolorum sit non laborum
        provident, itaque laboriosam iusto est suscipit cum voluptates in?
      </template>
    </card>
  </div>
</template>
<script lang="ts">
import { defineComponent, ref } from 'vue';

export default defineComponent({
  setup() {
    const value1 = ref('hello world');
    return { value1 };
  },
});
</script>
