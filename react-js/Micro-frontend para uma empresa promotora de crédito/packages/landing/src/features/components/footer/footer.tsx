import React, { FC, useState } from 'react';

import {
  Flex,
  Text,
  Box,
  useBreakpointValue,
  Stack,
  StackDivider,
  Divider,
  Icon,
} from '@chakra-ui/react';
import { useMount } from 'react-use';

import { LogoBemHorizontalWhiteIcon } from '@pcf/design-system-icons';

import { BEM_PROMOTORA_LINKS, LIST_FOOTER_ITEMS } from './footer.consts';
import { ListFooterItems, SocialMedia, BackToTop } from './components';

export const Footer: FC = () => {
  const isMobile = useBreakpointValue([true, true, true, false], 'base');

  // TODO: Stack divider does not render properly on SSR
  const [mount, isMount] = useState<boolean>();
  useMount(() => isMount(true));

  return !mount ? (
    <div />
  ) : (
    <Flex as="footer" width="100%" bg="grey.800" flexDir="column" h="auto">
      {isMobile && (
        <Flex
          background="primary.gradient"
          flex={1}
          alignItems="center"
          justifyContent="center"
          minHeight="96px"
        >
          <Icon as={LogoBemHorizontalWhiteIcon} height="32px" width="190px" />
        </Flex>
      )}
      <Box p={8}>
        <Stack
          direction={['column', 'column', 'column', 'row']}
          wrap="wrap"
          spacing={[10, 10, 10, 5]}
          divider={
            (!isMobile && <StackDivider borderColor="grey.700" />) || undefined
          }
          alignItems={['center', 'center', 'center', 'start']}
          justifyContent="space-around"
        >
          <Stack
            spacing="30px"
            divider={
              (!isMobile && <StackDivider borderColor="grey.700" />) ||
              undefined
            }
          >
            {!isMobile && (
              <Icon
                as={LogoBemHorizontalWhiteIcon}
                height="32px"
                width="190px"
              />
            )}
            <ListFooterItems
              key="bem-promotora-links"
              listFooterItem={BEM_PROMOTORA_LINKS}
            />
          </Stack>

          {LIST_FOOTER_ITEMS.map((listFooterItems) => (
            <ListFooterItems
              key={listFooterItems.title}
              listFooterItem={listFooterItems}
            />
          ))}
        </Stack>

        {isMobile && <Divider mt={9} borderColor="grey.400" />}

        <Text
          pt={['23px', '23px', '23px', '51px']}
          textStyle={isMobile ? 'regular12' : 'regular16'}
          color="grey.400"
          textAlign="justify"
        >
          <Text color="grey.100" as="span">
            Crédito sujeito a margem consignável disponível e adequação à
            política de concessão estabelecida.{' '}
          </Text>
          Oferecemos prazos de no mínimo 24 meses até 96 meses com juros de no
          máximo 2,05% ao mês e 28,97% ao ano.{' '}
          <Text color="grey.100" as="span">
            CEF (Custo Efetivo Total)
          </Text>{' '}
          - Todas as operações têm incidência de IOF conforme previsto na
          legislação vigente. Os prazos e taxas apresentadas dependerão da
          análise e disponibilidade de cada convênio e poderão sofrer alterações
          a qualquer momento, sem aviso prévio.{' '}
          <Text color="grey.100" as="span">
            Exemplo representativo:
          </Text>{' '}
          Valor financiado: R$ 13.000,00 | Prazo: 84 meses | Taxa máxima: 1,80 %
          ao mês | Taxa anual (CET) 25,43 % a.a | Valor da parcela mensal: R$
          315,13. Custo total máximo do empréstimo ao final do período: R$
          26.470,92.{' '}
        </Text>

        <Stack
          spacing={[0, 0, 0, 3]}
          mt={[4, 4, 7]}
          direction={['column', 'column', 'column', 'row']}
          divider={
            (!isMobile && <StackDivider borderColor="primary.light" />) ||
            undefined
          }
          alignItems={['center', 'center', 'center', 'start']}
        >
          <Text
            textStyle={isMobile ? 'regular12' : 'regular14'}
            textAlign={['center', 'center', 'start']}
            color="primary.light"
          >
            Bem Promotora - Todos direitos reservados.
          </Text>

          <Text
            textStyle={isMobile ? 'regular12' : 'regular14'}
            textAlign={['center', 'center', 'start']}
            color="primary.light"
          >
            Rua Siqueira Campos, nº 1163 – Porto Alegre/RS
          </Text>

          <Text
            textStyle={isMobile ? 'regular12' : 'regular14'}
            textAlign={['center', 'center', 'start']}
            color="primary.light"
          >
            CNPJ: 10.397.031/0001-81
          </Text>
        </Stack>
      </Box>

      <Flex
        minH={20}
        bg="primary.gradient"
        alignItems="center"
        justifyContent="space-between"
        flexDir={['column', 'column', 'column', 'row']}
        px={8}
        pt={[4, 4, 4, 0]}
      >
        <Text
          textStyle="bold32"
          mb={[6, 6, 6, 0]}
          textAlign={['center', 'start', 'start']}
          color="grey.100"
        >
          Bem Promotora, você sempre Bem!
        </Text>

        <SocialMedia />
      </Flex>

      <BackToTop />
    </Flex>
  );
};
