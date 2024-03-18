import { FC } from 'react';

import { useMount } from 'react-use';
import { Flex } from '@chakra-ui/react';
import { useErrorHandler } from 'react-error-boundary';
import { useQueryClient } from 'react-query';

import {
  CustomHeading,
  Loader,
  useModal,
  useStepsContainerContext,
  BemErrorBoundary,
} from '@pcf/design-system';
import {
  AnexoCriacaoModel,
  ANEXOS_QUERY_ENDPOINT,
  DocumentTypeCode,
  useGravarAnexos,
  toBase64,
  base64WithoutContentType,
} from '@pcf/core';

import { UploadDocumentForm } from './models/upload-document-form.model';

interface UploadDocumentProps {
  documentTypeCode: DocumentTypeCode;
  successMessage?: string;
}

export const UploadDocument: FC<UploadDocumentProps> = ({
  documentTypeCode,
  successMessage = 'Pronto! Cadastramos seu documento com sucesso!',
}) => {
  const { finish, data } = useStepsContainerContext<UploadDocumentForm>();
  const { showModal } = useModal();
  const queryCache = useQueryClient();

  const { mutate: uploadAttachment } = useGravarAnexos();
  const handleError = useErrorHandler();

  useMount(async () => {
    const onFinish = (): void => {
      queryCache.invalidateQueries(ANEXOS_QUERY_ENDPOINT);

      finish();
      showModal({
        closeOnClickOverlay: false,
        title: successMessage,
      });
    };

    const files = data?.files;

    const requests: AnexoCriacaoModel[] = await Promise.all(
      // TODO: UPDATE typescript
      // https://stackoverflow.com/questions/58772314/typescript-array-prototype-map-has-error-expression-is-not-callable-when-th
      (files as any[]).map(async (file) => ({
        anexoBase64:
          typeof file === 'string'
            ? base64WithoutContentType(file)
            : await toBase64(file),
        extensao: typeof file === 'string' ? 'jpg' : file.type.split('/').pop(),
        idTipoDocumento: documentTypeCode,
      })),
    );

    uploadAttachment(requests, {
      onSuccess: onFinish,
      onError: handleError,
    });
  });

  return (
    <Flex
      flex={1}
      width="100%"
      flexDirection="column"
      justifyContent="center"
      alignItems="center"
    >
      <Loader size="lg" marginY={7} />

      <CustomHeading
        as="h2"
        textStyle="bold24_32"
        color="secondary.mid-dark"
        textAlign="center"
      >
        Estamos fazendo o upload do seu documento
      </CustomHeading>
    </Flex>
  );
};

export const UploadDocumentStepWrapped: FC<UploadDocumentProps> = (props) => {
  return (
    <BemErrorBoundary>
      <UploadDocument {...props} />
    </BemErrorBoundary>
  );
};

export { UploadDocumentStepWrapped as UploadDocumentStep };
