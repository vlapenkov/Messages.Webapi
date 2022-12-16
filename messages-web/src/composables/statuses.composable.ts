import { computed, ref } from 'vue';

export interface IStatus {
  name: string;
  color: string;
  value: number;
}

export function useStatuses() {
  const statuses = ref<IStatus[]>([
    {
      value: 0,
      name: 'Новая',
      color: 'var(--orange-400)',
    },
    {
      value: 1,
      name: 'Активная',
      color: 'var(--green-700)',
    },
    {
      value: 10,
      name: 'Закрыта',
      color: 'var(--red-700)',
    },
  ]);
  const initial = computed(() => statuses.value.find((x) => x.value === 0));
  return {
    statuses,
    initial,
  };
}
