import { useState, useCallback } from 'react';

import { useDebounce } from 'react-use';
import { IFuzzyClient, useFuzzy } from 'react-use-fuzzy';

interface UseDebounceSearchData<T> extends Omit<IFuzzyClient<T>, 'search'> {
  isLoading: boolean;
  setIsLoading: (value: boolean) => void;
  onSearch: (value: string) => void;
  searchText: string;
}

export function useDebounceSearch<T>(
  data: T[],
  options: any,
): UseDebounceSearchData<T> {
  const [searchText, setSearchText] = useState('');
  const [isLoading, setIsLoading] = useState(false);
  const { result, keyword, search, resetSearch } = useFuzzy<T>(data, options);

  const handleSearch = useCallback(
    (value: string) => {
      if (!isLoading && !!value) {
        setIsLoading(true);
      }

      setSearchText(value);
    },
    [isLoading, setIsLoading, setSearchText],
  );

  const handleResetSearch = useCallback(() => {
    setSearchText('');
    resetSearch();
  }, [resetSearch]);

  const handleSearchDebounced = useCallback(() => {
    setIsLoading(false);
    search(searchText);
  }, [search, searchText]);

  useDebounce(handleSearchDebounced, 500, [searchText]);

  return {
    result,
    keyword,
    setIsLoading,
    isLoading,
    onSearch: handleSearch,
    resetSearch: handleResetSearch,
    searchText,
  };
}
