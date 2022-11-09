export const displayModes = ['data-view', 'tree-view'] as const;

export type DisplayMode = typeof displayModes[number];
