export interface IMethodConfig {
  url: string;
}

export interface IRepositoryDefinition {
  prefix: string;
  methods: Record<string, IMethodConfig>;
}

export function defineRepository(_arg: IRepositoryDefinition) {
  throw new Error('Not Implemented!');
}
