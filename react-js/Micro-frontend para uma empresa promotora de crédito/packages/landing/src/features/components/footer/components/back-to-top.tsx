import { FC } from 'react';

import { Text, Flex, Icon, Center } from '@chakra-ui/react';

import { ArrowUpIcon } from '@pcf/design-system-icons';

export const BackToTop: FC = () => {
  const scrollTop = (): void => {
    const childEl = document.getElementById('page')?.children[0];

    if (childEl) {
      childEl.scrollIntoView({ block: 'start', behavior: 'smooth' });
    } else {
      window.scrollTo(0, 0);
    }
  };

  return (
    <Flex
      width="100%"
      bg="grey.700"
      height="48px"
      color="white"
      justifyContent="center"
      alignItems="center"
      display={['flex', 'flex', 'flex', 'none']}
      onClick={scrollTop}
      cursor="pointer"
    >
      <Text textAlign="center" as="p" textStyle="bold16" color="white">
        Topo da página
      </Text>
      <Center borderRadius="full" marginLeft="16px" bg="white" boxSize={5}>
        <Icon as={ArrowUpIcon} color="grey.700" boxSize={3} />
      </Center>
    </Flex>
  );
};
