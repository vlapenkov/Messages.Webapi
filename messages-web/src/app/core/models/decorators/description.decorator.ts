import { metadataKey } from '../base/metadata-key';
import { ModelBase } from '../base/model-base';
import { createMetadata } from './tools/createMetadata';

export const description =
  (descriptionValue: string) =>
  <T extends ModelBase>(target: T, key: string) => {
    createMetadata(target, key);
    target[metadataKey][key].description = descriptionValue;
  };
