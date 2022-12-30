import { defineStore } from '@/app/core/services/harlem/harlem.service';
import { productFullStore } from '@/app/product-full/state/product-full.store';
import { sectionsStore } from '@/app/sections/state/sections.store';
import { breadcrumbService } from '@/services/breadcrumb.service';
import { RouteLocationNamedRaw, RouteLocationPathRaw } from 'vue-router';
import { catalogFiltersStore } from './catalog-filters.store';

type RouteLocationStateBase = RouteLocationNamedRaw & RouteLocationPathRaw;
export type RouteLocationState = Partial<RouteLocationStateBase> &
  Required<Pick<RouteLocationStateBase, 'name'>>;

export interface ITreeNode {
  children?: ITreeNode[];
  label: () => string | undefined;
  route: () => RouteLocationState;
}

export interface IListNode {
  id: symbol;
  parentId: symbol | null;
  label: () => string | undefined;
  route: () => RouteLocationState;
}

export interface IBreadcrumbStore {
  list: IListNode[];
}

const { sections } = sectionsStore;
const { sectionName, sectionId } = catalogFiltersStore;
const { product } = productFullStore;
const tree: ITreeNode = {
  label: () => 'Главная',
  route: () => ({ name: 'home' }),
  children: [
    {
      label: () => 'Каталог',
      route: () => ({ name: 'catalog' }),
      children: [
        {
          label: () =>
            sectionName.value ??
            sections.value?.find((x) => x.id === product.value.catalogSectionId)?.name,
          route: () => ({
            name: 'catalog',
            query: {
              sectionId: sectionId.value?.toString() ?? product.value.catalogSectionId.toString(),
            },
          }),
          children: [
            {
              label: () => product.value.name,
              route: () => ({
                name: 'product',
                params: { id: product.value.id.toString() },
              }),
            },
            {
              label: () => product.value.name,
              route: () => ({
                name: 'product-service',
                params: { id: product.value.id.toString() },
              }),
            },
            {
              label: () => product.value.name,
              route: () => ({
                name: 'product-work',
                params: { id: product.value.id.toString() },
              }),
            },
          ],
        },
      ],
    },
  ],
};

const defaultState: IBreadcrumbStore = {
  list: breadcrumbService.treeToList(tree),
};

const { getter } = defineStore('breadcrumb', defaultState);

const list = getter('get-list', (state) => state.list);

const breadcrumbItemsByPath = (loc: RouteLocationState) =>
  getter<readonly IListNode[]>(`get-breadcrumb-items--${loc.name.toString()}`, (state) => {
    const root = state.list.find((x) => breadcrumbService.isRoutesEquals(x.route(), loc));
    if (root == null) {
      throw new Error('Не удалось найти элемент breadcrumb по заданному пути');
    }
    const res = [root];
    let node = root;
    while (node.parentId != null) {
      // eslint-disable-next-line no-loop-func
      const parent = state.list.find((x) => x.id === node.parentId);
      if (parent == null) {
        throw new Error('Не удалось найти элемент breadcrumb по id родителя');
      }
      node = parent;
      res.push(node);
    }
    return res.reverse();
  });

export const breadcrumbStore = {
  list,
  breadcrumbItemsByPath,
};
