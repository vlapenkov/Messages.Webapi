export function descriptonPropkey(key: string) {
  return Symbol.for(`--description-${key}`);
}
