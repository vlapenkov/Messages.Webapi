export const queryMarker = Symbol('--query-marker');

/** Маркерный интерфейс для запросов */
export interface IQuery<T> {
  [queryMarker]: never & T;
}
