import { breadcrumbService } from '@/services/breadcrumb.service';
import type { ITreeNode } from '@/store/breadcrumb.store';

describe('breadcrumbs', () => {
  test('simple breadcrumb', () => {
    const foon = jest.fn(() => 'hello');
    foon();
    expect(foon).toBeCalled();
  });
  test('---', () => {
    const tree: ITreeNode = {
      label: () => 'foo',
      route: () => ({ path: '/foo', name: 'foo' }),
      children: [
        {
          label: () => 'bar',
          route: () => ({ path: 'bar', name: 'bar' }),
        },
        {
          label: () => 'baz',
          route: () => ({ path: 'baz', name: 'baz' }),
          children: [
            {
              label: () => 'bazzer',
              route: () => ({ path: 'bazzer', name: 'bazzer' }),
            },
            {
              label: () => 'bazzer2',
              route: () => ({ path: 'bazzer2', name: 'bazzer2' }),
            },
          ],
        },
      ],
    };
    const list = breadcrumbService.treeToList(tree);
    console.log(list);
    const [{ parentId }, ...rest] = list;
    expect(parentId).toBeNull();
    expect(rest.every((i) => i.parentId != null)).toBe(true);
    expect(rest.every((i) => i.parentId !== i.id)).toBe(true);
  });
});
