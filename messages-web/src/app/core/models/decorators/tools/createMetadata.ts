import { metadataKey } from '../../base/metadata-key';
import type { ModelBase } from '../../base/model-base';

export const createMetadata = <T extends ModelBase>(target: T, key: string) => {
  if (target[metadataKey] == null) {
    target[metadataKey] = {};
  }
  if (target[metadataKey][key] == null) {
    target[metadataKey][key] = {
      visible: true,
      description: key,
    };
  }
};
