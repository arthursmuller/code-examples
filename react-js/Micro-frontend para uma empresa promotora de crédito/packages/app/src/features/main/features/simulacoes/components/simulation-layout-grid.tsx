import { Children, FC } from 'react';

import { Grid, GridProps } from '@chakra-ui/react';

export const SimulationLayoutGrid: FC<GridProps> = ({
  children,
  ...gridProps
}) => {
  const childrenLength = Children.count(children);

  return (
    <Grid
      gridTemplateColumns={['1fr', '1fr', '1fr 1fr']}
      gridGap="24px"
      mt={['16px', '16px', '40px']}
      mb="16px"
      {...gridProps}
      sx={{
        '> *': {
          gridColumnStart: [
            undefined,
            undefined,
            childrenLength === 1 ? 2 : undefined,
          ],
        },
      }}
    >
      {children}
    </Grid>
  );
};
