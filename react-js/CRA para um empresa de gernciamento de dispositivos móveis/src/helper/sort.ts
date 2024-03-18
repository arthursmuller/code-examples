import { Header } from "../components/Table/TableInterfaces";
import { SortingQueryParameters } from "../types/generic_list";

export const useSorting = (
  column: Header[],
  sortingParameters: SortingQueryParameters
) => {
  return column.map((cell) => ({
    ...cell,
    isSorted: sortingParameters.sortingProperty === cell.accessor,
    isSortedDesc: sortingParameters.sortingDirection === 'DESC',
  }));
};
