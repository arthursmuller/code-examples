import { FC } from 'react';

import { Grid, SystemProps } from '@chakra-ui/react';

export interface FormGridProps {
  gridTemplateColumns?: SystemProps['gridTemplateColumns'];
  gridTemplateAreas?: SystemProps['gridTemplateAreas'];
}

export const FormGrid: FC<FormGridProps> = ({
  gridTemplateColumns,
  gridTemplateAreas,
  children,
}) => (
  <Grid
    marginY={4}
    gridRowGap={[2, 2, 3]}
    gridColumnGap={6}
    alignItems="center"
    gridTemplateColumns={
      gridTemplateColumns || ['1fr', '1fr', 'repeat(4, 1fr)']
    }
    gridTemplateAreas={gridTemplateAreas || 'unset'}
  >
    {children}
  </Grid>
);
