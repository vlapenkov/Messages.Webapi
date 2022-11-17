import { StateBase } from '../../base/state-base';

export const ignoreMountedPropKey = (key: string) => Symbol.for(`--state--ignore-mounted${key}`);

export function getIgnoreMountedOpt<TState extends StateBase>(target: TState, key: string) {
  return target[ignoreMountedPropKey(key) as unknown as keyof typeof target] as unknown as
    | boolean
    | undefined;
}
