/* eslint-disable no-restricted-imports */
import { createStore } from '@harlem/core';
import actionExtension from '@harlem/extension-action';
import resetExtension from '@harlem/extension-reset';
import composeExtension from '@harlem/extension-compose';
import { AnyRecord } from '@/app/@types/any-record';

export function createHarlemStore<TState extends AnyRecord<string>>(name: string, state: TState) {
  return createStore(name, state, {
    extensions: [actionExtension(), resetExtension(), composeExtension()],
  });
}
