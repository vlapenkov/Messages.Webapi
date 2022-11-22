import { ShallowRef } from 'vue';
import { ProviderBase } from './provider-base';

export class ShallowProvider<T> extends ProviderBase<T, ShallowRef<T>> {}
