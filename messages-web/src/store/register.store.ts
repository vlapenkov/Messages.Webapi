import { defineStore } from '@/app/core/services/harlem/harlem.service';

export interface IRegisterStore {
  showRegisterDialog: boolean;
}

const defaultState: IRegisterStore = {
  showRegisterDialog: false,
};

const { computeState } = defineStore('register', defaultState);

export const showRegisterDialog = computeState((state) => state.showRegisterDialog);
