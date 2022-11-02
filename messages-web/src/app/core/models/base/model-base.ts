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

  abstract equalsDeep(mb: ModelBase): boolean;

  abstract get key(): string | number | symbol;

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
    return Object.keys(this)
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

  static renderField(target: ModelBase, key: string, mode = 'default'): RenderFunction | null {
    const self = target as unknown as Record<symbol | string, unknown>;
    const rp = renderPropkey(key, mode);
    const rf = self[rp] as ((m: ModelBase) => RenderFunction | undefined) | undefined;
    const render = rf ? rf(target) : undefined;
    return render ?? null;
  }
}
