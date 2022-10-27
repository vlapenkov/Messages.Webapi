export const modelMarker = Symbol('--model-marker');

export interface IModel {
  [modelMarker]: never;
}

export interface IModelUnique<T extends string | number | symbol> extends IModel {
  id: T;
}
