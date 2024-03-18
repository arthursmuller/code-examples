import { FC, ReactElement, useState } from 'react';

import { Button, Center } from '@chakra-ui/react';
import { useQuery } from 'react-query';

import { fakeApiCall } from './fake-api-call';
import { BemErrorBoundary } from './bem-error-boundary';

import { BemErrorFallback } from '../bem-error-fallback';
import { Loader } from '../../loader';
// import { queryCacheConfig } from 'app/app-providers';
import { CustomHeading } from '../../custom-heading';

export default {
  title: 'Error/Bem Error Boundary',
  component: BemErrorBoundary,
  decorators: [
    (StoryWrapped: FC): ReactElement => (
      // <QueryClientProvider client={queryCacheConfig}>
      <StoryWrapped />
      // </QueryClientProvider>
    ),
  ],
};

const ComponentCaller: FC = () => {
  const { data, isLoading } = useQuery('fake-call-key', fakeApiCall, {
    retry: 0,
  });

  return (
    <Center h="400px" w="100%" bg="secondary.washed">
      {data && (
        <Center>
          <CustomHeading textStyle="bold40">{data}</CustomHeading>
        </Center>
      )}
      {isLoading && <Loader />}
    </Center>
  );
};

const Bomb: FC = () => {
  throw new Error('💥 KABOOM 💥');
};

export const AnyError: FC = () => {
  const [bomb, showBomb] = useState(false);

  return (
    <BemErrorBoundary onReset={() => showBomb(false)} resetKeys={['bomb']}>
      {bomb && <Bomb />}
      <Button onClick={() => showBomb(true)}>
        Bomb
        <span role="img" aria-label="bomb">
          💥
        </span>
      </Button>
    </BemErrorBoundary>
  );
};

export const ReactQuery: FC = () => {
  return (
    <BemErrorBoundary
      fallbackRender={(props) => (
        <BemErrorFallback {...props} description="Irá falhar 3x" />
      )}
    >
      <ComponentCaller />
    </BemErrorBoundary>
  );
};
