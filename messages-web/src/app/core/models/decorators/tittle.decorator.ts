import type { ModelBase } from '../base/model-base';

export const titleProp = Symbol('--title-prop');
/** Данное свойство является названием для объекта модели */
export const title = <T extends ModelBase>(target: T, key: string) => {
  target[titleProp] = key;
};
