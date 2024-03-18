import { FC } from 'react';

import { Button, Flex, Icon, Text } from '@chakra-ui/react';
import { Control, Controller, useForm } from 'react-hook-form';

import {
  CustomHeading,
  RadioCard,
  RadioCardsGroup,
  useStepsContainerContext,
} from '@pcf/design-system';
import { RgFrontIcon } from '@pcf/design-system-icons';

import { IdentityType } from './identity-types.enum';
import { UploadIdentityForm } from './models/upload-identity-form.model';

const documentTypes = [
  {
    type: IdentityType.RG,
    description: 'Com CPF',
  },
  {
    type: IdentityType.CNH,
    description: 'Carteira Nacional de Habilitação',
  },
  {
    type: IdentityType.Passport,
    description: 'Páginas de Identificação',
  },
];

export const IdentityTypeForm: FC<{
  documentTypeValue: string;
  formControl: Control<any>;  //eslint-disable-line
}> = ({ documentTypeValue, formControl }) => {
  return (
    <>
      <Flex
        alignItems="center"
        justifyContent="center"
        marginBottom="16px"
        marginX="8px"
      >
        <Icon
          as={RgFrontIcon}
          borderRadius="full"
          border="1px solid"
          padding="2px"
          height="48px"
          width="48px"
          marginRight="8px"
          color="secondary.mid-dark"
        />

        <CustomHeading
          as="h2"
          textStyle="bold24_32"
          color="secondary.mid-dark"
          marginBottom="0px"
        >
          Documento de Identificação
        </CustomHeading>
      </Flex>

      <Text as="p" textStyle="regular14" textAlign="center" marginBottom="8px">
        Escolha somente um documento.
      </Text>

      <Controller
        control={formControl}
        name="documentType"
        defaultValue={documentTypeValue || ''}
        rules={{ required: true }}
        render={({ field: { onChange, value } }) => (
          <RadioCardsGroup
            name="documentType"
            onChange={(id) => onChange(id)}
            defaultValue={value}
            minWidth="auto"
          >
            {documentTypes.map((opt) => (
              <RadioCard
                key={opt.type}
                value={opt.type}
                title={opt.type}
                information={opt.description}
              />
            ))}
          </RadioCardsGroup>
        )}
      />
    </>
  );
};

export const IdentityTypeStep: FC = () => {
  const { nextStep, data } = useStepsContainerContext<UploadIdentityForm>();
  const { control, formState, handleSubmit } = useForm({
    mode: 'onChange',
  });

  const { isValid } = formState;

  return (
    <Flex direction="column">
      <IdentityTypeForm
        documentTypeValue={data.documentType as string}
        formControl={control}
      />

      <Button onClick={handleSubmit(nextStep)} disabled={!isValid}>
        Continuar
      </Button>
    </Flex>
  );
};
