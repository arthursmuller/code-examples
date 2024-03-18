import { FC } from 'react';

import { Text, Flex, Link, HStack, useBreakpointValue } from '@chakra-ui/react';

import {
  IconeFacebookIcon,
  IconeLinkedinIcon,
  IconeYoutubeIcon,
  IconeInstagramIcon,
} from '@pcf/design-system-icons';

export const SocialMedia: FC = () => {
  const isMobile = useBreakpointValue({ base: true, md: false }, 'base');

  return (
    <Flex
      flexDirection="column"
      justifyContent="flex-end"
      alignItems="space-around"
    >
      <Text
        textAlign={['center', 'start', 'start', 'start']}
        as="p"
        textStyle={isMobile ? 'regular16' : 'bold20'}
        color="white"
      >
        Nos acompanhe nas nossas redes sociais!
      </Text>

      <HStack
        mt={[6, 6, 6, 2]}
        mb={[8, 8, 8, 2]}
        spacing={4}
        justifyContent={[
          'space-around',
          'space-around',
          'space-around',
          'flex-end',
        ]}
      >
        <Link href="https://www.facebook.com/bempromotora" isExternal>
          <IconeFacebookIcon color="white" width="24px" height="24px" />
        </Link>

        <Link href="https://www.instagram.com/bempromotora/" isExternal>
          <IconeInstagramIcon color="white" width="24px" height="24px" />
        </Link>

        <Link
          href="https://www.youtube.com/channel/UCbC8SikuGTlnxOEg7_Yy-DA"
          isExternal
        >
          <IconeYoutubeIcon color="white" width="34px" height="24px" />
        </Link>

        <Link href="https://www.linkedin.com/company/3657713" isExternal>
          <IconeLinkedinIcon color="white" width="24px" height="24px" />
        </Link>
      </HStack>
    </Flex>
  );
};
