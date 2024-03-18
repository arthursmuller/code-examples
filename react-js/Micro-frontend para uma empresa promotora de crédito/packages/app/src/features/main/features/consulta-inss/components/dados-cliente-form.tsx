import { FC } from 'react';

import { useForm } from 'react-hook-form';
import { Flex, Text, Button } from '@chakra-ui/react';
import { useGeolocation } from 'react-use';

import {
  SolicitacaoAutorizacaoConsultaBeneficioModel,
  useBeneficioInssAutorizacaoMutation,
  useClienteLogado,
  TelefoneClienteModel,
  useTelefones,
  useCreatePhones,
} from '@pcf/core';
import {
  BemTextInput,
  CpfInput,
  FormItemControl,
  getCelular,
  Loader,
  PhoneInput,
  useQuickToast,
} from '@pcf/design-system';

import { getDDDandFone } from '../../perfil/contacts/utils/contacts.utils';

export interface DadosClienteFormData {
  nome?: string;
  cpf?: string;
  celular: string;
}

interface DadosClienteFormProps {
  onSuccess: (data: SolicitacaoAutorizacaoConsultaBeneficioModel) => void;
  active: boolean;
}

const inactiveStyles = {
  opacity: '0.4',
  pointerEvents: 'none',
};

export const DadosClienteForm: FC<DadosClienteFormProps> = ({
  onSuccess,
  active,
}) => {
  const {
    handleSubmit,
    formState: { errors },
    control,
  } = useForm<DadosClienteFormData>();

  const { data: clienteLogado, isLoading: isLoadingClienteLogado } =
    useClienteLogado();
  const { data: phones, isLoading: isLoadingTelefones } = useTelefones();
  const { mutate: phonesMutation, isLoading: isLoadingPhoneMutation } =
    useCreatePhones();
  const { mutate: mutateBeneficioInss, isLoading: isLoadingBeneficioInss } =
    useBeneficioInssAutorizacaoMutation();
  const { latitude, longitude } = useGeolocation();
  const toast = useQuickToast();

  const celularCliente = getCelular(phones);

  function handleMutateBeneficioInss(id: number): void {
    mutateBeneficioInss(
      {
        idTelefoneEnvioSolicitacao: id,
        latitude,
        longitude,
      },
      {
        onSuccess(response) {
          onSuccess(response);
        },
        onError() {
          toast('Houve um problema na requisição');
        },
      },
    );
  }

  function onSubmit(formData: DadosClienteFormData): void {
    if (celularCliente) {
      handleMutateBeneficioInss(celularCliente.id);
    } else {
      const newPhone: TelefoneClienteModel = {
        ...getDDDandFone(formData.celular),
        id: null,
        principal: false,
      };
      // TODO:
      // https://ava.bempromotora.com.br/browse/PCF-1291

      console.log('TODO:');
      console.log('https://ava.bempromotora.com.br/browse/PCF-1291');

      // phonesMutation(
      //   phones
      //     ? (phones as TelefoneClienteModel[]).concat(newPhone)
      //     : [newPhone],
      //   {
      //     onSuccess(data) {
      //       const foundCelular = getCelular(data);
      //       if (foundCelular) {
      //         handleMutateBeneficioInss(foundCelular.id);
      //       }
      //     },
      // },
      // );
    }
  }

  return (
    <Flex
      w={['100%', '100%', '251px']}
      minH="304px"
      layerStyle="card"
      as="form"
      onSubmit={handleSubmit(onSubmit)}
      flexDirection="column"
      sx={!active ? inactiveStyles : {}}
    >
      <Text
        textStyle="bold14"
        color="secondary.mid-dark"
        mb={4}
        textAlign="center"
      >
        Dados do Cliente
      </Text>

      {isLoadingClienteLogado || isLoadingTelefones ? (
        <Loader />
      ) : (
        <>
          <FormItemControl
            label="Nome"
            name="nome"
            disabled
            defaultValue={clienteLogado?.nome || ''}
            errorMessage={errors?.nome?.message}
            control={control}
            as={BemTextInput}
          />

          <CpfInput
            disabled
            control={control}
            defaultValue={clienteLogado?.cpf || ''}
            errors={errors}
          />

          <PhoneInput
            defaultValue={
              celularCliente
                ? `${celularCliente.ddd}${celularCliente.fone}`
                : ''
            }
            label="Celular"
            disabled={!!celularCliente?.id}
            name="celular"
            errorMessage={errors?.celular?.message}
            control={control}
            acceptLandlinePhone={false}
          />
        </>
      )}

      <Button
        type="submit"
        isFullWidth
        size="sm"
        colorScheme="secondary"
        isLoading={isLoadingBeneficioInss || isLoadingPhoneMutation}
      >
        Enviar Token
      </Button>
    </Flex>
  );
};
