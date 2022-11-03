import { animals } from './words/animals';
import { names } from './words/people-names';
import { surnames } from './words/people-surnames';

const capitalize = (word: string) => word.charAt(0).toUpperCase() + word.slice(1);

export const randomWord =
  (words: string[] = animals) =>
  () =>
    capitalize(words[Math.floor(Math.random() * words.length)]);

export const manyWordsFor = (fn: () => string, repeat: number) => () =>
  [...new Array(repeat)].map(() => fn()).join(' ');

export const combine =
  (...fns: (() => string)[]) =>
  () =>
    fns.map((fn) => fn()).join(' ');

export const randomName = combine(randomWord(names), randomWord(surnames));
