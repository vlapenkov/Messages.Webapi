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

const showDialog = mutation<RouteLocation | undefined>(
  'show-dialog',
  (state, to?: RouteLocation) => {
    state.dialog.show = true;
    state.dialog.redirectTo = to;
  },
);

const hideDialog = mutation<RouteLocation | undefined>(
  'hide-dialog',
  (state, to?: RouteLocation) => {
    state.dialog.show = false;
    state.dialog.redirectTo = to;
  },
);

const hasShowDialog = getter('has-show-dialog', (state) => state.dialog.show);

const redirectToLocation = getter('get-redirect-to-location', (state) => state.dialog.redirectTo);

const isShowDialog = computed({
  get: () => hasShowDialog.value,
  set: (val) => {
    if (val) {
      showDialog();
    } else {
      hideDialog();
    }
  },
});

export const registerStore = {
  isShowDialog,
  redirectToLocation,
  showDialog,
  hideDialog,
};
