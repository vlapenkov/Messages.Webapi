import { IModel, modelMarker } from '../@types/IModel';
import { IModelFieldMetadata } from '../@types/IModelFieldMetadata';
import { titleProp } from '../decorators/tittle.decorator';

export const metadataKey = Symbol('--model-props-metadata');

export abstract class ModelBase<T extends IModel = IModel> implements IModel {
  [modelMarker]: never = null as never;

  [titleProp]: string | undefined;

  [metadataKey]: Record<string, IModelFieldMetadata> = {};

  constructor() {
    Object.keys(this).forEach((key) => {
      this[metadataKey][key] = {
        description: key,
        visible: true,
      };
    });
  }

  abstract tryParse(model: T): boolean;

  abstract asObject(): T;

  abstract equalsDeep(mb: ModelBase): boolean;
}
