import { sanitizeFilter } from './filter';

describe('Helper sanitizeFilter', () => {
  test('Should exclude attributes with values undefined', () => {
    expect(sanitizeFilter({})).toEqual({});
    expect(sanitizeFilter({
      test: undefined,
      test2: null,
      test3: '',
      test4: 0,
    })).toEqual({});
    expect(sanitizeFilter({ test: 1 })).toEqual({ test: 1 });
  });
});
