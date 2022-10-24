export const dataStatuses = ['initial', 'loading', 'loaded', 'updating'] as const;

export type DataStatus = typeof dataStatuses[number];

export interface IDataStatusObject {
  status: DataStatus;
  message: string | null;
}

export const createStatus = (): IDataStatusObject => ({
  status: 'initial',
  message: null,
});
