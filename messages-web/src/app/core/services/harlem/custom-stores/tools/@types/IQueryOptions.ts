// eslint-disable-next-line @typescript-eslint/no-explicit-any
export interface IQueryOtions<TArg = any> {
  /** Выполнять запрос элементов всегда, даже если элементы уже загружены */
  force: boolean;
  arguments?: TArg;
}
