import { defineRepository } from '@/app/core/repositories/http-repository-base';

export const repo = defineRepository({
  setup: () => {
    throw new Error('Not Implemented!');
  },
  url: '',
});
