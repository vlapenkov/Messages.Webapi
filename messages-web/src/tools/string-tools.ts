export function isNullOrEmpty(str: string | null | undefined) {
  return str == null || str.trim() === '';
}
