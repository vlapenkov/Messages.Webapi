import type { ModelBase } from '../base/model-base';

export const titleProp = Symbol.for('--title');

/** Данное свойство является названием для объекта модели */
export const title = <T extends ModelBase>(target: T, key: string) => {
  Object.defineProperty(target, titleProp, {
    get: () => key,
  });
};
