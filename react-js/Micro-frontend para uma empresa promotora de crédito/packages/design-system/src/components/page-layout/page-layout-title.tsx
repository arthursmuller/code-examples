import { FC } from 'react';

import { CustomHeading } from '../custom-heading';

export const PageLayoutTitle: FC = ({ children }) => (
  <CustomHeading
    as="h2"
    textStyle="bold32"
    color="grey.100"
    mt={[5, 5, 2]}
    mr={['65px', '75px', 0]}
    mb={['35px', '35px', 0]}
  >
    {children}
  </CustomHeading>
);
