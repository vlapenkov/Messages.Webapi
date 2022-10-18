export abstract class Handler<TInput, TOutput> {
  abstract handle(input: TInput): TOutput;
}
