import { Ref } from 'vue';
import { ProviderBase } from './provider-base';

export class Provider<T> extends ProviderBase<T, Ref<T>> {}
