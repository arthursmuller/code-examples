import { Story } from '@storybook/react';

import { StepStatus } from './step-status.enum';
import { StepsProgressBar, StepsProgressBarProps } from './steps-progress-bar';

export default {
  title: 'Steps Progress Bar',
  component: StepsProgressBar,
};

const Template: Story<StepsProgressBarProps> = (args) => {
  return <StepsProgressBar {...args} />;
};

export const Simple = Template.bind({});
Simple.args = {
  items: [
    {
      label: 'Solicitação',
      status: StepStatus.active,
    },
    {
      label: 'Análise',
      status: StepStatus.inactive,
    },
    {
      label: 'Ass. Digital',
      status: StepStatus.success,
    },
    {
      label: 'Concluída',
      status: StepStatus.error,
    },
  ],
  isHorizontal: true,
  size: 'md',
  showLabels: true,
};

export const History = Template.bind({});
History.args = {
  items: [
    {
      label: 'Solicitação Aprovada',
      status: StepStatus.active,
      description:
        'Sua Solicitação foi Aprovada e o crédito estará disponível em 24h.',
    },
    {
      label: 'Assinatura Digital',
      status: StepStatus.active,
      description: 'Você cadastrou com sucesso sua Assinatura Digital.',
    },
    {
      label: 'Reprovação automática',
      status: StepStatus.error,
      description:
        'Enfrentamamos problemas ao verificar o seu comprovante de residência. Solicitamos que envie novamente.',
    },
    {
      label: 'Reenvio de documentos',
      description:
        'Você enviou novamente o seu contracheque (holerite). O documento foi aprovado.',
      status: StepStatus.success,
    },
  ],
  isHorizontal: false,
  size: 'sm',
  showLabels: true,
};
