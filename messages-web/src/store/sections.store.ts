import { createHarlemStore } from '@/app/core/services/harlem/harlem.service';

export interface IActionsState {
  name: string;
}

const defaultState: IActionsState = {
  name: 'foo',
};

export const { state } = createHarlemStore('sections', defaultState);
