import { defineStore } from '@/app/core/services/harlem/harlem.service';
import { sectionsStore } from '@/app/sections/state/sections.store';
import { treeToList } from '@/services/breadcrumb.service';
import { RouteLocation, RouteLocationRaw } from 'vue-router';
import { catalogFiltersStore } from './catalog-filters.store';

export interface ITreeNode {
  label: () => string | undefined;
  route: RouteLocationRaw | RouteLocation;
  children?: ITreeNode[];
}

export interface IListNode {
  id: symbol;
  parentId: symbol | null;
  label: () => string | undefined;
  route: RouteLocationRaw | RouteLocation;
}

export interface IBreadcrumbStore {
  list: IListNode[];
}

const { sections } = sectionsStore;
const { sectionId } = catalogFiltersStore;
const tree: ITreeNode = {
  label: () => 'Главная',
  route: { name: 'home' },
  children: [
    {
      label: () => 'Каталог',
      route: { name: 'catalog' },
      children: [
        {
          label: () => sections.value?.find((x) => x.id === sectionId.value)?.name,
          route: { name: 'catalog', params: { sectionId: sectionId.value } },
          children: [
            {
              label: () => 'Товар',
              route: { name: 'product' },
            },
            {
              label: () => 'Услуга',
              route: { name: 'product-service' },
            },
            {
              label: () => 'Работа',
              route: { name: 'product-work' },
            },
          ],
        },
      ],
    },
  ],
};

const defaultState: IBreadcrumbStore = {
  list: treeToList(tree),
};

const { getter } = defineStore('breadcrumb', defaultState);

const list = getter('get-list', (state) => state.list);

const getByPathTest = getter('get-test', (state) => {
  const name = 'catalog';
  return state.list.find((x) => (x.route as RouteLocation).name === name);
});

export const breadcrumbStore = {
  list,
  getByPathTest,
};
