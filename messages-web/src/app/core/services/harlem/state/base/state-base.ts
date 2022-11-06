export const stateMarker = Symbol.for('--state-marker');

export abstract class StateBase {
  [stateMarker] = true;
}
