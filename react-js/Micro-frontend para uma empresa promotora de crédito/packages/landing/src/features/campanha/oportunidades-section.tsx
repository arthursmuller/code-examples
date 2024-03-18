import { FC } from 'react';

import { Box, Text, Wrap, Button } from '@chakra-ui/react';

import { BemImage } from 'features/components/images';

import { CutMoney, GrowGraph, MoneyInHand, TouchSmartphone } from './assets';

const oportunidades = [
  {
    icon: TouchSmartphone,
    text: 'A <b>melhor Portabilidade</b> do mercado! Traga seu contrato para a Bem Promotora!',
    alt: 'ilustação smartphone',
  },
  {
    icon: GrowGraph,
    text: '<b>Aumento de 5%</b> na margem de crédito. Aproveite!',
    alt: 'ilustação gráfico',
  },
  {
    icon: MoneyInHand,
    text: 'Faça a sua simulação de <b>Crédito Consignado INSS e SIAPE.</b>',
    alt: 'ilustação dinheiro na mão',
  },
  {
    icon: CutMoney,
    text: 'Simule uma proposta e usufrua das nossas <b>taxas competitivas.</b>',
    alt: 'ilustação cortando dinheiro das taxas',
  },
];

export const OportunidadesSection: FC = () => {
  function scrollToCpf(): void {
    const elements = document.getElementsByName('cpf');

    if (elements?.length) {
      elements[0].scrollIntoView({ block: 'start', behavior: 'smooth' });
      elements[0].focus();
    }
  }

  return (
    <Box>
      <Text
        color="secondary.mid-dark"
        textStyle="bold32_40"
        textAlign="center"
        mt="80px"
      >
        Diversas oportunidades de Crédito Consignado
      </Text>

      <Text textStyle="regular20" color="grey.800" mt={8} textAlign="center">
        Bem Promotora ajuda você a realizar os seus sonhos!
      </Text>

      <Wrap mt="43px" spacing={6} mx={[0, 0, 0, 14]} justify="center">
        {oportunidades.map(({ icon, text, alt }) => (
          <Box
            layerStyle="card"
            key={text}
            w={['100%', '100%', '569px']}
            minH="186px"
          >
            <BemImage src={icon} width="60px" height="60px" alt={alt} />
            <Text
              mt="20px"
              color="grey.800"
              textStyle="regular24"
              dangerouslySetInnerHTML={{ __html: text }}
            />
          </Box>
        ))}
      </Wrap>

      <Box textAlign="center" mt={[8, 8, 8, '54px']}>
        <Button onClick={scrollToCpf}>Faça já sua simulação</Button>
      </Box>
    </Box>
  );
};
