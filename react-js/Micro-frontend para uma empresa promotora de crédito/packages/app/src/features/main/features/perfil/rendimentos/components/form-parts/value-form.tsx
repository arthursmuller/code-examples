import { FC } from 'react';

import { GridItem, Text, useBreakpointValue } from '@chakra-ui/react';

import { FormItemControl, BemCurrencyInput } from '@pcf/design-system';

import { FormPartProps } from '.';

import {
  MatriculaInssFormModelKeys,
  MatriculaInssFormModelKeys as areas,
} from '../../models/matricula-inss-form.model';

export const desktopValueGridTemplate = `'label label ${areas.valorRendimento} ${areas.valorRendimento}'`;

interface ValueFormProps extends FormPartProps {
  isCreating?: boolean;
}

export const ValueForm: FC<ValueFormProps> = ({
  initialData,
  errors,
  control,
  isCreating,
  hasGridAreas,
}) => {
  const isMobile = useBreakpointValue({ base: true, md: false }, 'base');

  return (
    <>
      <Text
        textStyle={
          isCreating ? 'regular16' : (isMobile && 'bold16') || 'bold24'
        }
        color="secondary.regular"
        textAlign="center"
        gridArea={hasGridAreas ? 'label' : 'unset'}
      >
        {isCreating
          ? 'Insira o valor do seu salário:'
          : 'O valor do seu benefício mudou?'}
      </Text>

      <GridItem gridArea={hasGridAreas ? areas.valorRendimento : 'unset'}>
        <FormItemControl
          label="Valor do Benefício"
          name={MatriculaInssFormModelKeys.valorRendimento}
          defaultValue={initialData.valorRendimento || 0}
          errorMessage={errors?.valorRendimento?.message}
          required
          rules={{
            min: { value: 1, message: 'Valor inválido' },
          }}
          control={control}
          as={BemCurrencyInput}
        />
      </GridItem>
    </>
  );
};
