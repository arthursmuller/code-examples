import { FC } from 'react';

import { VStack } from '@chakra-ui/react';
import { Link } from 'react-router-dom';

import { ButtonCard } from '@pcf/design-system';

import { perfilOptions } from '../perfil.consts';

export const ListPerfilOptions: FC = () => {
  return (
    <VStack spacing={6} py={6} align="stretch">
      {perfilOptions.map(({ title, description, route, disabled }) => (
        <Link
          key={title}
          to={route}
          style={disabled ? { pointerEvents: 'none' } : {}}
        >
          <ButtonCard
            isFullWidth
            title={title}
            description={description}
            disabled={disabled}
          />
        </Link>
      ))}
    </VStack>
  );
};
