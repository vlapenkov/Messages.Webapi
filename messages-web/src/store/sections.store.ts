import { createStore } from '@harlem/core';

import actionExtension from '@harlem/extension-action';

export interface IActionsState {
  name: string;
}

const defaultState: IActionsState = {
  name: 'foo',
};

export const { state } = createStore('sections', defaultState, {
  extensions: [actionExtension()],
});
