export const queryMarker = Symbol('--query-marker');

/** Маркерный интерфейс для запросов */
// eslint-disable-next-line @typescript-eslint/no-unused-vars
export interface IQuery<T> {
  [queryMarker]: never;
}
