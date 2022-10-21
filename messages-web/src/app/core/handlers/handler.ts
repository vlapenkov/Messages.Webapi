/**
 * Функция обработчик: Один вход - один выход
 * @example
 * const getFoo: Handler<string, undefined> = () => 'foo';
 * const f: string = getFoo();
 */
export type Handler<TOut, Tin = undefined> = (a: Tin) => TOut;

// const x: Handler<string, undefined> = () => 'foo';
// const f: sting = x();
/** Создаёт новую функцию-обработчик *
 * @example
 * const checkLength: Handler<boolean, string> = createHandler(() => {
 *   const add = 5;
 *   return (str: string) => str.length < add;
 * })`;
 * @example
 * const genericHandler = createHandler(
 *   () => <T extends string>(a: T): T => a
 * );
 * const foo: 'foo' = genericHandler('foo');
 */
export function createHandler<TOut, Tin = undefined>(
  fn: () => Handler<TOut, Tin>,
): Handler<TOut, Tin> {
  return fn();
}
