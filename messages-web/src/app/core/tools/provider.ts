import { inject, provide, Ref, ShallowRef, watch } from 'vue';

export class Provider<TData, TRef extends Ref<TData | undefined> | ShallowRef<TData | undefined>> {
  private key: symbol;

  constructor(private createDefault: () => TRef, name = '') {
    this.key = Symbol(name);
  }

  provide(value: TRef = this.createDefault()) {
    provide<TRef>(this.key, value);
    return value;
  }

  provideFrom(compute: () => TData) {
    const initial = this.createDefault();
    watch(
      compute,
      (val) => {
        initial.value = val;
      },
      {
        immediate: true,
      },
    );
    this.provide(initial);
    return initial;
  }

  inject() {
    return inject<TRef>(this.key, this.createDefault());
  }
}
