import { useRouteQuery } from '@vueuse/router';
import { ref, Ref, watch, WritableComputedRef } from 'vue';

export type MaybeWritableRef<T> = Ref<T> | WritableComputedRef<T>;

export interface IReferenceDefinition<T extends string | number> {
  ref?: MaybeWritableRef<T | null | undefined>;
  type: 'string' | 'number';
}

export function useRouteQueryBinded(
  queryName: string,
  valueDescription: {
    ref?: MaybeWritableRef<string | null | undefined>;
    type: 'string';
  },
): MaybeWritableRef<string | null | undefined>;
export function useRouteQueryBinded(
  queryName: string,
  valueDescription: {
    ref?: MaybeWritableRef<number | null | undefined>;
    type: 'number';
  },
): MaybeWritableRef<number | null | undefined>;
export function useRouteQueryBinded<T extends number | string>(
  queryName: string,
  { ref: valueRef, type: valueType }: IReferenceDefinition<T>,
): MaybeWritableRef<T | null | undefined> {
  const resultRef: MaybeWritableRef<T | null | undefined> =
    valueRef ?? (ref(null) as Ref<T | null>);
  const queryRef = useRouteQuery(queryName);

  if (valueType === 'string') {
    watch(
      queryRef,
      (val) => {
        if (!Array.isArray(val)) {
          (resultRef as Ref<string | null>).value = val as string | null;
        }
      },
      {
        immediate: true,
      },
    );

    watch(
      resultRef as Ref<string | null>,
      (val) => {
        if (!Array.isArray(queryRef.value) && val !== queryRef.value) {
          queryRef.value = val;
        }
      },
      {
        immediate: true,
      },
    );
  } else if (valueType === 'number') {
    watch(
      queryRef,
      (val) => {
        if (!Array.isArray(val)) {
          (resultRef as Ref<number | null>).value = val == null ? null : parseInt(val, 10);
        }
      },
      {
        immediate: true,
      },
    );

    watch(
      resultRef as Ref<number | null>,
      (val) => {
        if (Array.isArray(queryRef.value)) {
          return;
        }
        const strVal = val == null ? null : `${val}`;
        if (strVal !== queryRef.value) {
          queryRef.value = strVal;
        }
      },
      {
        immediate: true,
      },
    );
  }

  return resultRef;
}
