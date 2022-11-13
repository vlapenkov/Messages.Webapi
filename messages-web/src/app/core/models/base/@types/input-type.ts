export const inputTypes = ['text', 'number'] as const;

export type InputType = typeof inputTypes[number];
