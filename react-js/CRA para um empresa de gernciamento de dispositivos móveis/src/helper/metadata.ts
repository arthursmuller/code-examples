import { ListMetadata } from "../types/generic_list";

export const newMetadata = (metadata?: ListMetadata) => ({
  sortingProperty: 'id',
  ...metadata,
});