export const commandMarker = Symbol('--command-marker');

/** Маркерный интерфейс для команд */
export interface ICommand<T> {
  [commandMarker]: never & T;
}
