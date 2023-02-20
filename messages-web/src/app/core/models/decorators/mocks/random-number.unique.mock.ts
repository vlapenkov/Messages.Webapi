import { randomNumber } from './random-number.mock';

const pool: number[] = [];

export const randomUniqueNumber =
  (start = 0, end = 100) =>
  () => {
    const rand = randomNumber(start, end);
    let i = 10 ** 5;
    while (i > 0) {
      i -= 1;
      const randomNumb = rand();
      if (pool.every((w) => w !== randomNumb)) {
        pool.push(randomNumb);
        break;
      }
    }
    if (i === 0) {
      throw new Error('Пул кажется закончился');
    }
    return pool[pool.length - 1];
  };
