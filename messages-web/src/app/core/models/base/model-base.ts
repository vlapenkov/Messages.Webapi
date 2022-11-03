import useVuelidate, {
  ValidationRule,
  ValidationRuleCollection,
  Validation,
  ValidationArgs,
} from '@vuelidate/core';
import { Ref, RenderFunction } from 'vue';
import { IModel, modelMarker } from '../@types/IModel';
import { descriptonPropkey } from './props-keys/descripton.prop-key';
import { titleProp } from '../decorators/tittle.decorator';
import { validationPropkey } from './props-keys/validation.prop-key';
import { hiddenPropkey } from './props-keys/hidden.prop-key';
import { renderPropkey } from './props-keys/render.prop-key';
import { DisplayMode } from '../decorators/@types/ViewMode';
import { mockPropKey } from './props-keys/mock.prop-key';

export const inputTypes = ['text', 'number'] as const;

export type InputType = typeof inputTypes[number];

export interface IModelField {
  key: string;
  label: string;
  value: unknown;
  hide: DisplayMode;
  control: InputType;
  render: (mode: string) => RenderFunction | null;
}

export abstract class ModelBase<T extends IModel = IModel> implements IModel {
  [modelMarker]: never = null as never;

  abstract fromResponse(model: T): boolean;

  abstract toRequest(): T;

  abstract equals(mb: ModelBase): boolean;

  abstract get key(): string | number | symbol;

  abstract clone(): ModelBase;

  get title(): IModelField {
    const self = this as unknown as Record<string | symbol, string>;
    const title = self[titleProp];
    return {
      key: title,
      label: (self[descriptonPropkey(title)] as string) ?? 'Название',
      value: self[title],
      hide: 'always',
      control: 'text',
      render: () => null,
    };
  }

  get fields(): IModelField[] {
    const self = this as unknown as Record<symbol | string, unknown>;
    const title = self[titleProp];

    const result = Object.keys(this)
      .filter((key) => key !== title)
      .map(
        (key): IModelField => ({
          label: (this.getValue(descriptonPropkey(key)) as string) ?? key,
          value: this.getValue(key),
          hide: this.getValue<DisplayMode, symbol>(hiddenPropkey(key)),
          key,
          control: ModelBase.checkType(this, key),
          render: (mode = 'default') => ModelBase.renderField(this, key, mode),
        }),
      );
    return result;
  }

  static checkType(target: ModelBase, key: string): InputType {
    const self = target as unknown as Record<symbol | string, unknown>;
    return typeof self[key] === 'number' ? 'number' : 'text';
  }

  get vuelidation() {
    const rules = Object.keys(this)
      .map((key) => (this as unknown as Record<symbol, unknown>)[validationPropkey(key)])
      .filter((vRule): vRule is ValidationRule | ValidationRuleCollection => vRule != null);
    const v$: Ref<Validation<ValidationArgs<unknown>, ModelBase<IModel>>> = useVuelidate<ModelBase>(
      { ...rules },
      this,
    );
    return v$;
  }

  getValue<TVal = unknown, TKey extends string | symbol = string>(key: TKey): TVal {
    return (this as unknown as Record<TKey, TVal>)[key];
  }

  setKey<TVal = unknown, TKey extends string | symbol = string>(key: TKey, value: TVal) {
    (this as unknown as Record<TKey, TVal>)[key] = value;
  }

  mock() {
    const mocked = this.clone();
    Object.keys(mocked).forEach((key) => {
      const mockFn = mocked.getValue<() => unknown | undefined, symbol>(mockPropKey(key));
      if (mockFn == null) {
        throw new Error("Model can't be mocked!");
      }

      mocked.setKey(key, mockFn());
    });
    return mocked;
  }

  mockMany(capacity: number) {
    return [...new Array(capacity)].map(() => this.mock());
  }

  static renderField(
    target: ModelBase,
    key: string,
    mode: DisplayMode = 'view',
  ): RenderFunction | null {
    const renderModeProp = renderPropkey(key, mode);
    const renderAlwaysProp = renderPropkey(key, 'always');
    const renderFunction =
      target.getValue<((m: ModelBase) => RenderFunction | undefined) | undefined, symbol>(
        renderModeProp,
      ) ??
      target.getValue<((m: ModelBase) => RenderFunction | undefined) | undefined, symbol>(
        renderAlwaysProp,
      );
    const render = renderFunction ? renderFunction(target) : undefined;
    return render ?? null;
  }
}
