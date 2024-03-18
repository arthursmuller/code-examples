
interface SortingQueryParameters {
  sortingProperty?: string;
  sortingDirection?: 'ASC' | 'DESC';
}
interface PaginationQueryParameters {
  page?: number;
  pageSize?: number;
}

export interface ListQueryParameters extends SortingQueryParameters, PaginationQueryParameters {}

export interface PaginationPayload extends PaginationQueryParameters {
  totalPages?: number,
  totalItems?: number,
}

export interface ListMetadata extends PaginationPayload, ListQueryParameters {

}

export interface ListPayload <T> extends PaginationPayload {
  items: T[];
}

export interface QuerysWithFilters {
  queryParameters: ListQueryParameters;
  filters: Record<string, unknown>;
}
