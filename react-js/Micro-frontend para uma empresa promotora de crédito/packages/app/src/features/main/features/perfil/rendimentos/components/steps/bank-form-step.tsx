import { FC } from 'react';

import { useBreakpointValue } from '@chakra-ui/react';

import { BemErrorBoundary } from '@pcf/design-system';

import { MatriculaBaseStep } from './matricula-base-step';

import { MatriculaSiapeFormModel } from '../../models/matricula-siape-form.model';
import { BankAccountForm, SpecieForm } from '../form-parts';
import {
  MatriculaInssFormModel,
  MatriculaInssFormModelKeys as areas,
} from '../../models/matricula-inss-form.model';

const desktopDialogTemplateAreas = `'${areas.tipoConta} ${areas.tipoConta}'
                                    '${areas.banco} ${areas.banco}'
                                    '${areas.agencia} ${areas.conta}'
                                    '${areas.inssEspecieBeneficio} ${areas.inssEspecieBeneficio}'`;

export const BankFormStep: FC = () => {
  const isMobile = useBreakpointValue({ base: true, md: false }, 'base');

  return (
    <BemErrorBoundary>
      <MatriculaBaseStep<MatriculaSiapeFormModel>
        renderForm={(props) => (
          <BankAccountForm {...props} hasGridAreas={!isMobile} />
        )}
        gridTemplateColumns={!isMobile ? '1fr 1fr' : '1fr'}
        gridTemplateAreas={!isMobile ? desktopDialogTemplateAreas : 'unset'}
      />
    </BemErrorBoundary>
  );
};

export const BankWithSpecieFormStep: FC = () => {
  const isMobile = useBreakpointValue({ base: true, md: false }, 'base');

  return (
    <BemErrorBoundary>
      <MatriculaBaseStep<MatriculaInssFormModel>
        renderForm={(props) => (
          <>
            <BankAccountForm {...props} hasGridAreas={!isMobile} />
            <SpecieForm {...props} hasGridAreas={!isMobile} />
          </>
        )}
        gridTemplateColumns={!isMobile ? '1fr 1fr' : '1fr'}
        gridTemplateAreas={!isMobile ? desktopDialogTemplateAreas : 'unset'}
      />
    </BemErrorBoundary>
  );
};
