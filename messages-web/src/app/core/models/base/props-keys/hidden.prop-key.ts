export function hiddenPropkey(key: string) {
  return Symbol.for(`--hidden--${key}`);
}
