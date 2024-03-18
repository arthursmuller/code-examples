import { FC } from 'react';

import {
  useDocumentos,
  useUploadLiveness,
  useUploadProofOfResidence,
} from 'features/main/features/perfil/documentos';
import {
  DocumentInfo,
  DocumentInfoProps,
} from 'features/main/features/perfil/documentos/document-info';

import { BaseLayoutStep } from './base-step-layout';

export const AnexosObrigatorios: FC = () => {
  const { isLoading, anexosResidencia, selfies } = useDocumentos();
  const uploadProofOfResidence = useUploadProofOfResidence();
  const uploadLiveness = useUploadLiveness();

  const documentTypes: DocumentInfoProps[] = [
    {
      title: 'Comprovante de residência',
      attachFunc: uploadProofOfResidence,
      notes: `${anexosResidencia?.length || 1}ª via`,

      attachment: anexosResidencia[0],
    },
    {
      title: 'Selfie',
      notes: 'Video-selfie',
      attachFunc: uploadLiveness,
      buttonLabel: 'Tirar selfie',
      attachment: selfies[0],
    },
  ];

  return (
    <BaseLayoutStep isLoading={isLoading}>
      {documentTypes.map((doc) => (
        <DocumentInfo key={doc.title} {...doc} />
      ))}

      <BaseLayoutStep.Footer />
    </BaseLayoutStep>
  );
};
