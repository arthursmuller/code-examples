import { FC } from 'react';

import { Text } from '@chakra-ui/react';

import {
  CustomHeading,
  TelefoneClienteExibicaoModel,
} from '@pcf/design-system';

interface CommonTitleProps {
  phone?: TelefoneClienteExibicaoModel;
  subTitle?: string;
}

export const CommonTitle: FC<CommonTitleProps> = ({
  phone,
  subTitle = 'Precisamos validar a propriedade do seu contato',
}) => {
  return (
    <>
      <CustomHeading
        textStyle="bold24"
        color="secondary.regular"
        textAlign="center"
      >
        {phone?.fone ? 'Editar Telefone' : 'Cadastrar Telefone'}
      </CustomHeading>

      <Text
        color="secondary.mid-dark"
        textStyle="regular16"
        mt={2}
        textAlign="center"
      >
        {subTitle}
      </Text>
    </>
  );
};
