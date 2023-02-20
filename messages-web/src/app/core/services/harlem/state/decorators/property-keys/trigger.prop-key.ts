import { StateBase } from '../../base/state-base';

export const triggerPropkeyFor = (key: string) => Symbol.for(`--state--trigger--${key}`);

export function getTriggerFor<TState extends StateBase>(target: TState, key: keyof TState) {
  const triggerPropkey = triggerPropkeyFor(key as string);
  const methodName = target[triggerPropkey as unknown as keyof TState];
  return methodName as unknown as string | undefined;
}
