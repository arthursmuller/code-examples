import { useMemo } from 'react';

const range = (start, end) => {
  const length = end - start + 1;
  return Array.from({ length }, (_, idx) => idx + start);
};

export const usePagination = ({ totalCount, currentPage, pageSize }) => {
  const paginationRange = useMemo(() => {
    const totalPageCount = Math.ceil(totalCount / pageSize);
    const pageInGroup = 5;
    const currentGroup = Math.ceil(currentPage / pageInGroup);
    const lastGroup = Math.ceil(totalPageCount / pageInGroup);

    if (pageInGroup >= totalPageCount) {
      return { paginationRange: range(1, totalPageCount), totalPageCount };
    }
    if (currentGroup !== lastGroup) {
      return {
        paginationRange: [
          ...range(
            pageInGroup * (currentGroup - 1) + 1,
            pageInGroup * currentGroup
          ),
          '...',
          totalPageCount,
        ],
        totalPageCount,
      };
    }
    if (currentGroup === lastGroup) {
      return {
        paginationRange: [
          1,
          '...',
          ...range(pageInGroup * (currentGroup - 1) + 1, totalPageCount),
        ],
        totalPageCount,
      };
    }
  }, [totalCount, currentPage, pageSize]);

  return paginationRange;
};
