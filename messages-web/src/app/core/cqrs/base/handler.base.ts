// eslint-disable-next-line @typescript-eslint/no-explicit-any
export abstract class HandlerBase<TOutput = any, TInput = undefined> {
  abstract handle(input?: TInput): TOutput;
}
