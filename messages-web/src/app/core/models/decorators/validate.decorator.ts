import { ValidationRuleCollection, ValidationRule } from '@vuelidate/core';
import { metadataKey } from '../base/metadata-key';
import { ModelBase } from '../base/model-base';
import { createMetadata } from './tools/createMetadata';

export const validate =
  (rules: ValidationRuleCollection | ValidationRule) =>
  <T extends ModelBase>(target: T, key: string) => {
    createMetadata(target, key);
    target[metadataKey][key].validationRules = rules;
  };
