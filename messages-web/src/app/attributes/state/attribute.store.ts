import { defineCollectionStore } from '@/app/core/services/harlem/custom-stores/collection/collection.store';
import { attributeHttpService } from '../infrastructure/attribute.http-service';
import { AttributeModel } from '../models/AttributeModel';
import { AttributeState } from './attribute.state';

const [attributeStore] = defineCollectionStore(
  'attribute',
  AttributeModel,
  AttributeState,
  attributeHttpService,
);

export { attributeStore };
