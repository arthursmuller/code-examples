import { FC, ReactNode } from 'react';

import { Flex, Box, Text, CloseButton, Button } from '@chakra-ui/react';
import { useForm } from 'react-hook-form';
import qs from 'qs';
import { useLocation } from 'react-use';

import { useModal, CpfInput, useQuickToast } from '@pcf/design-system';
import { useAppContext } from 'app/app.context';
import { useGravarLead } from '@pcf/core';
import { translateQueryParams } from 'features/captura-lead/utils/captura-lead-query-params';
import { openLink } from 'features/components/utils';
import { LogoWhatsappIcon } from '@pcf/design-system-icons';

type FormData = {
  cpf: string;
};

export const WhatsAppModal: FC<{ close?: ReactNode }> = ({ close }) => {
  const {
    handleSubmit,
    formState: { errors },
    control,
  } = useForm<FormData>();
  const { hideModal } = useModal();
  const { latitude, longitude } = useAppContext();
  const toast = useQuickToast();
  const { search = '' } = useLocation();
  const { currentCpf, setCurrentCpf } = useAppContext();

  const queryParams = qs.parse(search, { ignoreQueryPrefix: true });

  const { mutate, isLoading } = useGravarLead();

  function onSubmit(formData: FormData): void {
    mutate(
      {
        ...formData,
        latitude,
        longitude,
        desejaContatoWhatsApp: true,
        ...translateQueryParams(queryParams),
      },
      {
        onSuccess({ linkContatoWhatsAppLoja }) {
          if (linkContatoWhatsAppLoja) {
            openLink(linkContatoWhatsAppLoja, true);

            hideModal();
          } else {
            toast('Ops', 'Não foi possível localizar uma loja');
          }
        },
      },
    );
  }

  return (
    <Flex
      as="form"
      layerStyle="card"
      noValidate
      onSubmit={handleSubmit(onSubmit)}
      sx={{
        flexDirection: 'column',
        justifyContent: 'center',
        alignItems: 'center',
        height: 'auto',
        position: 'relative',
      }}
    >
      <Box
        sx={{
          height: '49px',
          width: '49px',
          position: 'absolute',
          top: '-25px',
        }}
      >
        <LogoWhatsappIcon width="49px" height="49px" />
      </Box>

      {close || (
        <CloseButton
          sx={{
            position: 'absolute',
            top: '15px',
            right: '15px',
          }}
          onClick={hideModal}
          size="sm"
          color="black"
        />
      )}

      <Flex mt="12px" flexDirection="column">
        <Text
          as="p"
          textStyle="bold16"
          color="success.regular"
          textAlign="center"
        >
          Fale com a Bem
        </Text>
        <Text
          as="p"
          textStyle="bold16"
          color="success.regular"
          textAlign="center"
        >
          pelo WhatsApp!
        </Text>
      </Flex>

      <Box marginY="18px" width="100%">
        <CpfInput
          control={control}
          defaultValue={currentCpf}
          onBlur={setCurrentCpf}
          errors={errors}
        />
      </Box>

      <Button
        isLoading={isLoading}
        loadingText="Carregando"
        isFullWidth
        colorScheme="success"
        type="submit"
      >
        Conversar
      </Button>
    </Flex>
  );
};
