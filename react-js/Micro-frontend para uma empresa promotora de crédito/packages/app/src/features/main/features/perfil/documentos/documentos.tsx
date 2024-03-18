import { FC } from 'react';

import { Flex, useBreakpointValue } from '@chakra-ui/react';

import { PageLayout, Loader } from '@pcf/design-system';
import { useNavigatePathUp } from 'hooks';

import { DocumentInfo, DocumentInfoProps } from './document-info';
import { DocumentsGuidelines } from './components/upload-guidelines';
import { useUploadIdentity } from './components/upload-identity';
import { useUploadProofOfResidence } from './components/upload-proof-of-residence';
// import { useUploadPaycheck } from './components/upload-paycheck';
import { useUploadLiveness } from './components/upload-liveness';
import { useDocumentos } from './use-documentos';

export const Documentos: FC = () => {
  const isLargeDesktop = useBreakpointValue({ base: false, lg: true }, 'base');
  const navigateUp = useNavigatePathUp();

  const uploadIdentityDialog = useUploadIdentity();
  // const uploadPaycheck = useUploadPaycheck();
  const uploadProofOfResidence = useUploadProofOfResidence();
  const uploadLiveness = useUploadLiveness();
  const { isLoading, anexosResidencia, anexosIdentidade, selfies } =
    useDocumentos();

  const documentTypes: DocumentInfoProps[] = [
    {
      title: 'Documento de Identificação',
      attachFunc: uploadIdentityDialog,
      notes: `${anexosIdentidade?.length || 1}ª via`,

      attachment: anexosIdentidade[0],
      showType: true,
    },
    // {
    //   title: 'Contracheque (Holerite)',
    //   status: DocumentStatus.pending,
    //   notes: '1 via',
    //   attachFunc: uploadPaycheck,
    // },
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
    <PageLayout>
      <PageLayout.Header>
        <PageLayout.BackButton onClick={navigateUp} />
        <PageLayout.Title>Meus Documentos</PageLayout.Title>
      </PageLayout.Header>

      <Flex height="100%" overflow="hidden">
        <PageLayout.Content paddingX={!isLargeDesktop ? undefined : '32px'}>
          {isLoading ? (
            <Loader height="100%" />
          ) : (
            documentTypes.map((doc) => (
              <DocumentInfo key={doc.title} {...doc} />
            ))
          )}
        </PageLayout.Content>

        {isLargeDesktop && <DocumentsGuidelines marginLeft="70px" flex="0.4" />}
      </Flex>
    </PageLayout>
  );
};
