import { useMemo } from 'react';

import { Anexo, DocumentTypeCode, useAnexosQuery } from '@pcf/core';

interface UseDocumentosData {
  anexosResidencia: Anexo[];
  anexosIdentidade: Anexo[];
  anexosFotoPessoal: Anexo[];
  selfies: Anexo[];
  isLoading: boolean;
}

export function useDocumentos(): UseDocumentosData {
  const { data: attachments, isLoading } = useAnexosQuery();

  const [anexosResidencia, anexosIdentidade, selfies, anexosFotoPessoal] =
    useMemo(() => {
      const residence: Anexo[] = [];
      const identity: Anexo[] = [];
      const selfie: Anexo[] = [];
      const fotosPessoais: Anexo[] = [];

      const sortedAttachments =
        attachments
          ?.sort(
            (a, b) =>
              new Date(a.dataCadastro).getTime() -
              new Date(b.dataCadastro).getTime(),
          )
          .reverse() || [];

      sortedAttachments.forEach((anexo: Anexo) => {
        switch (anexo.tipoDocumento.id) {
          case DocumentTypeCode.ComprovanteDeResidencia:
            residence.push(anexo);
            break;
          case DocumentTypeCode.RegistroIdentidadeCivil:
          case DocumentTypeCode.CarteiraNacionalDeHabilitacao:
          case DocumentTypeCode.PassaporteBrasileiro:
            identity.push(anexo);
            break;
          case DocumentTypeCode.Selfie:
            selfie.push(anexo);
            break;
          case DocumentTypeCode.FotoPessoal:
            fotosPessoais.push(anexo);
            break;
          default:
        }
      });
      return [residence, identity, selfie, fotosPessoais];
    }, [attachments]);

  return {
    anexosResidencia,
    anexosIdentidade,
    selfies,
    isLoading,
    anexosFotoPessoal,
  };
}
