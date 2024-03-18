import { zIndexes } from '../../../../consts/z-indexes.enum';
import { Offsets } from './use-calculate-offset';

export const selectSearchStyles = (
  hasValue: boolean,
  hasError: boolean,
  hasOptions: boolean,
  { bottom, top, height, width }: Offsets,
): any => ({
  backgroundColor: hasError ? 'error.washed' : 'inherit',
  borderRadius: '4px',

  input: {
    textOverflow: 'ellipsis',
    color: 'inherit',
    '&::placeholder': {
      color: 'grey.400',
    },
  },

  '.select-search': {
    flex: 1,
  },
  '.select-search__select': {
    display: hasOptions ? 'inherit' : 'none',
    marginTop: '16px',

    position: 'fixed',
    zIndex: zIndexes.menu + 1,
    bottom: bottom < 150 ? bottom + 16 : 'unset',
    top: bottom < 150 ? 'unset' : top + height,
    right: 'unset',

    width: `${width}px`,
    maxHeight: height
      ? `${bottom < 150 ? 150 : bottom - 16 - 50}px`
      : 'fit-content',
    overflow: 'auto',

    backgroundColor: 'white',
    boxShadow: 'medium',
    borderRadius: '4px',
    paddingY: '8px',

    color: 'grey.700',

    ul: {
      listStyle: 'none',

      'li button': {
        width: '100%',
        padding: '12px',

        outline: 'none',
        textAlign: 'start',

        overflow: 'hidden',
        whiteSpace: 'nowrap',
        textOverflow: 'ellipsis',
        fontWeight: '900',

        '&.is-selected': {
          backgroundColor: hasValue ? 'grey.200' : 'white',
          color: hasValue ? 'primary.light' : 'grey.800',
        },

        '&.is-highlighted': {
          color: 'primary.light',
        },
        '&:hover': {
          color: 'primary.light',
        },
      },
    },
  },
});
