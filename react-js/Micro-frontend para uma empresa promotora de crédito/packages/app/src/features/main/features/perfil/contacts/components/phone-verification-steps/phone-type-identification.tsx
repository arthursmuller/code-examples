import { FC } from 'react';

import { Flex, Text, Button } from '@chakra-ui/react';
import { isValidMobilePhone } from '@brazilian-utils/brazilian-utils';

import {
  getFormattedPhone,
  useStepsContainerContext,
} from '@pcf/design-system';
import {
  TipoSolicitacaoConfirmacao,
  usePhoneGetConfirmationToken,
  useTelefones,
} from '@pcf/core';

import { TelefoneFormData } from './phone-form';
import { CommonTitle } from './common-title';

export const PhoneTypeIdentification: FC = () => {
  const {
    data: { phone },
    nextStep,
  } = useStepsContainerContext<TelefoneFormData>();

  const { data: telefones } = useTelefones();

  const foundTelefone = telefones
    .reverse()
    .find((telefone) => phone.includes(telefone.fone));

  const { mutate, isLoading } = usePhoneGetConfirmationToken(
    foundTelefone?.id || '',
  );

  function handleNextStep(): void {
    if (!isValidMobilePhone(phone)) {
      mutate(
        {
          tipoSolicitacaoConfirmacao: TipoSolicitacaoConfirmacao.Telefonema,
        },
        {
          onSuccess({ solicitacaoEnviada }) {
            if (solicitacaoEnviada) {
              nextStep({
                phone,
                id: Number(foundTelefone.id),
                tipoSolicitacaoConfirmacao:
                  TipoSolicitacaoConfirmacao.Telefonema,
              });
            }
          },
        },
      );
    } else {
      nextStep({ phone, id: Number(foundTelefone.id) });
    }
  }

  return (
    <Flex direction="column">
      <CommonTitle />

      <Text
        color="secondary.mid-light"
        textStyle="bold24_32"
        my={10}
        textAlign="center"
      >
        {getFormattedPhone(phone)}
      </Text>

      {!isValidMobilePhone(phone) ? (
        <Text textAlign="center">
          Identificamos que o seu telefone é fixo, por isso, você receberá um
          telefonema em breve para que possamos fazer essa verificação
        </Text>
      ) : (
        <Text textAlign="center">
          Identificamos que o seu telefone é celular, por isso, escolha a melhor
          forma para receber o código de verificação.
        </Text>
      )}

      <Button
        mt={20}
        isFullWidth
        colorScheme="secondary"
        isLoading={isLoading}
        onClick={() => handleNextStep()}
        type="submit"
      >
        Continuar
      </Button>
    </Flex>
  );
};
