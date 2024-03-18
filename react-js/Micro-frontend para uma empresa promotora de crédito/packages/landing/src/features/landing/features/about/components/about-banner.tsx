import React, { FC } from 'react';

import { Flex, useBreakpointValue } from '@chakra-ui/react';

import { CustomHeading } from '@pcf/design-system';
import { BemImage } from 'features/components/images';

import CasalFeliz from '../assets/casal-feliz.jpg';
import CasalFelizMob from '../assets/casal-feliz-mob.jpg';

export const AboutBanner: FC = () => {
  const isMobile = useBreakpointValue({ base: true, md: false });

  return (
    <Flex flexDirection="column" marginBottom={10}>
      <Flex
        height={['260px', '260px', '560px', '800px']}
        width="100%"
        position="relative"
      >
        {isMobile !== undefined && (
          <BemImage
            zIndex={-1}
            src={isMobile ? CasalFelizMob : CasalFeliz}
            alt="casal feliz"
            position="left"
          />
        )}
      </Flex>
      <Flex
        boxShadow="soft"
        backgroundColor="white"
        marginTop={-14}
        marginLeft={[3, 3, 'auto']}
        paddingY={6}
        paddingRight={[1, 1, 10, '154px']}
        paddingLeft={[8, 8, 16]}
        borderLeftRadius={16}
      >
        <CustomHeading textStyle="bold32_40" color="secondary.regular">
          Isso é bem o nosso jeito de ser.
          <br />
          Isso é Bem Promotora.
        </CustomHeading>
      </Flex>
    </Flex>
  );
};
