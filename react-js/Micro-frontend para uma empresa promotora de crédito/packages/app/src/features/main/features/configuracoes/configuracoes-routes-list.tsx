import { FC } from 'react';

import { VStack } from '@chakra-ui/react';
import { Link } from 'react-router-dom';

import { ButtonCard } from '@pcf/design-system';
import { PadlockIcon } from '@pcf/design-system-icons';
import { RouteOpt } from 'features/main/components/use-sub-route-menu';

import { ConfigRoutesPaths } from './configuracoes-routes';

interface configOptions extends RouteOpt {
  icon: FC;
}

export const configOptions: configOptions[] = [
  {
    title: 'Quero redefinir minha senha',
    icon: PadlockIcon,
    route: ConfigRoutesPaths.atualizarSenha,
    disabled: false,
  },
];

export const ConfiguracoesRoutesList: FC = () => (
  <VStack spacing={6} py={6} align="stretch">
    {configOptions.map(({ title, icon, route, disabled }) => (
      <Link
        key={title}
        to={route}
        style={disabled ? { pointerEvents: 'none' } : {}}
      >
        <ButtonCard
          isFullWidth
          title={title}
          icon={icon}
          disabled={disabled}
          colorScheme="primary"
        />
      </Link>
    ))}
  </VStack>
);
