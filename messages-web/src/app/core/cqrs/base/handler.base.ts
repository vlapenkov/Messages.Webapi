// eslint-disable-next-line @typescript-eslint/no-explicit-any
export abstract class HandlerBase<TInput = any, TOutput = any> {
  abstract handle(input: TInput): TOutput;
}
