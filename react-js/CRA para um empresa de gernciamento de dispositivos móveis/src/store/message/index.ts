import { createSlice, PayloadAction } from '@reduxjs/toolkit';

import { sanitizeFilter } from '../../helper/filter';
import { newMetadata } from '../../helper/metadata';
import {
  ListMetadata,
  ListQueryParameters,
  PaginationPayload,
} from '../../types/generic_list';
import { MessageType, MessageDetailsType } from '../../types/message';
import { ID } from '../../types/util';

// Action Types

export const Types = {
  CREATE: 'message/CREATE',
  LIST: 'message/LIST',
  GET: 'message/GET',
  UPDATE_TOASTER: 'message/UPDATE_TOASTER',
  LIST_DETAILS: 'message/LIST_DETAILS',
};

// Reducer

interface MessagesState {
  messages: MessageType[];
  message: MessageType;
  messagesDetail: MessageDetailsType[];
  metadata: ListMetadata;
  metadataDetails: ListMetadata;
  errors: Error;
  toaster: boolean;
}

const initialState: MessagesState = {
  message: {},
  messages: [],
  messagesDetail: [],
  metadata: newMetadata(),
  metadataDetails: newMetadata(),
  errors: null,
  toaster: false,
};

export const messagesSlice = createSlice({
  name: 'messages',
  initialState,
  reducers: {
    messageError: (state, action: PayloadAction<Error>) => {
      state.errors = action.payload;
      state.toaster = true;
    },
    messageToaster: (state, action: PayloadAction<boolean>) => {
      state.toaster = action.payload;
    },
    messagePagination: (state, action: PayloadAction<PaginationPayload>) => {
      state.metadata = { ...state.metadata, ...action.payload };
    },
    messagePaginationDetails: (state, action: PayloadAction<PaginationPayload>) => {
      state.metadataDetails = { ...state.metadata, ...action.payload };
    },
    messageSelected: (state, action: PayloadAction<Partial<MessageType>>) => {
      state.message = { ...state.message, ...action.payload };
    },
    messageSelectedClear: (state) => {
      state.message = initialState.message;
    },
    messageListSuccess: (state, action: PayloadAction<MessageType[]>) => {
      state.messages = action.payload;
    },
    messageListFilterSuccess: (state, action: PayloadAction<MessageType[]>) => {
      state.messages = action.payload;
    },
    messageCreateSuccess: (state, action: PayloadAction<MessageType>) => {
      state.message = action.payload;
      state.errors = initialState.errors;
      state.toaster = true;
    },
    messageGetSuccess: (state, action: PayloadAction<MessageType>) => {
      state.message = action.payload;
    },
    messageDetailListSuccess: (
      state,
      action: PayloadAction<MessageDetailsType[]>
    ) => {
      state.messagesDetail = action.payload;
    },
  },
});
export default messagesSlice.reducer;

// Action Creators

export const {
  messageError,
  messageToaster,
  messagePagination,
  messagePaginationDetails,
  messageSelected,
  messageSelectedClear,
  messageListSuccess,
  messageListFilterSuccess,
  messageCreateSuccess,
  messageGetSuccess,
  messageDetailListSuccess,
} = messagesSlice.actions;

export function listMessages(
  queryParameters: ListQueryParameters,
  filters?: Record<string, unknown>
) {
  return {
    type: Types.LIST,
    payload: { queryParameters, filters: sanitizeFilter(filters) },
  };
}

export function getMessage(id: number) {
  return {
    type: Types.GET,
    payload: id,
  };
}

export function createMessage(data: MessageType) {
  return {
    type: Types.CREATE,
    payload: data,
  };
}

export function listMessageDetails(
  id: ID,
  queryParameters: ListQueryParameters,
  filters?: Record<string, unknown>
) {
  return {
    type: Types.LIST_DETAILS,
    payload: { queryParameters, filters: sanitizeFilter(filters), id },
  };
}
