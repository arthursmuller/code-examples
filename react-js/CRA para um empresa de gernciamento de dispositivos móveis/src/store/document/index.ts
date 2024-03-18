import { createSlice, PayloadAction } from '@reduxjs/toolkit';

import { sanitizeFilter } from '../../helper/filter';
import { newMetadata } from '../../helper/metadata';
import { DocumentType, DocumentDetailsType } from '../../types/document';
import {
  ListMetadata,
  ListQueryParameters,
  PaginationPayload,
} from '../../types/generic_list';
import { ID } from '../../types/util';

// Action Types

export const Types = {
  CREATE: 'document/CREATE',
  TOASTER: 'document/TOASTER',
  LIST: 'document/LIST',
  GET: 'document/GET',
  LIST_DETAILS: 'document/LIST_DETAILS',
};

// Reducer

interface DocumentsState {
  documents: DocumentType[];
  document: DocumentType;
  documentsDetail: DocumentDetailsType[];
  metadata: ListMetadata;
  metadataDetails: ListMetadata;
  errors: Error;
  toaster: boolean;
}

const initialState: DocumentsState = {
  document: {},
  documents: [],
  documentsDetail: [],
  metadata: newMetadata(),
  metadataDetails: newMetadata(),
  errors: null,
  toaster: false,
};

export const documentsSlice = createSlice({
  name: 'documents',
  initialState,
  reducers: {
    documentError: (state, action: PayloadAction<Error>) => {
      state.errors = action.payload;
      state.toaster = true;
    },
    documentToaster: (state, action: PayloadAction<boolean>) => {
      state.toaster = action.payload;
    },
    documentPagination: (state, action: PayloadAction<PaginationPayload>) => {
      state.metadata = { ...state.metadata, ...action.payload };
    },
    documentPaginationDetails: (state, action: PayloadAction<PaginationPayload>) => {
      state.metadataDetails = { ...state.metadata, ...action.payload };
    },
    documentSelected: (state, action: PayloadAction<Partial<DocumentType>>) => {
      state.document = { ...state.document, ...action.payload };
    },
    documentSelectedClear: (state) => {
      state.document = initialState.document;
    },
    documentListSuccess: (state, action: PayloadAction<DocumentType[]>) => {
      state.documents = action.payload;
    },
    documentListFilterSuccess: (state, action: PayloadAction<DocumentType[]>) => {
      state.documents = action.payload;
    },
    documentCreateSuccess: (state, action: PayloadAction<DocumentType>) => {
      state.document = action.payload;
      state.errors = initialState.errors;
      state.toaster = true;
    },
    documentGetSuccess: (state, action: PayloadAction<DocumentType>) => {
      state.document = action.payload;
    },
    documentDetailListSuccess: (
      state,
      action: PayloadAction<DocumentDetailsType[]>
    ) => {
      state.documentsDetail = action.payload;
    },
  },
});
export default documentsSlice.reducer;

// Action Creators

export const {
  documentError,
  documentToaster,
  documentPagination,
  documentPaginationDetails,
  documentSelected,
  documentSelectedClear,
  documentListSuccess,
  documentListFilterSuccess,
  documentCreateSuccess,
  documentGetSuccess,
  documentDetailListSuccess,
} = documentsSlice.actions;

export function listDocument(
  queryParameters: ListQueryParameters,
  filters?: Record<string, unknown>
) {
  return {
    type: Types.LIST,
    payload: { queryParameters, filters: sanitizeFilter(filters) },
  };
}

export function getDocument(id: number) {
  return {
    type: Types.GET,
    payload: id,
  };
}

export function createDocument(data: DocumentType) {
  return {
    type: Types.CREATE,
    payload: data,
  };
}

export function listDocumentDetails(
  id: ID,
  queryParameters: ListQueryParameters,
  filters?: Record<string, unknown>
) {
  return {
    type: Types.LIST_DETAILS,
    payload: { queryParameters, filters: sanitizeFilter(filters), id },
  };
}
