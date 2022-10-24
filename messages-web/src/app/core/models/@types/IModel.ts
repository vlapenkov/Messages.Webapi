export const modelMarker = Symbol('--model-marker');

export interface IModel {
  [modelMarker]: never;
}
