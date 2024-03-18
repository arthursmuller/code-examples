import { toggleCheckbox } from './';

describe('Helper toggleCheckbox', () => {
  test('When list not exists must be added item to list', () => {
    const list = undefined;
    expect(toggleCheckbox(list, 1)).toEqual([1]);
    expect(toggleCheckbox(list, 'Element List')).toEqual(['Element List']);
    expect(toggleCheckbox(list, -1)).toEqual([-1]);
  });

  test('When list exists and item is new, the item must be added to the end of the list.', () => {
    const list1 = [];
    const list2 = [1, 2, 3];
    const list3 = ['tes', 'tes2', 'tes3'];

    expect(toggleCheckbox(list1, 4)).toEqual([4]);
    expect(toggleCheckbox(list1, 'test')).toEqual(['test']);
    expect(toggleCheckbox(list1, undefined)).toEqual([undefined]);

    expect(toggleCheckbox(list2, 4)).toEqual([...list2, 4]);
    expect(toggleCheckbox(list2, -1)).toEqual([...list2, -1]);

    expect(toggleCheckbox(list3, 'Element')).toEqual([...list3, 'Element']);
    expect(toggleCheckbox(list3, '')).toEqual([...list3, '']);
  });

  test('When list exists and item not is new, the item must be removed of the list.', () => {
    const list2 = [1, 2, 3, 4];
    const list3 = ['tes1', 'tes2', 'tes3'];

    expect(toggleCheckbox(list2, 1)).toEqual([2, 3, 4]);
    expect(toggleCheckbox(list2, 2)).toEqual([1, 3, 4]);
    expect(toggleCheckbox(list2, 4)).toEqual([1, 2, 3]);

    expect(toggleCheckbox(list3, 'tes1')).toEqual(['tes2', 'tes3']);
    expect(toggleCheckbox(list3, 'tes2')).toEqual(['tes1', 'tes3']);
    expect(toggleCheckbox(list3, 'tes3')).toEqual(['tes1', 'tes2']);
  });
});
