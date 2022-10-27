import { metadataKey, ModelBase } from '../base/model-base';

export const hidden = <T extends ModelBase>(target: T, key: string) => {
  target[metadataKey][key].visible = false;
};
