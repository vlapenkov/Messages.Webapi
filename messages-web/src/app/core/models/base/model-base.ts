import useVuelidate, {
  ValidationRule,
  ValidationRuleCollection,
  Validation,
  ValidationArgs,
} from '@vuelidate/core';
import { Ref, RenderFunction, toRaw } from 'vue';
import { IModel, modelMarker } from '../@types/IModel';
import { descriptonPropkey } from './props-keys/descripton.prop-key';
import { titleProp } from '../decorators/tittle.decorator';
import { validationPropkey } from './props-keys/validation.prop-key';
import { hiddenPropkey } from './props-keys/hidden.prop-key';
import { renderPropkey } from './props-keys/render.prop-key';
import { HiddenValue } from '../decorators/HiddenValue';

export const inputTypes = ['text', 'number'] as const;

export type InputType = typeof inputTypes[number];

export interface IModelField {
  key: string;
  label: string;
  value: unknown;
  hide: HiddenValue;
  control: InputType;
  render: (mode: string) => RenderFunction | null;
}

export abstract class ModelBase<T extends IModel = IModel> implements IModel {
  [modelMarker]: never = null as never;

  abstract tryParse(model: T): boolean;

  abstract asObject(): T;

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
          label: (self[descriptonPropkey(key)] as string) ?? key,
          value: self[key],
          hide: self[hiddenPropkey(key)] as HiddenValue,
          key,
          control: ModelBase.checkType(this, key),
          render: (mode = 'default') => ModelBase.renderField(this, key, mode),
        }),
      );
    console.log('fields for', toRaw(this), 'are', result);
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

  getKey<TVal = unknown, TKey extends string | symbol = string>(key: TKey): TVal {
    return (this as unknown as Record<TKey, TVal>)[key];
  }

  setKey<TVal = unknown, TKey extends string | symbol = string>(key: TKey, value: TVal) {
    (this as unknown as Record<TKey, TVal>)[key] = value;
  }

  static renderField(target: ModelBase, key: string, mode = 'default'): RenderFunction | null {
    const rp = renderPropkey(key, mode);
    const rf = target.getKey<((m: ModelBase) => RenderFunction | undefined) | undefined, symbol>(
      rp,
    );
    const render = rf ? rf(target) : undefined;
    return render ?? null;
  }
}
