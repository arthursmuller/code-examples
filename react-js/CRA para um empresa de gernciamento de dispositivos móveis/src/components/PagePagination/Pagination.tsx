import { ChevronLeftIcon, ChevronRightIcon } from '@chakra-ui/icons';
import { HStack, Button } from '@chakra-ui/react';
import React from 'react';

import { usePagination } from './usePagination';

interface PaginationProps {
  onPageChange: (page: number) => void;
  totalCount: number;
  currentPage: number;
  pageSize: number;
}

const Pagination = ({ onPageChange, totalCount, currentPage, pageSize }: PaginationProps) => {
  const { paginationRange, totalPageCount } = usePagination({
    currentPage,
    totalCount,
    pageSize,
  });

  if (currentPage === 0 || paginationRange.length < 2) {
    return null;
  }

  const onNext = () => {
    onPageChange(Number(currentPage) + 1);
  };

  const onPrevious = () => {
    onPageChange(currentPage - 1);
  };

  return (
    <HStack mt={4} spacing="24px" width="full" justify="center">
      <Button disabled={Number(currentPage) === 1} onClick={onPrevious}>
        <ChevronLeftIcon />
      </Button>
      {paginationRange.map((pageNumber, idx) => {
        if (pageNumber === '...') {
          return <Button key={`${idx}-${pageNumber}`}>...</Button>;
        }

        return (
          <Button
            key={`${idx}-${pageNumber}`}
            bg={pageNumber === Number(currentPage) ? '' : 'default'}
            onClick={() => onPageChange(pageNumber)}
            disabled={pageNumber === Number(currentPage)}
          >
            {pageNumber}
          </Button>
        );
      })}
      <Button disabled={totalPageCount === Number(currentPage)} onClick={onNext}>
        <ChevronRightIcon />
      </Button>
    </HStack>
  );
};

export default Pagination;
