import useVuelidate, {
  ValidationRule,
  ValidationRuleCollection,
  Validation,
  ValidationArgs,
} from '@vuelidate/core';
import { Ref } from 'vue';
import { IModel, modelMarker } from '../@types/IModel';
import { descriptonProp } from './props/descripton.prop';
import { titleProp } from '../decorators/tittle.decorator';
import { validationProp } from './props/validation.prop';
import { hiddenProp } from './props/hidden.prop';

export const inputTypes = ['text', 'number'] as const;

export type InputType = typeof inputTypes[number];

export interface IModelField {
  key: string;
  label: string;
  value: unknown;
  visible: boolean;
  control: InputType;
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
      label: (self[descriptonProp(title)] as string) ?? 'Название',
      value: self[title],
      visible: false,
      control: 'text',
    };
  }

  get fields(): IModelField[] {
    const self = this as unknown as Record<symbol | string, unknown>;
    const title = self[titleProp];
    return Object.keys(this)
      .filter((key) => key !== title)
      .map(
        (key): IModelField => ({
          label: (self[descriptonProp(key)] as string) ?? key,
          value: self[key],
          visible: self[hiddenProp(key)] !== true,
          key,
          control: ModelBase.checkType(this, key),
        }),
      );
  }

  static checkType(target: ModelBase, key: string): InputType {
    const self = target as unknown as Record<symbol | string, unknown>;
    return typeof self[key] === 'number' ? 'number' : 'text';
  }

  get vuelidation() {
    const rules = Object.keys(this)
      .map((key) => (this as unknown as Record<symbol, unknown>)[validationProp(key)])
      .filter((vRule): vRule is ValidationRule | ValidationRuleCollection => vRule != null);
    const v$: Ref<Validation<ValidationArgs<unknown>, ModelBase<IModel>>> = useVuelidate<ModelBase>(
      { ...rules },
      this,
    );
    return v$;
  }
}
