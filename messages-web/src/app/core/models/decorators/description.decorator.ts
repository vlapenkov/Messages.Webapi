import { metadataKey, ModelBase } from '../base/model-base';

export const description =
  (descriptionValue: string) =>
  <T extends ModelBase>(target: T, key: string) => {
    target[metadataKey][key].description = descriptionValue;
  };
