export const modelMarker = Symbol('--model-marker');

export interface IModel {
  [modelMarker]?: never;
}

export interface IModelUnique<T extends string | number | symbol> extends IModel {
  id: T;
}

// eslint-disable-next-line @typescript-eslint/no-explicit-any
export type ModelUniqueAny = IModelUnique<any>;
