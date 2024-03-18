import { createContext, useContext, useState, FC } from 'react';

import { useRouter } from 'next/router';

import { useAppContext } from 'app/app.context';
import {
  useAtualizarLead,
  useGravarLead,
} from '@pcf/core';
import { useModal, useStepState } from '@pcf/design-system';

import { translateQueryParams } from './utils/captura-lead-query-params';
import {
  ConvenioFormData,
  DadosClienteFormData,
  ProdutoFormData,
} from './models/captura-lead.model';

interface FormData
  extends ProdutoFormData,
    DadosClienteFormData,
    ConvenioFormData {}

interface CapturaLeadData {
  index: number;
  formData: FormData;
  onNext(): void;
  onPrevious(): void;
  onUpdateFormData(
    data: DadosClienteFormData | ConvenioFormData | ProdutoFormData,
  ): void;
  onSave(data: DadosClienteFormData | ConvenioFormData | ProdutoFormData): void;
  onFinish(data: DadosClienteFormData | ConvenioFormData): Promise<void>;
  isCreatingLead: boolean;
  isFinishingFormFilling: boolean;
}

const CapturaLeadContext = createContext<CapturaLeadData>(
  {} as CapturaLeadData,
);

const initialFormDataState = {
  produto: '',
  cpf: '',
  telefone: '',
  email: '',
  convenio: '',
  requerConvenio: false,
};

// queryCacheConfig.prefetchQuery(getProdutosQueryConfig());
// queryCacheConfig.prefetchQuery(getConveniosQueryConfig());

const CapturaLeadProvider: FC<{ defaultValues: Partial<FormData> }> = ({
  children,
  defaultValues,
}) => {
  const [formData, setFormData] = useState<FormData>({
    ...initialFormDataState,
    ...defaultValues,
  });
  const [leadId, setLeadId] = useState<number | null>(null);
  const {
    stepNumber,
    nextStep,
    previousStep,
    reset: resetStep,
  } = useStepState();

  const { latitude, longitude } = useAppContext();
  const { mutate: atualizarLead } = useAtualizarLead();
  const { mutate: gravarLead, isLoading } = useGravarLead();
  const { showModal } = useModal();
  const { query: queryParams } = useRouter();
  const [isFinishingFormFilling, setIsFinishingFormFilling] = useState(false);

  function updateFormData(
    data: DadosClienteFormData | ConvenioFormData | ProdutoFormData,
  ): void {
    setFormData((oldFormData) => ({ ...oldFormData, ...data }));
  }

  function reset(): void {
    setFormData({ ...initialFormDataState, ...defaultValues });
    resetStep();
    setLeadId(null);
  }

  async function save(
    data: DadosClienteFormData | ConvenioFormData | ProdutoFormData,
    cb?: () => void,
  ): Promise<void> {
    if (!leadId) {
      !isLoading &&
        (await gravarLead(
          {
            ...formData,
            ...data,
            ...translateQueryParams(queryParams),
            idProduto: +(data as ProdutoFormData).produto || undefined,
            latitude,
            longitude,
            desejaContatoWhatsApp: false,
          },
          {
            onSuccess({ id }) {
              updateFormData(data);
              setLeadId(id);

              cb && cb();
            },
          },
        ));
    } else {
      const payload = {
        id: leadId,
        data: {
          ...formData,
          ...data,
          idConvenio:
            +(data as ConvenioFormData).convenio ||
            +(formData as ConvenioFormData).convenio ||
            undefined,
          idProduto:
            +(data as ProdutoFormData).produto ||
            +(formData as ProdutoFormData).produto ||
            undefined,
        },
      };
      updateFormData(payload.data);

      await atualizarLead(payload, { onSuccess: () => cb && cb() });
    }
  }

  async function handleFinish(
    data: DadosClienteFormData | ConvenioFormData,
  ): Promise<void> {
    setIsFinishingFormFilling(true);

    await save(data, () => {
      showModal({
        closeOnClickOverlay: false,
        title: 'Pronto! Seu pedido foi enviado com sucesso!',
        information:
          'Recebemos seus dados e, em breve, entraremos em contato para dar andamento ao seu sonho e solucionar eventuais dúvidas.',
      });

      reset();
      setIsFinishingFormFilling(false);
    });
  }

  return (
    <CapturaLeadContext.Provider
      value={{
        index: stepNumber,
        onNext: nextStep,
        onPrevious: previousStep,
        onUpdateFormData: updateFormData,
        onSave: save,
        onFinish: handleFinish,
        formData,
        isCreatingLead: isLoading,
        isFinishingFormFilling,
      }}
    >
      {children}
    </CapturaLeadContext.Provider>
  );
};

function useCapturaLeadContext(): CapturaLeadData {
  const context = useContext(CapturaLeadContext);

  if (!context) {
    throw new Error(
      'useCapturaLeadContext must be used within an CapturaLeadContext',
    );
  }

  return context;
}

export { CapturaLeadProvider, useCapturaLeadContext };
