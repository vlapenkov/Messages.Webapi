import { ModelBase } from '../model/model-base';

export interface IMethodConfig {
  url: string;
}

export interface IRepositoryDefinitionOptional {
  url: string;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface, @typescript-eslint/no-unused-vars
export interface IRepositoryDefinition<TModel extends ModelBase> {}

const defaultOptionalProps: IRepositoryDefinitionOptional = {
  url: '',
};

export function defineRepository<TModel extends ModelBase>(
  optionsProvided: IRepositoryDefinition<TModel> & Partial<IRepositoryDefinitionOptional>,
) {
  const options: IRepositoryDefinition<TModel> & IRepositoryDefinitionOptional = {
    ...defaultOptionalProps,
    ...optionsProvided,
  };

  return options;
}
