export abstract class HandlerBase<TInput, TOutput> {
  abstract handle(input: TInput): TOutput;
}
