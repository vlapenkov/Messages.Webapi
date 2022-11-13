<template>
  <div
    class="flex flex-row align-items-center gap-2 p-1 pl-2 avatar border-round-3xl"
    v-if="isAuthenticated"
  >
    <div>
      {{ userShortName }}
    </div>
    <avatar shape="circle" :image="gravatarUrl"></avatar>
  </div>
</template>

<script lang="ts">
import { isAuthenticated, userInfo } from '@/store/user.store';
import { computed, defineComponent } from 'vue';
import { url } from 'gravatar';

const avatarSize = 100;
export default defineComponent({
  setup() {
    const gravatarUrl = computed(() =>
      userInfo.value == null ? undefined : url(userInfo.value.email, { s: `${avatarSize}` }),
    );
    const userShortName = computed(() =>
      [(userInfo.value?.familyName ?? '')[0], (userInfo.value?.givenName ?? '')[0]].join(' '),
    );
    return { isAuthenticated, gravatarUrl, userShortName };
  },
});
</script>

<style lang="scss" scoped>
.avatar {
  transition: background-color 0.35s ease-in-out;
  transition: color 0.35s ease-in-out;
  &:hover {
    color: var(--primary-color-text);
    background-color: var(--primary-300);
    cursor: pointer;
  }
}
</style>
