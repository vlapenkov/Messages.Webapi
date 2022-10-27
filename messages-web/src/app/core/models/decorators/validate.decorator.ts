import { ValidationRuleCollection, ValidationRule } from '@vuelidate/core';
import { metadataKey, ModelBase } from '../base/model-base';

export const validate =
  (rules: ValidationRuleCollection | ValidationRule) =>
  <T extends ModelBase>(target: T, key: string) => {
    target[metadataKey][key].validationRules = rules;
  };
