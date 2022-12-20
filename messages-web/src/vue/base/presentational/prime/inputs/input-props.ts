import { PropType } from 'vue';
import { InputSize } from './@types/input-size';

export const inputProps = {
  size: {
    type: String as PropType<InputSize>,
    default: 'middle',
  },
  disabled: {
    type: Boolean,
    default: false,
  },
};
