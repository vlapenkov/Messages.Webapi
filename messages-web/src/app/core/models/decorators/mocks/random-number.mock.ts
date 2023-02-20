export function randomNumber(start = 0, end = 1): () => number {
  return () => Math.floor(Math.random() * (end - start) + start);
}
