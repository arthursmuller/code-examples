import { FC } from 'react';

import { useStepsContainerContext, enumKeyFor } from '@pcf/design-system';
import {
  PassaporteIcon,
  RgAbertoIcon,
  RgBackIcon,
  RgFrontIcon,
} from '@pcf/design-system-icons';

import { IdentityType } from './identity-types.enum';
import { UploadIdentityForm } from './models/upload-identity-form.model';

import {
  DocumentGuidelinesWithCameraStep,
  DocumentGuidelinesWithCameraStepProps,
} from '../shared-steps';

const content: {
  [key in keyof typeof IdentityType]: DocumentGuidelinesWithCameraStepProps;
} = {
  RG: {
    icon: RgFrontIcon,
    title: 'Vamos tirar a foto da FRENTE do seu RG.',
    cameraInputTitle: 'Tirar foto da frente do RG',
    info: 'Para tirar uma foto com qualidade, se conseguir, retire seu documento do plástico de proteção.',
  },
  CNH: {
    icon: RgAbertoIcon,
    title: 'Tire foto da sua CNH aberta',
    cameraInputTitle: 'Tirar foto da CNH',
    info: 'Para tirar uma foto com qualidade, se conseguir, retire seu documento do plástico de proteção. Verifique também se sua CNH está dentro do prazo de validade.',
  },
  Passport: {
    icon: PassaporteIcon,
    title: 'Tire foto das páginas de identificação do seu Passaporte',
    cameraInputTitle: 'Tirar foto do Passaporte',
    info: 'Por favor, verifique também se seu Passaporte está dentro do prazo de validade.',
  },
  RGBack: {
    icon: RgBackIcon,
    title: 'Agora, vamos tirar a foto do VERSO do seu RG.',
    cameraInputTitle: 'Tirar foto do verso do RG',
    info: 'Só mais este passo e pronto!',
    guideline:
      'Posicione a TRASEIRA INTEIRA do seu documento dentro da moldura',
  },
};

export const IdentityGuidelinesStep: FC = () => {
  const { data } = useStepsContainerContext<UploadIdentityForm>();

  const key = enumKeyFor(IdentityType, data.documentType as string) || '';

  return <DocumentGuidelinesWithCameraStep {...content[key]} />;
};

export const IdentityGuidelineExtraStep: FC = () => {
  const { nextStep, data } = useStepsContainerContext<UploadIdentityForm>();

  const handleSubmit = (file: File): void => {
    nextStep({ files: [data.files[0], file] });
  };

  return (
    <DocumentGuidelinesWithCameraStep
      {...content.RGBack}
      onSubmit={handleSubmit}
    />
  );
};
