<template>
  <img
    v-if="id != null && imageData != null"
    class="max-height"
    :src="imageData"
    alt="Изображение товара"
  />
  <skeleton v-else height="100px" class="max-height"></skeleton>
</template>

<script lang="ts">
import { http } from '@/app/core/services/http/axios/axios.service';
import { defineComponent, ref, watch } from 'vue';

export default defineComponent({
  props: {
    id: {
      type: String,
    },
  },
  setup(props) {
    const imageData = ref<string>();
    watch(
      () => props.id,
      (idVal) => {
        if (idVal != null && idVal !== '' && idVal.trim() !== '') {
          http.get(`/files/${idVal}`).then((response) => {
            if (response.status === 200) {
              imageData.value = response.data;
            }
          });
        }
      },
      {
        immediate: true,
      },
    );

    return { imageData };
  },
});
</script>

<style lang="scss" scoped>
.max-height {
  max-height: 100px;
}
</style>
