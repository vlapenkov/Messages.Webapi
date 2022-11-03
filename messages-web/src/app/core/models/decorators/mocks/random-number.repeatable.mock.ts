const cache: number[] = [];

/** Выкидывает случайное число в интервале либо выдаёт одно из выпавших ранее */
export function randomRepeatableNumber(start = 0, end = 100, chance = 0.5): () => number {
  return () => {
    if (Math.random() < chance) {
      const index = Math.floor(Math.random() * cache.length);
      return cache[index];
    }

    const randNumb = Math.floor(Math.random() * (end - start) + start);
    cache.push(randNumb);
    return randNumb;
  };
}
