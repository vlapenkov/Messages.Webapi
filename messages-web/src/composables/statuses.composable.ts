import { ref } from 'vue';

export interface IStatus {
  name: string;
  color: string;
}

export function useStatuses() {
  const statuses = ref<IStatus[]>([
    {
      name: 'Новая',
      color: 'var(--orange-400)',
    },
    {
      name: 'Активна',
      color: 'var(--green-700)',
    },
    {
      name: 'Закрыта',
      color: 'var(--red-700)',
    },
  ]);
  return {
    statuses,
  };
}
