import { ValidationRuleCollection, ValidationRule } from '@vuelidate/core';
import type { ModelBase } from '../base/model-base';
import { validationProp } from '../base/props/validation.prop';

export const validate =
  (rules: ValidationRuleCollection | ValidationRule) =>
  <T extends ModelBase>(target: T, key: string) => {
    Object.defineProperty(target, validationProp(key), {
      get: () => rules,
    });
  };
