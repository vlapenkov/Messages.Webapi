<template>
  <div>
    <collection-state
      :modes="[
        { label: 'Деревом', mode: 'tree-view' },
        { label: 'Сеткой', mode: 'data-view' },
      ]"
      :state="sectionsStore"
      reload-on-save
    >
      <template #data-view>
        <data-view-collection></data-view-collection>
      </template>
      <template #tree-view>
        <splitter class="mt-2">
          <splitter-panel :size="30">
            <tree-view-collection class="reshape-tree">
              <template #default="{ node }">
                <model-provider :data="node.data">
                  <hover-tag
                    #default="{ hover }"
                    style="min-height: 50px"
                    class="flex w-full border-round justify-content-between p-2 flex-row align-items-center gap-3 tree-node-hover"
                  >
                    <span>{{ node.label }}</span>
                    <transition
                      mode="out-in"
                      enter-active-class="fadeinleft animation-duration-100"
                      leave-active-class="fadeoutleft animation-duration-100"
                    >
                      <div v-if="hover" class="flex flex-row gap-2">
                        <edit-item-button class="icon-small" />
                      </div>
                    </transition>
                  </hover-tag>
                </model-provider>
              </template>
            </tree-view-collection>
          </splitter-panel>
          <splitter-panel :size="70">
            <router-view></router-view>
          </splitter-panel>
        </splitter>
      </template>
    </collection-state>
  </div>
</template>

<script lang="ts">
import { sectionsStore } from '@/app/sections/state/sections.store';
import { defineComponent } from 'vue';

export default defineComponent({
  setup() {
    return {
      sectionsStore,
    };
  },
});
</script>

<style lang="scss" scoped>
.reshape-tree {
  box-sizing: border-box;
  :deep(.p-tree) {
    margin-top: 0 !important;
    border: none;
  }
}

$size: 0;
$padding-and-font: 1rem;
.icon-small {
  height: $size !important;
  width: $size;
  border-radius: $padding-and-font * 2;
  padding: $padding-and-font;
  :deep(.p-button-icon) {
    font-size: $padding-and-font;
  }
}

.tree-node-hover {
  transition: background-color 0.2s ease-in-out;
  &:hover {
    background-color: var(--surface-ground);
  }
}
</style>
