import { FC } from 'react';

import { Flex, Text, Icon, Button, Center } from '@chakra-ui/react';
import { Controller, useForm } from 'react-hook-form';

import {
  RadioCard,
  RadioCardLogicProps,
  RadioCardsGroup,
  useStepsContainerContext,
} from '@pcf/design-system';
import {
  SpeechbubbleIcon,
  TelephoneOutlineIcon,
  WhatsappIcon,
} from '@pcf/design-system-icons';
import {
  TipoSolicitacaoConfirmacao,
  usePhoneGetConfirmationToken,
} from '@pcf/core';
import { useFeatureFlags } from 'app';

import { CommonTitle } from './common-title';
import { TelefoneFormData } from './phone-form';

enum PhoneVerificationType {
  ligacao = 'ligacao',
  sms = 'sms',
  whatapp = 'whatsapp',
}

interface CustomRadioCardProps extends RadioCardLogicProps {
  value: string;
  title: string;
  icon: FC;
}

const CustomRadioCard: FC<CustomRadioCardProps> = ({
  value,
  title,
  icon,
  ...rest
}) => {
  return (
    <RadioCard
      containerDirection="row"
      value={value}
      customContent={({ isChecked }) => (
        <Flex
          justifyContent="space-between"
          alignItems="center"
          pl={4}
          flexGrow={1}
        >
          <Text as="h3" textStyle="bold20" color="inherit">
            {title}
          </Text>

          <Icon
            as={icon}
            w="22px"
            h="22px"
            color={isChecked ? 'white' : 'primary.regular'}
          />
        </Flex>
      )}
      {...rest}
    />
  );
};

type PhoneVerificationOptionsFormType = {
  phoneVerificationType: string;
};

const getTipoSolicitacaoEnum = (
  data: PhoneVerificationOptionsFormType,
): number => {
  switch (data.phoneVerificationType) {
    case PhoneVerificationType.ligacao:
      return TipoSolicitacaoConfirmacao.Telefonema;
    case PhoneVerificationType.sms:
      return TipoSolicitacaoConfirmacao.Sms;
    case PhoneVerificationType.whatapp:
      return TipoSolicitacaoConfirmacao.WhatsApp;

    default:
      throw new Error('Impossible');
  }
};

export const PhoneVerificationOptions: FC = () => {
  const {
    nextStep,
    data: { id },
  } = useStepsContainerContext<TelefoneFormData>();

  const { flags } = useFeatureFlags();

  const { control, watch, handleSubmit } =
    useForm<PhoneVerificationOptionsFormType>({
      mode: 'onChange',
    });

  const { mutate, isLoading } = usePhoneGetConfirmationToken(id);

  const phoneVerificationType = watch('phoneVerificationType');

  const onSubmit = (data: PhoneVerificationOptionsFormType): void => {
    const tipoSolicitacao = getTipoSolicitacaoEnum(data);
    mutate(
      {
        tipoSolicitacaoConfirmacao: tipoSolicitacao,
      },
      {
        onSuccess({ solicitacaoEnviada }) {
          if (solicitacaoEnviada) {
            nextStep({ tipoSolicitacaoConfirmacao: tipoSolicitacao });
          }
        },
      },
    );
  };

  return (
    <Flex flexDir="column" as="form" onSubmit={handleSubmit(onSubmit)}>
      <CommonTitle />

      {!flags?.TELEFONE_CRIACAO_VALIDACAO_TELEFONEMA &&
      !flags?.TELEFONE_CRIACAO_VALIDACAO_SMS &&
      !flags?.TELEFONE_CRIACAO_VALIDACAO_WHATSAPP ? (
        <Text textAlign="center" w="100%" mt={6}>
          Nenhum meio de validação está disponível no momento. Lamentamos o
          ocorrido. Por favor, tente mais tarde.
        </Text>
      ) : (
        <>
          <Text mt="34px" textAlign="center">
            Desejo receber a verificação por
          </Text>
          <Center>
            <Controller
              control={control}
              name="phoneVerificationType"
              rules={{ required: true }}
              render={({ field: { onChange, value } }) => (
                <RadioCardsGroup
                  fitMode="fill"
                  chakraProps={{ maxW: ['90%', '90%', '70%'] }}
                  name="phoneVerificationType"
                  onChange={onChange}
                  defaultValue={value}
                >
                  {flags?.TELEFONE_CRIACAO_VALIDACAO_TELEFONEMA && (
                    <CustomRadioCard
                      value={PhoneVerificationType.ligacao}
                      title="Ligação"
                      icon={TelephoneOutlineIcon}
                    />
                  )}
                  {flags?.TELEFONE_CRIACAO_VALIDACAO_SMS && (
                    <CustomRadioCard
                      value={PhoneVerificationType.sms}
                      title="SMS"
                      icon={SpeechbubbleIcon}
                    />
                  )}
                  {flags?.TELEFONE_CRIACAO_VALIDACAO_WHATSAPP && (
                    <CustomRadioCard
                      value={PhoneVerificationType.whatapp}
                      title="WhatsApp"
                      icon={WhatsappIcon}
                    />
                  )}
                </RadioCardsGroup>
              )}
            />
          </Center>
        </>
      )}
      <Button
        type="submit"
        mt={6}
        isLoading={isLoading}
        isFullWidth
        colorScheme="secondary"
        disabled={!phoneVerificationType}
      >
        Continuar
      </Button>
    </Flex>
  );
};
