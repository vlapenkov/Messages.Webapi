import { AnyHandler, Handler } from './handler';

/** Обработчик, вызывающий внутри себя друой обработчик
 * @example
 * // считаем длину строки
 * const testHandler = createHandler(() => (str: string) => str.length);
 * // считаем длины всех строк в массиве
 * const wrapper: HandlerWrapper<typeof testHandler, Handler<number[], string[]>> =
 *   (getLen) => (arr) =>
 *     arr.map(getLen);
 * const newHand ler: Handler<number[], string[]> = wrapper(testHandler);
 */
export type HandlerWrapper<TWrapped extends AnyHandler, TWrapper extends AnyHandler> = (
  handler: TWrapped,
) => TWrapper;

/** Декоратор - это обёртка, которая не меняет тип входных и выходных параметров */
export type HandlerDecorator<THandler extends AnyHandler> = HandlerWrapper<THandler, THandler>;

/** Создаёт обёртку для хендлера нужного типа
 * @example
 * // считаем длину строки
 * const testHandler: Handler<number, string> = createHandler(() => (i: string) => i.length);
 * // считаем длины всех строк в массиве
 * const wrapper = createWrapper<typeof testHandler, Handler<number[], string[]>>(
 *   (handler) => (arr: string[]) => arr.map(handler),
 * );
 * const wrappedHandler: Handler<number[], string[]> = wrapper((i: string) => i.length + 1);
 */
export function createWrapper<TWrapped extends AnyHandler, TWrapper extends AnyHandler>(
  setup: (handler: TWrapped) => TWrapper,
): HandlerWrapper<TWrapped, ReturnType<typeof setup>> {
  return setup;
}

/** Создаёт обёртку для существующего типа хендлеров
 * @param _handler - нужен, чтобы корректно вывести тип (см. пример)
 * @example
 * const testHandler: Handler<number, string> = createHandler(() => (i: string) => i.length);
 *
 * const wrapper = createWrapperFor(testHandler, (handler) => (arr: string[]) => arr.map(handler));
 *
 * const wrappedHandler: Handler<number[], string[]> = wrapper((i: string) => i.length + 1);
 */
export function createWrapperFor<TOutWas, TOut, TinWas = void, Tin = void>(
  _handler: Handler<TOutWas, TinWas>,
  setup: HandlerWrapper<Handler<TOutWas, TinWas>, Handler<TOut, Tin>>,
) {
  return setup;
}
