/* eslint-disable no-restricted-imports */
import { createStore } from '@harlem/core';
import actionExtension from '@harlem/extension-action';
import resetExtension from '@harlem/extension-reset';
import composeExtension from '@harlem/extension-compose';

// eslint-disable-next-line @typescript-eslint/no-explicit-any
export function createHarlemStore<TState extends Record<string, any>>(name: string, state: TState) {
  return createStore(name, state, {
    extensions: [actionExtension(), resetExtension(), composeExtension()],
  });
}
