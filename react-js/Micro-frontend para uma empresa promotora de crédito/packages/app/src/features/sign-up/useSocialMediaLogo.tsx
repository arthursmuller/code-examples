import { Center, Icon } from '@chakra-ui/react';
import { useLocation } from 'react-router-dom';

import {
  LogoAppleIcon,
  LogoFacebookIcon,
  LogoGoogleIcon,
} from '@pcf/design-system-icons';
import { RedeSocialEnum } from '@pcf/core';

export function useSocialMediaLogo(): React.ReactElement | null {
  const location = useLocation();
  const urlSearchParams = new URLSearchParams(location.search);
  const socialMedia = urlSearchParams.get('socialMedia');

  if (socialMedia === RedeSocialEnum.Google.toString()) {
    return (
      <Center
        bg="white"
        borderRadius="50%"
        w="90px"
        h="90px"
        position="absolute"
        top={5}
        left="calc(50% - 45px)"
      >
        <Icon as={LogoGoogleIcon} w="62px" h="62px" />
      </Center>
    );
  }

  if (socialMedia === RedeSocialEnum.Facebook.toString()) {
    return (
      <Center
        bg="white"
        borderRadius="50%"
        w="90px"
        h="90px"
        position="absolute"
        top={5}
        left="calc(50% - 45px)"
      >
        <Icon as={LogoFacebookIcon} w="62px" h="62px" />
      </Center>
    );
  }

  if (socialMedia === RedeSocialEnum.Apple.toString()) {
    return (
      <Center
        bg="white"
        borderRadius="50%"
        w="90px"
        h="90px"
        position="absolute"
        top={5}
        left="calc(50% - 45px)"
      >
        <Icon as={LogoAppleIcon} w="62px" h="62px" />
      </Center>
    );
  }

  return null;
}
