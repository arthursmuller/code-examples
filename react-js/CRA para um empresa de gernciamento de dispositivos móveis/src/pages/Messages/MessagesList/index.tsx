import { Box, FormLabel } from '@chakra-ui/react';
import React, { useState, useRef, useMemo, useEffect } from 'react';
import { FormattedMessage, useIntl } from 'react-intl';
import { useParams, useRouteMatch } from 'react-router-dom';
import SimpleReactValidator from 'simple-react-validator';

import DatePicker from '../../../components/Datepicker';
import FormContainer from '../../../components/FormContainer';
import FormControl from '../../../components/FormControl';
import PageActions from '../../../components/PageActions';
import PageFilter from '../../../components/PageFilter';
import PagePagination from '../../../components/PagePagination';
import PageTitle from '../../../components/PageTitle';
import PageToaster from '../../../components/PageToaster';
import TableComponent from '../../../components/Table/Table';
import TableActions from '../../../components/TableActions';
import { routeWithParameters } from '../../../helper';
import { getMode, ModeObject } from '../../../helper/mode';
import { useSorting } from '../../../helper/sort';
import { validatorDefaultMessages } from '../../../helper/validador';
import { useAppDispatch, useAppSelector } from '../../../hooks/useRedux';
import routes from '../../../routes';
import { documentToaster, listDocument } from '../../../store/document';
import { messageToaster, listMessages } from '../../../store/message/index';
import { ListMetadata } from '../../../types/generic_list';
import { MessageTypeFilter } from '../../../types/message';

const MessagesList = () => {
  const dispatch = useAppDispatch();
  const isMessages = !!useRouteMatch(routes.messages.list);
  const { id } = useParams<{ id: string }>();

  const {
    messages,
    errors: messageErrors,
    metadata: messageMetadata,
    toaster: messageShowToaster,
  } = useAppSelector((state) => state.message);
  const {
    documents,
    errors: documentErrors,
    metadata: documentMetadata,
    toaster: documentShowToaster,
  } = useAppSelector((state) => state.document);
  const [filterMessage, setFilterMessage] = useState<MessageTypeFilter>({});
  const [showToaster, setShowToaster] = useState(false);

  const list = isMessages ? messages : documents;
  const errors = isMessages ? messageErrors : documentErrors;
  const metadata = isMessages ? messageMetadata : documentMetadata;
  const toaster = isMessages ? messageShowToaster : documentShowToaster;
  const listEntity = isMessages ? listMessages : listDocument;
  const toasterClear = isMessages ? messageToaster : documentToaster;
  const linkNew = (isMessages ? routes.messages : routes.documents).register;
  const linkView = (isMessages ? routes.messages : routes.documents).details;

  const keysIntl = {
    entity: isMessages ? 'message.entity' : 'document.entity',
    title: isMessages ? 'message.title' : 'document.title',
    description: isMessages ? 'message.description' : 'document.description',
    start_date: isMessages ? 'message.start_date' : 'document.start_date',
    end_date: isMessages ? 'message.end_date' : 'document.end_date',
    toaster: isMessages
      ? 'message.toaster_success'
      : 'document.toaster_success',
    column_date: isMessages ? 'message.column.date' : 'document.column.date',
    column_message: isMessages
      ? 'message.column.message'
      : 'document.column.message',
    new: isMessages ? 'message.new' : 'document.new',
  };

  const handlePagination = (newPagination: Partial<ListMetadata>) => {
    dispatch(listEntity({ ...metadata, ...newPagination }));
  };

  const filterClean = useMemo(() => {
    return {
      startAt: filterMessage?.startAt,
      endAt: filterMessage?.endAt,
      search: filterMessage?.search,
    };
  }, [filterMessage]);

  const intl = useIntl();

  useEffect(() => {
    if (toaster) {
      setShowToaster(true);
      dispatch(toasterClear(false));
    }
  }, [toaster]);

  const handlePeriodFilterChange = (date: Date, field: string) => {
    setFilterMessage({ ...filterMessage, [field]: date });
  };

  const handleMetadata = (newMetadata: Partial<ListMetadata>) => {
    dispatch(listEntity({ ...metadata, ...newMetadata }, filterClean));
  };

  const handleSecundary = () => {
    dispatch(listEntity(metadata, filterClean));
  };

  const simpleValidator = useRef(
    new SimpleReactValidator({
      messages: {
        ...validatorDefaultMessages(intl),
      },
    })
  );
  const CRUDMode = getMode(id);

  const columns = useSorting(
    [
      {
        header: intl.formatMessage({
          id: keysIntl.column_date,
          defaultMessage: 'Data',
        }),
        accessor: 'date',
        canSort: true,
      },
      {
        header: intl.formatMessage({
          id: keysIntl.column_message,
          defaultMessage: 'Mensagem',
        }),
        accessor: 'message',
        canSort: true,
      },
    ],
    metadata
  );
  const data = list.map((message) => ({
    cells: [
      {
        field: 'date',
        value: `${intl.formatDate(message.createdAt)} ${intl.formatTime(
          message.createdAt
        )}`,
      },
      {
        field: 'message',
        value: message.content,
      },
      {
        field: 'actions',
        value: '',
        transform: () => {
          return (
            <TableActions
              entityIntlLabel={<FormattedMessage id={keysIntl.entity} />}
              linkView={routeWithParameters(linkView, {
                id: message?.id,
              })}
            />
          );
        },
      },
    ],
  }));

  return (
    <>
      <Box>
        <PageTitle
          title={<FormattedMessage id={keysIntl.title} />}
          description={<FormattedMessage id={keysIntl.description} />}
        />
        <FormContainer
          labelPrimary={
            CRUDMode === ModeObject.CREATE ? (
              <FormattedMessage id="global.register" />
            ) : (
              <FormattedMessage id="global.update" />
            )
          }
          disabledPrimary={!simpleValidator.current.allValid()}
          labelSecundary={<FormattedMessage id="global.cancel" />}
          labelFilter={<FormattedMessage id="global.search" />}
          handleFilter={handleSecundary}
        >
          <PageFilter>
            <FormControl w="176px" mr="24px">
              <FormLabel fontSize="sm" color="gray.500">
                <FormattedMessage id={keysIntl.start_date} />
              </FormLabel>
              <DatePicker
                selected={filterMessage.startAt}
                onChange={(e) => {
                  handlePeriodFilterChange(e, 'startAt');
                }}
              />
            </FormControl>
            <FormControl w="176px">
              <FormLabel fontSize="sm" color="gray.500">
                <FormattedMessage id={keysIntl.end_date} />
              </FormLabel>
              <DatePicker
                selected={filterMessage.endAt}
                onChange={(e) => {
                  handlePeriodFilterChange(e, 'endAt');
                }}
              />
            </FormControl>
          </PageFilter>
        </FormContainer>
        <PageToaster
          message={
            errors?.message || <FormattedMessage id={keysIntl.toaster} />
          }
          onClose={() => setShowToaster(false)}
          showToaster={showToaster}
          type={errors ? 'error' : 'success'}
        />
      </Box>

      <PageActions
        linkNew={linkNew}
        labelButtonNew={<FormattedMessage id={keysIntl.new} />}
        onSearch={(e) => setFilterMessage({ ...filterMessage, search: e })}
      />
      <Box w="90%">
        <TableComponent
          headerColumns={columns}
          rows={data}
          handleSort={handleMetadata}
        />
      </Box>
      <PagePagination pagination={metadata} onPageChange={handlePagination} />
    </>
  );
};

export default MessagesList;
