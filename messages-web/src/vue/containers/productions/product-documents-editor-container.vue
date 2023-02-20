<template>
  <div class="flex flex-column gap-2">
    <div class="relative text-center">
      <img v-if="docsToDisplay.firstDoc == null" alt="load image" class="w-full"
        :src="require('@/assets/images/fallback-image.png')" />
      <product-document-display v-else :document="docsToDisplay.firstDoc">
      </product-document-display>
      <div class="absolute bottom-0 w-full flex flex-row pb-3 justify-content-center">
        <file-upload mode="basic" id="organization-img" accept="image/*" :maxFileSize="3000000" @input="onFileInput"
          :auto="true" :customUpload="true" chooseLabel="Загрузить изображение" class="p-button-sm" />
      </div>
    </div>
    <div class="grid">
      <div class="col-3 h-full" v-for="(doc, i) in docsToDisplay.otherDocs" :key="i">
        <img v-if="doc == null" alt="load image" class="w-full" :src="require('@/assets/images/fallback-image.png')" />
        <product-document-display style="min-height: 80.12px" v-else :document="doc" />
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { IProductDocument } from '@/app/product-full/@types/IProductDocument';
import { useBase64 } from '@vueuse/core';
import { computed, defineComponent, PropType, ref, Ref, toRaw, watch } from 'vue';
import { v4 as uuidv4 } from 'uuid';

export default defineComponent({
  props: {
    modelValue: {
      type: Array as PropType<IProductDocument[]>,
      default: (): IProductDocument[] => [],
    },
  },
  emits: {
    'update:modelValue': (_: IProductDocument[]) => true,
  },
  setup(props, { emit }) {
    const docs = computed({
      get: () => props.modelValue,
      set: (val) => {
        emit('update:modelValue', val);
      },
    });
    const file = ref() as Ref<File>;

    const { base64: fileB64 } = useBase64(file);

    watch(fileB64, (b64) => {
      if (file.value == null || b64 == null) {
        return;
      }

      const docsToUpdate = docs.value.map((x) => toRaw(x));
      docsToUpdate.push({
        data: b64.replace(/data:image\/(\w*);base64,/, ''),
        fileId: uuidv4(),
        fileName: file.value.name,
      });
      // console.log({ docsToUpdate });

      docs.value = docsToUpdate;
    });

    const onFileInput = (e: Event) => {
      const target = e.target as HTMLInputElement;
      const { files } = target;
      if (files == null) {
        return;
      }
      [file.value] = files;
    };

    const docsToDisplay = computed(() => {
      const [firstDoc, ...rest] = docs.value.slice(0, 5);
      const otherDocs: (IProductDocument | null)[] = [...rest];
      if (otherDocs.length < 4) {
        const addLen = 4 - otherDocs.length;
        for (let i = 0; i < addLen; i += 1) {
          otherDocs.push(null);
        }
      }
      return { firstDoc, otherDocs };
    });

    return { docsToDisplay, onFileInput };
  },
});
</script>

<style scoped>

</style>
