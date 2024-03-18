import { FC } from 'react';

import { Button, Flex, Icon, Text } from '@chakra-ui/react';

import {
  DefaultFormatStrings,
  formatDate,
  CustomHeading,
  enumKeyFor,
} from '@pcf/design-system';
import {
  AttentionIcon,
  // ClockIcon,
  CheckIcon,
  StatusCloseErrorIcon,
  VisibilityShowIcon,
} from '@pcf/design-system-icons';
import { Anexo } from '@pcf/core';

import { useDeleteAttachment } from './components/use-delete-attachment';
import { InfoLine } from './components';
import { DocumentStatus } from './models/document-status.enum';

const statusColors: { [key in keyof typeof DocumentStatus]: string } = {
  approved: 'success.regular',
  pending: 'grey.600',
  rejected: 'error.regular',
  notRegistered: 'grey.600',
};

const statusIcons: { [key in keyof typeof DocumentStatus]: FC } = {
  approved: CheckIcon,
  pending: AttentionIcon,
  rejected: StatusCloseErrorIcon,
  notRegistered: AttentionIcon,
};

export interface DocumentInfoProps {
  title: string;
  notes: string;
  showType?: boolean;
  showTitle?: boolean;
  attachment: Anexo;

  attachFunc?: () => void;
  buttonLabel?: string;
}

export const DocumentInfo: FC<DocumentInfoProps> = ({
  title,
  notes,
  attachFunc,
  buttonLabel = 'Anexar documento',
  showType,
  showTitle = true,
  attachment,
}) => {
  const status = attachment
    ? DocumentStatus.approved
    : DocumentStatus.notRegistered;

  const statusKey = enumKeyFor(DocumentStatus, status) || '';

  const confirmDelete = useDeleteAttachment(attachment?.id || -1);

  return (
    <Flex
      layerStyle="card"
      padding="16px"
      marginBottom="32px"
      direction="column"
    >
      {showTitle && (
        <CustomHeading
          as="h3"
          textStyle="bold20"
          color="secondary.mid-dark"
          marginBottom="8px"
        >
          {title}
        </CustomHeading>
      )}

      <InfoLine
        label="Situação"
        custom={
          <Flex align="center" color={statusColors[statusKey]}>
            <Icon as={statusIcons[statusKey]} marginRight="8px" />
            <Text as="p" textStyle="bold16" color={statusColors[statusKey]}>
              {status}
            </Text>
          </Flex>
        }
      />

      {showType && attachment && (
        <InfoLine
          label="Tipo de documento"
          value={attachment.tipoDocumento.nome}
        />
      )}

      <InfoLine label="Observações" value={notes} />

      <InfoLine
        label="Data de cadastro"
        value={
          !attachment
            ? '00/00/0000'
            : formatDate(
                new Date(attachment.dataCadastro),
                DefaultFormatStrings.input,
              )
        }
      />

      {status === DocumentStatus.approved && (
        <Flex justifyContent="space-between" marginTop="8px">
          <Button
            variant="link"
            leftIcon={<StatusCloseErrorIcon height="8px" width="8px" />}
            size="sm"
            colorScheme="secondary"
            onClick={confirmDelete}
          >
            Excluir
          </Button>
          <Button
            as="a"
            variant="link"
            leftIcon={<VisibilityShowIcon height="16px" width="16px" />}
            size="sm"
            colorScheme="secondary"
            href={attachment.linkAnexo}
            target="_blank"
          >
            Visualizar
          </Button>
        </Flex>
      )}

      {status !== DocumentStatus.approved && (
        <Button marginTop="8px" colorScheme="secondary" onClick={attachFunc}>
          {buttonLabel}
        </Button>
      )}

      {/* {status === DocumentStatus.rejected && (
        <Button
          marginTop="16px"
          variant="link"
          leftIcon={<ClockIcon height="16px" width="16px" />}
          size="sm"
          colorScheme="error"
        >
          Ver os detalhes da reprovação
        </Button>
      )} */}
    </Flex>
  );
};
