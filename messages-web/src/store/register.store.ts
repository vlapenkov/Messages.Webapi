import { defineStore } from '@/app/core/services/harlem/harlem.service';
import { computed } from 'vue';
import { RouteLocation } from 'vue-router';

export interface IRegisterStore {
  dialog: {
    show: boolean;
    redirectTo?: RouteLocation;
  };
}

const defaultState: IRegisterStore = {
  dialog: {
    show: false,
    redirectTo: undefined,
  },
};

const { getter, mutation } = defineStore('register', defaultState);

export const showDialog = mutation('show-dialog', (state, to: RouteLocation | undefined) => {
  state.dialog.show = true;
  state.dialog.redirectTo = to;
});

export const hideDialog = mutation('hide-dialog', (state, to: RouteLocation | undefined) => {
  state.dialog.show = false;
  state.dialog.redirectTo = to;
});

export const hasShowDialog = getter('has-show-dialog', (state) => state.dialog.show);

export const redirectToLocation = getter(
  'get-redirect-to-location',
  (state) => state.dialog.redirectTo,
);

export const showRegisterDialog = computed({
  get: () => hasShowDialog.value,
  set: (val) => {
    if (val) {
      showDialog();
    } else {
      hideDialog();
    }
  },
});
