export const renderPropkey = (key: string, mode = 'default') =>
  Symbol.for(`--display-${key}:${mode}`);
