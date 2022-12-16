import { computed, ref } from 'vue';

export interface IProductStatus {
  name: string;
  color: string;
  value: number;
}

export function useProductStatuses() {
  const statuses = ref<IProductStatus[]>([
    {
      value: 0,
      name: 'Черновик',
      color: 'var(--orange-400)',
    },
    {
      value: 1,
      name: 'Опубликован',
      color: 'var(--green-700)',
    },
    {
      value: 2,
      name: 'Архивный',
      color: 'var(--red-700)',
    },
  ]);
  const initial = computed(() => statuses.value.find((x) => x.value === 0));
  return {
    statuses,
    initial,
  };
}
