/**
 * Функция обработчик: Один вход - один выход
 * @example
 * const getFoo: Handler<string, undefined> = () => 'foo';
 * const f: string = getFoo();
 */

export type Handler<TOut, Tin = void> = (a: Tin) => TOut;

// eslint-disable-next-line @typescript-eslint/no-explicit-any
export type AnyHandler = Handler<any, any>;

/** Вычисляет тип входных параметров для произвольного обработчика
 * @example
const checkLength: Handler<boolean, string> = createHandler(() => {
  const add = 5;
  return (str: string) => str.length < add;
});

const x: InputOf<typeof checkLength> = 'ddd'
 */
export type InputOf<T extends AnyHandler> = Parameters<T>[0];

/** Возвращает тип результата хендлера
 * @example
const checkLength: Handler<boolean, string> = createHandler(() => {
  const add = 5;
  return (str: string) => str.length < add;
});

const y: OutputOf<typeof checkLength> = true;
 */
export type OutputOf<T extends AnyHandler> = ReturnType<T>;

/** Создаёт новую функцию-обработчик
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
export function createHandler<TOut, Tin = void>(fn: () => Handler<TOut, Tin>): Handler<TOut, Tin> {
  return fn();
}
