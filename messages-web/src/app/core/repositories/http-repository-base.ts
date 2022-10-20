export interface IMethodConfig {
  url: string;
}

export interface IRepositoryDefinition {
  urlSuffix: string;
  setup: () => void;
}

export function defineRepository(options: IRepositoryDefinition) {
  options.setup();
  throw new Error('Not Implemented!');
}
