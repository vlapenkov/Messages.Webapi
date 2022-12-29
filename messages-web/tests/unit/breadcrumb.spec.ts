import { treeToList } from '@/services/breadcrumb.service';
import type { ITreeNode } from '@/store/breadcrumb.store';
import { RouteLocation } from 'vue-router';

describe('breadcrumbs', () => {
  test('simple breadcrumb', () => {
    const foon = jest.fn(() => 'hello');
    foon();
    expect(foon).toBeCalled();
  });
  test('---', () => {
    const tree: ITreeNode = {
      label: () => 'foo',
      route: () => ({ path: '/foo' } as RouteLocation),
      children: [
        {
          label: () => 'bar',
          route: () => ({ path: 'bar' } as RouteLocation),
        },
        {
          label: () => 'baz',
          route: () => ({ path: 'baz' } as RouteLocation),
          children: [
            {
              label: () => 'bazzer',
              route: () => ({ path: 'bazzer' } as RouteLocation),
            },
            {
              label: () => 'bazzer2',
              route: () => ({ path: 'bazzer2' } as RouteLocation),
            },
          ],
        },
      ],
    };
    const list = treeToList(tree);
    console.log(list);
    const [{ parentId }, ...rest] = list;
    expect(parentId).toBeNull();
    expect(rest.every((i) => i.parentId != null)).toBe(true);
  });
});
