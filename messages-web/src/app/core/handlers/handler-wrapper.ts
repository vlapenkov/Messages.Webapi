import { Handler } from './handler';

/** Обработчик, вызывающий внутри себя друой обработчик
 * @example
 * const testHandler = create(() => (str: string) => str.length);
 *
 * const wrapper: HandlerWrapper<number, number[], string, string[]> = (getLen) => (arr) =>
 *   arr.map(getLen);
 *
 * const newHandler: Handler<number[], string[]> = wrapper(testHandler);
 */
export type HandlerWrapper<TOutWas, TOut, TinWas = undefined, Tin = undefined> = (
  handler: Handler<TOutWas, TinWas>,
) => Handler<TOut, Tin>;

/** Создаёт обёртку для хендлера нужного типа
 * @example
 * const testHandler: Handler<number, string> = createHandler(() => (i: string) => i.length);
 *
 * const wrapper = createWrapperFor(testHandler, (handler) => (arr: string[]) => arr.map(handler));
 *
 * const wrappedHandler: Handler<number[], string[]> = wrapper((i: string) => i.length + 1);
 */
export function createWrapperFor<TOutWas, TOut, TinWas = undefined, Tin = undefined>(
  _handler: Handler<TOutWas, TinWas>,
  setup: HandlerWrapper<TOutWas, TOut, TinWas, Tin>,
) {
  return setup;
}
