/* eslint-disable max-classes-per-file */
export const loadingStatuses = ['initial', 'loading', 'loaded', 'updating', 'error'] as const;

export type LoadingStatus = typeof loadingStatuses[number];

export class DataStatus {
  constructor(public status: LoadingStatus = 'initial', public message: string | null = null) {}
}
