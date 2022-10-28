import { ValidationRuleCollection, ValidationRule } from '@vuelidate/core';
import { ModelBase } from '../base/model-base';

export const validate =
  (rules: ValidationRuleCollection | ValidationRule) =>
  <T extends ModelBase>(target: T, key: string) => {
    Object.defineProperty(target, Symbol(`--validation--${key}`), {
      get: () => rules,
    });
  };
