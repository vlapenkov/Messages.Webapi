import { AnyRecord } from '@/app/@types/any-record';
import { watch } from 'vue';
import { DefaultStore } from '../../harlem.service';
import { StateBase } from '../../state/base/state-base';
import { getTriggerFor } from '../../state/decorators/property-keys/trigger.prop-key';

export function useTriggers<TState extends StateBase>(
  { state, getter }: DefaultStore<TState>,
  actions: AnyRecord<string>,
) {
  Object.keys(state)
    .filter((key) => typeof state[key as unknown as keyof typeof state] === 'function')
    .map((key) => {
      const triggerMethod = getTriggerFor(state, key as keyof typeof state);
      if (triggerMethod == null) {
        return null;
      }
      return {
        key,
        triggerMethod,
      };
    })
    .filter((x): x is NonNullable<typeof x> => x != null)
    .forEach(({ key, triggerMethod }) => {
      const methodToWatch = getter(`watch-for-${key}`, (getterState) => {
        const fn = getterState[key as unknown as keyof typeof state] as unknown as (
          ...args: unknown[]
        ) => unknown;
        return fn();
      });
      watch(methodToWatch, (wv) => {
        actions[triggerMethod](wv);
      });
    });
}
