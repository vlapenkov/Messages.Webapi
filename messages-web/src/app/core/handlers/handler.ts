export type Handler<TOut, Tin = undefined> = (a: Tin) => TOut;

// const x: Handler<string, undefined> = () => 'foo';

// x();

export function handler<TOut, Tin = undefined>(fn: () => Handler<TOut, Tin>) {
  return fn();
}
