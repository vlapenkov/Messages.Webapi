<template>
  <div
    class="flex flex-row align-items-center gap-2 p-1 pl-3 avatar border-round-3xl"
    :style="selectionColor"
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
import { computed, CSSProperties, defineComponent } from 'vue';
import { url } from 'gravatar';
import { screenSmall } from '@/app/core/services/window/window.service';
import { isDark } from '@/store/theme.store';

const avatarSize = 100;
export default defineComponent({
  setup() {
    const gravatarUrl = computed(() =>
      userInfo.value == null ? undefined : url(userInfo.value.email, { s: `${avatarSize}` }),
    );
    const userShortName = computed(() =>
      [userInfo.value?.familyName ?? '', userInfo.value?.givenName ?? '']
        .map((part) => (!screenSmall.value ? `${part[0]}.` : part))
        .join(' '),
    );
    const selectionColor = computed<CSSProperties>(() => ({
      '--user-selection-olor': !isDark.value ? '#000000' : '#ffffff',
    }));
    return { isAuthenticated, gravatarUrl, userShortName, selectionColor };
  },
});
</script>

<style lang="scss" scoped>
.avatar {
  transition: background-color 0.35s ease-in-out;
  transition: color 0.35s ease-in-out;
  &:hover {
    background-color: rgba($color: var(--user-selection-color), $alpha: 0.05);
    cursor: pointer;
  }
}
</style>
