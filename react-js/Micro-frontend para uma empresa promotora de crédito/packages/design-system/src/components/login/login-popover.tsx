import { FC, useState } from 'react';

import {
  Popover,
  PopoverTrigger,
  Button,
  PopoverContent,
  PopoverBody,
  Divider,
  Flex,
  Text,
} from '@chakra-ui/react';

import { EmailLoginFormProps, EmailLoginForm } from './components/email';
import {
  SocialMediaButtonProps,
  SocialMediaButtons,
} from './components/redes-sociais/social-media-buttons';

export const LoginPopoverElId = 'acessar-conta-popover-button';

export const LoginDivider: FC = () => {
  return (
    <Flex alignItems="center" mt={6}>
      <Divider borderColor="gray.700" />
      <Text w="95px" textStyle="regular14" mx={4} flexShrink={0}>
        Ou, se preferir
      </Text>
      <Divider borderColor="gray.700" />
    </Flex>
  );
};

interface LoginPopoverProps
  extends EmailLoginFormProps,
    SocialMediaButtonProps {
  onCreateAccountClick(): void;
  showCreateAccountButton: boolean;
  showSocialMediaLoginButton: boolean;
}

export const LoginPopover: FC<LoginPopoverProps> = ({
  onGoogleLoginSuccess,
  onFacebookLoginSuccess,
  onAppleLoginSuccess,
  onCreateAccountClick,
  showCreateAccountButton,
  showSocialMediaLoginButton,
  ...otherProps
}) => {
  const [showLoginSocial, setShowLoginSocial] = useState(false);

  return (
    <Popover
      onClose={() => setShowLoginSocial(false)}
      placement="bottom-start"
      gutter={17}
      variant="responsive"
      isLazy
    >
      <PopoverTrigger>
        <Button colorScheme="grey" className={LoginPopoverElId}>
          Entrar
        </Button>
      </PopoverTrigger>
      <PopoverContent
        w="350px"
        bg="grey.100"
        border="none"
        _focus={{ outline: 'none' }}
        borderRadius="12px"
        boxShadow="medium"
      >
        <PopoverBody p="32px 24px">
          {!showLoginSocial ? (
            <>
              <EmailLoginForm {...otherProps} />
              {(showSocialMediaLoginButton || showCreateAccountButton) && (
                <LoginDivider />
              )}
              {showSocialMediaLoginButton && (
                <Button
                  onClick={() => setShowLoginSocial(true)}
                  mt="21px"
                  isFullWidth
                  colorScheme="grey"
                >
                  Entrar com redes sociais
                </Button>
              )}
              {showCreateAccountButton && (
                <Button
                  isFullWidth
                  mt="21px"
                  colorScheme="grey"
                  onClick={onCreateAccountClick}
                >
                  Criar Conta
                </Button>
              )}
            </>
          ) : (
            <>
              <SocialMediaButtons
                onGoogleLoginSuccess={onGoogleLoginSuccess}
                onFacebookLoginSuccess={onFacebookLoginSuccess}
                onAppleLoginSuccess={onAppleLoginSuccess}
              />
              <LoginDivider />
              <Button
                isFullWidth
                onClick={() => setShowLoginSocial(false)}
                mt="21px"
                colorScheme="grey"
              >
                Entrar com e-mail e senha
              </Button>
              {showCreateAccountButton && (
                <Button
                  onClick={onCreateAccountClick}
                  isFullWidth
                  mt="21px"
                  colorScheme="grey"
                >
                  Criar Conta
                </Button>
              )}
            </>
          )}
        </PopoverBody>
      </PopoverContent>
    </Popover>
  );
};
