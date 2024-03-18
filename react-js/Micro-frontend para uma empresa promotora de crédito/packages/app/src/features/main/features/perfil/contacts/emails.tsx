import React, { FC } from 'react';

import { Flex } from '@chakra-ui/react';

import { BemErrorBoundary, CustomHeading } from '@pcf/design-system';

import { EmailList } from './components/emails-list';

export const Emails: FC = () => {
  return (
    <Flex flexDir="column" flexGrow={1}>
      <CustomHeading
        textStyle="bold24_32"
        as="h2"
        color="secondary.regular"
        mt={10}
      >
        E-mail
      </CustomHeading>

      <BemErrorBoundary>
        <EmailList />
      </BemErrorBoundary>
    </Flex>
  );
};
