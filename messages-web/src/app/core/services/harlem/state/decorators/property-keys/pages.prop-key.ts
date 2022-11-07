import { StateBase } from '../../base/state-base';

export const pagesPropKey = Symbol('--state-pages');

export const getPagesProp = <T extends StateBase>(target: T) => target[pagesPropKey as keyof T];
