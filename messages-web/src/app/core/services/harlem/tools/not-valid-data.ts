/* eslint-disable max-classes-per-file */
export type DataMode = 'create' | 'edit' | 'moderate';

export class NotValidData<T> {
  constructor(public data: T, public mode: DataMode) {}
}

export class Creation<T> extends NotValidData<T> {
  constructor(data: T) {
    super(data, 'create');
  }
}

export class Edititng<T> extends NotValidData<T> {
  constructor(data: T) {
    super(data, 'edit');
  }
}

export class Moderation<T> extends NotValidData<T> {
  constructor(data: T) {
    super(data, 'moderate');
  }
}
