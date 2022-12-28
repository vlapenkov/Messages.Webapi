import { defineStore } from '@/app/core/services/harlem/harlem.service';
import { sectionsStore } from '@/app/sections/state/sections.store';
// eslint-disable-next-line @typescript-eslint/no-unused-vars
import { ref, Ref } from 'vue';
import { RouteLocationRaw } from 'vue-router';

export interface ITreeNode {
  label: () => string | undefined;
  route: RouteLocationRaw;
  children?: ITreeNode[];
}

export interface IListNode {
  id: string;
  parentId: string;
  label?: string;
  route: RouteLocationRaw;
}

export interface IBreadcrumbStore {
  tree: ITreeNode;
}

// eslint-disable-next-line @typescript-eslint/no-unused-vars
const { sections } = sectionsStore;
const defaultState: IBreadcrumbStore = {
  tree: {
    label: () => 'Главная',
    route: { name: 'home' },
    children: [
      {
        label: () => 'Продукция',
        route: { name: 'catalog' },
      },
    ],
  },
};

const { getter } = defineStore('breadcrumb', defaultState);

const tree = getter('get-tree', (state) => state.tree);

const list = getter('get-list', (state) => {
  const res: ITreeNode[] = [];
  const stack = [state.tree];
  console.log(stack);

  while (stack.length !== 0) {
    const node: ITreeNode = { ...stack.pop() } as ITreeNode;
    if (node?.children !== null) {
      node?.children?.forEach((x) => {
        stack.push(x);
      });
    } else {
      res.push(node);
    }
  }
  return res;
});

const breadcrumb = (path: string) => {
  getter<ITreeNode[]>(`get-breadcrumb-${path}`, (state) => {
    const res: ITreeNode[] = [];

    console.log(state.tree);
    const stack = [state.tree];
    console.log(stack, state.tree);

    while (stack.length !== 0) {
      const node: ITreeNode = { ...stack.pop() } as ITreeNode;
      if (node?.children !== null) {
        node?.children?.forEach((x) => {
          stack.push(x);
        });
      } else {
        res.push(node);
      }
    }

    return res;
  });
};

export const breadcrumbStore = {
  tree,
  list,
  breadcrumb,
};
