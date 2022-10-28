export function hiddenProp(key: string) {
  return Symbol.for(`--hidden--${key}`);
}
