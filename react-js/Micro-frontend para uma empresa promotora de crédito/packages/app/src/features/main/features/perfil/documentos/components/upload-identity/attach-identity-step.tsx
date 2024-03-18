import { FC } from 'react';

import { useStepsContainerContext, enumKeyFor } from '@pcf/design-system';

import { IdentityType } from './identity-types.enum';
import { UploadIdentityForm } from './models/upload-identity-form.model';

import { AttachFilesStep } from '../shared-steps/attach-files-step';

const content: {
  [key in keyof typeof IdentityType]: { info: string; maxFiles: number };
} = {
  RG: {
    info: 'Arraste os arquivos da FRENTE e do VERSO do seu documento para cá',
    maxFiles: 2,
  },
  CNH: {
    info: 'Arraste um ÚNICO arquivo contendo a FRENTE e o VERSO do seu documento para cá',
    maxFiles: 1,
  },
  Passport: {
    info: 'Arraste um ÚNICO arquivo contendo as Páginas de Identificação do seu documento para cá',
    maxFiles: 1,
  },
  RGBack: {
    info: '',
    maxFiles: 0,
  },
};

export const AttachIdentityStep: FC = () => {
  const { data } = useStepsContainerContext<UploadIdentityForm>();
  const key = enumKeyFor(IdentityType, data.documentType as string) || '';

  return <AttachFilesStep {...content[key]} />;
};
