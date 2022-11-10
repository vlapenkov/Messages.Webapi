import { Handler } from '@/app/core/handlers/base/handler';
import { StateBase } from '../../../state/base/state-base';
import { StoreCreationContext } from '../context';

export type StoreCreationPipe<TState extends StateBase> = Handler<
  StoreCreationContext<TState>,
  StoreCreationContext<TState>
>;
