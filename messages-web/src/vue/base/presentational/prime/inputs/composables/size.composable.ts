import { computed } from 'vue';
import { InputSize } from '../@types/input-size';

export function useSize(size: InputSize) {
  return computed(() => {
    const classes: string[] = [];
    switch (size) {
      case 'large':
        classes.push('p-inputtext-lg');
        break;
      case 'small':
        classes.push('p-inputtext-sm');
        break;
      case 'middle':
        break;
      default:
        throw new Error('Неизвестный размер');
    }
    return classes;
  });
}
