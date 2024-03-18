import { FC } from 'react';

import { useHistory, useLocation } from 'react-router-dom';
import { Flex } from '@chakra-ui/react';
import { useMount } from 'react-use';

import { StepsContainer } from '@pcf/design-system';
import { PublicRoutes as PublicRoutesEnum } from 'app/routes/public/public-routes.enum';
import { DecodedJwtGoogleData, parseJwt, RedeSocialEnum } from '@pcf/core';

import UserForm from './components/user-form';
import SecurityForm from './components/security-step';
import TermsOfServiceForm from './components/terms-of-service-form';
import { SignUpContextProvider, useSignUpContext } from './sign-up.context';

export const SignUp: FC = () => {
  const {
    currentStep,
    onBack,
    onForward,
    canGoForward,
    setFormData,
    setSocialEmail,
  } = useSignUpContext();
  const location = useLocation();
  const history = useHistory();
  const urlSearchParams = new URLSearchParams(location.search);
  const token = urlSearchParams.get('token');
  const socialMedia = urlSearchParams.get('socialMedia');

  useMount(() => {
    if (socialMedia) {
      switch (socialMedia) {
        case RedeSocialEnum.Google.toString(): {
          try {
            const decodedToken = parseJwt(token) as DecodedJwtGoogleData;
            const {
              email,
              family_name: familyName,
              given_name: givenName,
            } = decodedToken;
            setFormData({ email, name: givenName, surname: familyName });
            setSocialEmail(email);
          } catch (error) {
            history.push(PublicRoutesEnum.SignUp);
          }

          break;
        }

        case RedeSocialEnum.Apple.toString(): {
          const email = urlSearchParams.get('email');
          // Apple não provê nome e sobrenome
          // const givenName = '';
          // const surname = '';

          setFormData({ email });
          setSocialEmail(email);

          break;
        }

        case RedeSocialEnum.Facebook.toString(): {
          const email = urlSearchParams.get('email');
          const fullName = urlSearchParams.get('name');
          const givenName = fullName && fullName.split(' ')[0];
          const surname = fullName && fullName.split(' ')?.[1];

          setFormData({ email, name: givenName, surname });
          setSocialEmail(email);

          break;
        }

        default:
          history.push(PublicRoutesEnum.SignUp);
      }
    }
  });

  return (
    <Flex height="fit-content">
      <StepsContainer
        onBack={onBack}
        onForward={onForward}
        stepNumber={currentStep}
        canForward={canGoForward}
      >
        <UserForm />
        <SecurityForm />
        <TermsOfServiceForm />
      </StepsContainer>
    </Flex>
  );
};

interface SignUpProvidedProps {
  onPrevious: () => void;
}

export const SignUpProvided: FC<SignUpProvidedProps> = ({
  onPrevious,
}: SignUpProvidedProps) => {
  const history = useHistory();
  const defaultPrevious = (): void => history.push(PublicRoutesEnum.Root);

  return (
    <SignUpContextProvider onPrevious={onPrevious || defaultPrevious}>
      <SignUp />
    </SignUpContextProvider>
  );
};
