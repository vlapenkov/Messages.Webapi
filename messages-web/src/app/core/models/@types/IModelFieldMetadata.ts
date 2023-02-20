import { ValidationRule, ValidationRuleCollection } from '@vuelidate/core';

export interface IModelFieldMetadata {
  description: string;
  visible: boolean;
  validationRules?: ValidationRuleCollection | ValidationRule;
}
