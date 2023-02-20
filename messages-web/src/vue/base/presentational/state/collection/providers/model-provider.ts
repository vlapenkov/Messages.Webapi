import { ModelBase } from '@/app/core/models/base/model-base';
import { Provider } from '@/app/core/tools/provider';
import { ref } from 'vue';

export const modelProvider = new Provider<ModelBase | undefined>(() => ref(), '--provide--model');
