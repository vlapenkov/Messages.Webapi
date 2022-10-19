export const commandMarker = Symbol('--command-marker');

/** Маркерный интерфейс для команд */
// eslint-disable-next-line @typescript-eslint/no-unused-vars
export interface ICommand<T> {
  [commandMarker]: never;
}
