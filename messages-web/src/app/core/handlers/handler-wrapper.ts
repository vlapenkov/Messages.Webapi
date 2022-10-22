import { AnyHandler } from './handler';

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

/** Создаёт обёртку для хендлера нужного типа
 * @example
 * // считаем длину строки
 * const testHandler: Handler<number, string> = createHandler(() => (i: string) => i.length);
 * // считаем длины всех строк в массиве
 * const wrapper = createWrapperFor<typeof testHandler, Handler<number[], string[]>>(
 *   (handler) => (arr: string[]) => arr.map(handler),
 * );
 * const wrappedHandler: Handler<number[], string[]> = wrapper((i: string) => i.length + 1);
 */
export function createWrapperFor<TWrapped extends AnyHandler, TWrapper extends AnyHandler>(
  setup: (handler: TWrapped) => TWrapper,
): HandlerWrapper<TWrapped, ReturnType<typeof setup>> {
  return setup;
}
