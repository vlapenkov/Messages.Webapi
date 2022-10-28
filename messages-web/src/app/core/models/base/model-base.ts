import useVuelidate, {
  Validation,
  ValidationArgs,
  ValidationRule,
  ValidationRuleCollection,
} from '@vuelidate/core';
import { Ref } from 'vue';
import { IModel, modelMarker } from '../@types/IModel';
import { IModelFieldMetadata } from '../@types/IModelFieldMetadata';
import { titleProp } from '../decorators/tittle.decorator';
import { createMetadata } from '../decorators/tools/createMetadata';
import { metadataKey } from './metadata-key';

export abstract class ModelBase<T extends IModel = IModel> implements IModel {
  [modelMarker]: never = null as never;

  [titleProp]: string | undefined;

  [metadataKey]: Record<string, IModelFieldMetadata> = {};

  constructor() {
    Object.keys(this).forEach((key) => {
      createMetadata(this, key);
    });
  }

  abstract tryParse(model: T): boolean;

  abstract asObject(): T;

  abstract equalsDeep(mb: ModelBase): boolean;

  vuelidate() {
    const rules = Object.keys(this)
      .map((key) => this[metadataKey][key].validationRules)
      .filter((vRule): vRule is ValidationRule | ValidationRuleCollection => vRule != null);
    const v$: Ref<Validation<ValidationArgs<unknown>, ModelBase<IModel>>> = useVuelidate<ModelBase>(
      { ...rules },
      this,
    );
    return v$;
  }
}
