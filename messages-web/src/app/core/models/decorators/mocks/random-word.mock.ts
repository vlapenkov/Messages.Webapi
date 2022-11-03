import { animals } from './words/animals';
import { names } from './words/people-names';
import { surnames } from './words/people-surnames';

export const randomWord =
  (words: string[] = animals) =>
  () =>
    words[Math.floor(Math.random() * animals.length)];

export const manyWordsFor = (fn: () => string, repeat: number) => () =>
  [...new Array(repeat)].map(() => fn()).join(' ');

export const combine =
  (...fns: (() => string)[]) =>
  () =>
    fns.map((fn) => fn()).join(' ');

export const randomName = combine(randomWord(names), randomWord(surnames));
