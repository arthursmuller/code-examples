import { FC, useState } from 'react';

import { useUpdateEffect } from 'react-use';

import { StepsContainer, BemErrorBoundary } from '@pcf/design-system';
import { ProdutoModel } from '@pcf/core';

import {
  CapturaLeadProvider,
  useCapturaLeadContext,
} from './captura-lead.context';
import Produto from './components/produto-form';
import DadosCliente from './components/dados-cliente-form';
import Convenio from './components/convenio-form';

interface CapturaLeadProps {
  simpleSteps?: boolean;
  simpleStepsInnerButton?: boolean;
  product?: ProdutoModel;
}

const CapturaLeadContent: FC<CapturaLeadProps> = ({
  simpleSteps = false,
  simpleStepsInnerButton = false,
}) => {
  const {
    onNext,
    index,
    onFinish,
    onPrevious,
    formData,
    isFinishingFormFilling,
  } = useCapturaLeadContext();
  const { produto } = formData;

  // TODO: update React-hook-form, workaround for resetting form.
  const [childKey, setChildKey] = useState(1);
  useUpdateEffect(() => {
    if (simpleSteps && !isFinishingFormFilling) {
      setChildKey((key) => key + 2);
    }
  }, [isFinishingFormFilling]);

  const steps = simpleSteps
    ? [
        <DadosCliente
          simpleStepsInnerButton={simpleStepsInnerButton}
          onSuccess={!formData.requerConvenio ? onFinish : onNext}
          showBack={false}
          key={childKey}
        />,
        formData.requerConvenio && <Convenio onSuccess={onFinish} key={2} />,
      ]
    : [
        <Produto onSuccess={onNext} key={1} />,
        <DadosCliente
          onSuccess={!formData.requerConvenio ? onFinish : onNext}
          key={2}
        />,
        formData.requerConvenio && <Convenio onSuccess={onFinish} key={3} />,
      ];

  return (
    <BemErrorBoundary>
      <StepsContainer
        onBack={onPrevious}
        stepNumber={index}
        showStepsIndicator={!!produto}
        showBackButton={false}
        size="lg"
      >
        {steps}
      </StepsContainer>
    </BemErrorBoundary>
  );
};

export const CapturaLead: FC<CapturaLeadProps> = ({ product, ...props }) => {
  return (
    <CapturaLeadProvider
      defaultValues={{
        produto: product?.id.toString() || '',
        requerConvenio: product?.requerConvenio,
      }}
    >
      <CapturaLeadContent {...props} />
    </CapturaLeadProvider>
  );
};
