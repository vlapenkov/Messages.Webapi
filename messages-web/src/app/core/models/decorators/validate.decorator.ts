import { ValidationRuleCollection, ValidationRule } from '@vuelidate/core';
import type { ModelBase } from '../base/model-base';
import { validationPropkey } from '../base/props-keys/validation.prop-key';

export const validate =
  (rules: ValidationRuleCollection | ValidationRule) =>
  <T extends ModelBase>(target: T, key: string) => {
    Object.defineProperty(target, validationPropkey(key), {
      get: () => rules,
    });
  };
