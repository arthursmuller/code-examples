import { FC } from 'react';

import { GridItem } from '@chakra-ui/react';

import { BemTextInput, FormItemControl } from '@pcf/design-system';

import { FormPartProps } from '.';

import {
  MatriculaInssFormModelKeys,
  MatriculaInssFormModelKeys as areas,
} from '../../models/matricula-inss-form.model';
import { SelectBancos, SelectTiposConta } from '../selects';

export const desktopBankAccountGridTemplate = `'${areas.tipoConta} ${areas.banco} ${areas.banco} ${areas.banco}' 
                                               '${areas.agencia} ${areas.conta} ${areas.inssEspecieBeneficio} ${areas.inssEspecieBeneficio}'`;

export const BankAccountForm: FC<FormPartProps> = ({
  initialData,
  errors,
  control,
  hasGridAreas,
}) => (
  <>
    <GridItem gridArea={hasGridAreas ? areas.tipoConta : 'unset'}>
      <FormItemControl
        label="Tipo de Conta"
        name={MatriculaInssFormModelKeys.tipoConta}
        defaultValue={initialData?.tipoConta}
        errorMessage={errors?.tipoConta?.message}
        control={control}
        as={SelectTiposConta}
        required
      />
    </GridItem>
    <GridItem gridArea={hasGridAreas ? areas.banco : 'unset'}>
      <FormItemControl
        label="Banco"
        name={MatriculaInssFormModelKeys.banco}
        defaultValue={initialData?.banco}
        errorMessage={errors?.banco?.message}
        control={control}
        as={SelectBancos}
        required
      />
    </GridItem>
    <GridItem gridArea={hasGridAreas ? areas.agencia : 'unset'}>
      <FormItemControl
        label="Agência"
        name={MatriculaInssFormModelKeys.agencia}
        defaultValue={initialData?.agencia}
        errorMessage={errors?.agencia?.message}
        control={control}
        as={BemTextInput}
        type="number"
        required
      />
    </GridItem>
    <GridItem gridArea={hasGridAreas ? areas.conta : 'unset'}>
      <FormItemControl
        label="Conta"
        name={MatriculaInssFormModelKeys.conta}
        defaultValue={initialData?.conta}
        errorMessage={errors?.conta?.message}
        control={control}
        as={BemTextInput}
        type="number"
        required
      />
    </GridItem>
  </>
);
