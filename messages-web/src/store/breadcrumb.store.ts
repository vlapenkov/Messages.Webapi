import { defineStore } from '@/app/core/services/harlem/harlem.service';
import { productFullStore } from '@/app/product-full/state/product-full.store';
import { sectionsStore } from '@/app/sections/state/sections.store';
import { treeToList } from '@/services/breadcrumb.service';

export interface ILocation {
  name?: string;
  path?: string;
  params?: Record<string | number, unknown>;
}

export interface ITreeNode {
  label: () => string | undefined;
  route: ILocation;
  children?: ITreeNode[];
}

export interface IListNode {
  id: symbol;
  parentId: symbol | null;
  label: () => string | undefined;
  route: ILocation;
}

export interface IBreadcrumbStore {
  list: IListNode[];
}

const { sections } = sectionsStore;
const { product } = productFullStore;
const tree: ITreeNode = {
  label: () => 'Главная',
  route: { name: 'home' },
  children: [
    {
      label: () => 'Каталог',
      route: { name: 'catalog' },
      children: [
        {
          label: () => sections.value?.find((x) => x.id === product.value.catalogSectionId)?.name,
          route: { name: 'catalog', params: { sectionId: product.value.catalogSectionId } },
          children: [
            {
              label: () => product.value.name,
              route: { name: 'product' },
            },
            {
              label: () => product.value.name,
              route: { name: 'product-service' },
            },
            {
              label: () => product.value.name,
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

const breadcrumbItemsByPath = (loc: ILocation) =>
  getter(`get-test--${loc.name}`, (state) => {
    const root = state.list.find((x) => x.route.name === loc.name);
    if (root == null) {
      throw new Error('Не удалось найти элемент breadcrumb по заданному пути');
    }
    const res: IListNode[] = [root];
    let node: IListNode = root;
    while (node.parentId != null) {
      // eslint-disable-next-line no-loop-func
      const parent = state.list.find((x) => x.id === node.parentId);
      if (parent != null) {
        node = parent;
        res.push(node);
      }
    }
    return res.reverse();
  });

export const breadcrumbStore = {
  list,
  breadcrumbItemsByPath,
};
