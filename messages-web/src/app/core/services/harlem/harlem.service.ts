/* eslint-disable @typescript-eslint/no-restricted-imports */
import { createStore } from '@harlem/core';
import actionExtension from '@harlem/extension-action';
import resetExtension from '@harlem/extension-reset';
import composeExtension from '@harlem/extension-compose';
import { AnyRecord } from '@/app/@types/any-record';

export function createDefaultStore<TState extends AnyRecord<string>>(name: string, state: TState) {
  return createStore(name, state, {
    extensions: [actionExtension(), resetExtension(), composeExtension()],
  });
}

/** Этот класс нужен чтобы Тайпскрипту вывести типы. */
class CdsWrapper<TState extends AnyRecord<string>> {
  // eslint-disable-next-line class-methods-use-this
  cds(name: string, state: TState) {
    return createDefaultStore(name, state);
  }
}

/** Возвращаемый тип из функции  createDefaultStore */
export type DefaultStore<TState extends AnyRecord<string>> = ReturnType<CdsWrapper<TState>['cds']>;
