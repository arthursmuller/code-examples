import { FC } from 'react';

import {
  useDocumentos,
  useUploadIdentity,
} from 'features/main/features/perfil/documentos';
import { DocumentInfo } from 'features/main/features/perfil/documentos/document-info';

import { BaseLayoutStep } from './base-step-layout';

export const DocumentoPessoal: FC = () => {
  const { anexosIdentidade, isLoading } = useDocumentos();
  const uploadIdentityDialog = useUploadIdentity();

  const hasError = !anexosIdentidade?.length;
  const errorMessage =
    'É necessário incluir um documento pessoal para continuar.';

  const doc = {
    title: 'Documento de Identificação',
    attachFunc: uploadIdentityDialog,
    notes: `${anexosIdentidade?.length || 1}ª via`,

    attachment: anexosIdentidade[0],
    showType: true,
  };

  return (
    <BaseLayoutStep isLoading={isLoading}>
      <DocumentInfo showTitle={false} {...doc} />

      <BaseLayoutStep.Footer hasErrors={hasError} errorMessage={errorMessage} />
    </BaseLayoutStep>
  );
};
