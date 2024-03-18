import { Box, Divider } from '@chakra-ui/react';
import React, { useEffect, useState } from 'react';
import { FormattedMessage, useIntl } from 'react-intl';
import { useHistory, useParams, useRouteMatch } from 'react-router-dom';

import FormContainer from '../../../components/FormContainer';
import FormSubtitle from '../../../components/FormSubtitle';
import PageActions from '../../../components/PageActions';
import PagePagination from '../../../components/PagePagination';
import PageTitle from '../../../components/PageTitle';
import TableComponent from '../../../components/Table/Table';
import Text from '../../../components/Text';
import { MessageStatusToIntl } from '../../../helper/messageStatus';
import { useSorting } from '../../../helper/sort';
import { useAppDispatch, useAppSelector } from '../../../hooks/useRedux';
import routes from '../../../routes';
import { getDocument, listDocumentDetails } from '../../../store/document';
import {
  getMessage,
  listMessageDetails,
  messageSelectedClear,
} from '../../../store/message/index';
import { ListMetadata } from '../../../types/generic_list';

const MessageDetails = () => {
  const history = useHistory();
  const dispatch = useAppDispatch();
  const isMessages = !!useRouteMatch(routes.messages.details);

  const {
    messagesDetail,
    message,
    metadataDetails: metadataMessageDetails,
  } = useAppSelector((state) => state.message);
  const {
    documentsDetail,
    document,
    metadataDetails: metadataDocumentDetails,
  } = useAppSelector((state) => state.document);
  const { id } = useParams<{ id: string }>();

  const [search, setSearch] = useState('');

  const entity = isMessages ? message : document;
  const entityDetails = isMessages ? messagesDetail : documentsDetail;
  const metadata = isMessages
    ? metadataMessageDetails
    : metadataDocumentDetails;
  const listDetails = isMessages ? listMessageDetails : listDocumentDetails;
  const getEntity = isMessages ? getMessage : getDocument;
  const clearEntity = isMessages ? messageSelectedClear : messageSelectedClear;
  const linkList = (isMessages ? routes.messages : routes.documents).list;

  const keysIntl = {
    title: isMessages ? 'message_detail.title' : 'document_details.title',
    description: isMessages
      ? 'message_detail.title_text'
      : 'document_details.description',
    subtitle: isMessages
      ? 'message_detail.subtitle'
      : 'document_details.subtitle',
    messageTitle: isMessages ? 'global.message' : 'document_details.doc',
  };

  const handleInputSearch = (value) => {
    setSearch(value);
  };

  const handlePagination = (metadataNew: ListMetadata) => {
    dispatch(
      listDetails(parseInt(id), { ...metadata, ...metadataNew }, { search })
    );
  };

  const intl = useIntl();

  useEffect(() => {
    dispatch(listDetails(parseInt(id), metadata, { search }));
  }, [search]);

  useEffect(() => {
    if (id) {
      dispatch(getEntity(parseInt(id)));
    }
    return () => {
      dispatch(clearEntity());
    };
  }, [id]);

  const columns = React.useMemo(
    () =>
      useSorting(
        [
          {
            header: intl.formatMessage({
              id: 'message_details.column.data.user',
              defaultMessage: 'Usuário',
            }),
            accessor: 'name',
            canSort: true,
          },
          {
            header: intl.formatMessage({
              id: 'message_details.column.data.phone',
              defaultMessage: 'Telefone',
            }),
            accessor: 'phoneNumber',
            canSort: true,
          },
          {
            header: intl.formatMessage({
              id: 'message_details.column.data.sent_at',
              defaultMessage: 'Enviado em',
            }),
            accessor: 'sentAt',
            canSort: true,
          },
          {
            header: intl.formatMessage({
              id: 'message_details.column.data.status',
              defaultMessage: 'Estado',
            }),
            accessor: 'status',
            canSort: true,
          },
        ],
        metadata
      ),
    []
  );

  const data = entityDetails.map((details) => ({
    cells: [
      {
        field: 'name',
        value: details.deviceUser?.name,
      },
      {
        field: 'phoneNumber',
        value: details.deviceUser?.device?.phoneNumber,
      },
      {
        field: 'sentAt',
        value: `${intl.formatDate(details.sentAt)} ${intl.formatTime(
          details.sentAt
        )}`,
      },
      {
        field: 'status',
        value:
          details.status !== undefined &&
          intl.formatMessage({ id: MessageStatusToIntl[details.status] }),
      },
    ],
  }));
  return (
    <>
      <PageTitle
        title={<FormattedMessage id={keysIntl.title} />}
        description={<FormattedMessage id={keysIntl.description} />}
      />
      <FormContainer
        labelSecundary={<FormattedMessage id="global.back" />}
        handleSecundary={() => history.push(linkList)}
      >
        <Text
          margin="0 0 20px 0"
          color="gray.500"
          fontSize="md"
          fontWeight="600"
        >
          <FormattedMessage id={keysIntl.messageTitle} />
        </Text>
        <Box>
          <Divider orientation="horizontal" mb="1%" />
        </Box>
        <Box d="flex" flexDirection="row" alignItems="center">
          <Text m="0" fontSize="4xl" color="gray.600">
            &quot;
          </Text>
          <Text m="0">{entity.content}</Text>
          <Text m="0" fontSize="4xl" color="gray.600">
            &quot;
          </Text>
        </Box>
      </FormContainer>

      <Box mt="3%">
        <FormSubtitle
          subtitle={<FormattedMessage id={keysIntl.subtitle} />}
        ></FormSubtitle>
      </Box>
      <PageActions initialSearch={search} onSearch={handleInputSearch} />

      <Box w="90%">
        <TableComponent
          headerColumns={columns}
          rows={data}
          handleSort={handlePagination}
        />
      </Box>

      <PagePagination onPageChange={handlePagination} pagination={metadata} />
    </>
  );
};

export default MessageDetails;
