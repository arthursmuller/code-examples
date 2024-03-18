import { FC } from 'react';

import { Box, Button, Center, Text, Wrap } from '@chakra-ui/react';

import { CustomHeading } from '@pcf/design-system';
import { useLandingContext } from 'features/landing/landing.context';
import { BemImage } from 'features/components/images';

import { IconeDinheiro, IconeCasa, IconeGastos } from './assets';

const benefits = [
  {
    id: 1,
    icon: IconeDinheiro,
    text: '<b>Dinheiro extra</b> de maneira rápida, fácil e sem burocracias.',
    altDescription: 'ilustração dinheiro',
  },
  {
    id: 2,
    icon: IconeCasa,
    text: '<b>Realize seus sonhos</b> de reformar o lar, dar entrada num carro ou casa.',
    altDescription: 'ilustração casa',
  },
  {
    id: 3,
    icon: IconeGastos,
    text: 'Ou para lidar com os <b>gastos do dia a dia</b> ou pagar as contas. ',
    altDescription: 'ilustração gastos',
  },
];

export const CreditoConsignado: FC = () => {
  const { onShowSimulador } = useLandingContext();

  return (
    <Center
      flexDir="column"
      as="section"
      p="45px 18px"
      bg="grey.100"
      mt={['60px', 0]}
    >
      <CustomHeading
        as="h2"
        textStyle="bold32_40"
        color="secondary.mid-dark"
        textAlign="center"
      >
        Crédito Consignado
      </CustomHeading>
      <CustomHeading as="h3" textStyle="regular16_20" mt={4} textAlign="center">
        A Bem Promotora te ajuda a realizar os seus sonhos!
      </CustomHeading>

      <Wrap mt="39px" spacing={[4, 4, 6]} justify="center">
        {benefits.map(({ id, icon, text, altDescription }) => (
          <Center
            key={id}
            layerStyle="card"
            h={['137px', '137px', '288px']}
            w={['100%', '100%', '340px']}
            flexDir={['row', 'row', 'column']}
          >
            <Box mb={[0, 0, 6]} mr={[6, 6, 0]}>
              <BemImage
                src={icon}
                alt={altDescription}
                width={['48px', '48px', '56px']}
                height={['48px', '48px', '56px']}
              />
            </Box>
            <Text
              textStyle={['regular16', 'regular24']}
              color="grey.800"
              textAlign={['initial', 'initial', 'center']}
              dangerouslySetInnerHTML={{ __html: text }}
            />
          </Center>
        ))}
      </Wrap>

      <Button
        width={['100%', '100%', 'auto']}
        mt="39px"
        onClick={onShowSimulador}
      >
        Fazer Simulação
      </Button>
    </Center>
  );
};
