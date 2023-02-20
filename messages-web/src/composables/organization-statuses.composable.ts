import { computed, ref } from 'vue';

export interface IOrganizationStatus {
  name: string;
  color: string;
  value: number;
}

export function useOrganizationStatuses() {
  const statuses = ref<IOrganizationStatus[]>([
    {
      value: 0,
      name: 'Новая',
      color: 'var(--orange-500)',
    },
    {
      value: 1,
      name: 'Активная',
      color: 'var(--green-500)',
    },
    {
      value: 10,
      name: 'Закрыта',
      color: 'var(--red-500)',
    },
  ]);
  const initial = computed(() => statuses.value.find((x) => x.value === 0));
  return {
    statuses,
    initial,
  };
}
