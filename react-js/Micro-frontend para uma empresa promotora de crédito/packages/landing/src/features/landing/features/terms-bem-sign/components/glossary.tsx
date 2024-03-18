import { FC } from 'react';

import {
  useBreakpointValue,
  Text,
  UnorderedList,
  ListItem,
} from '@chakra-ui/react';

import { CustomHeading } from '@pcf/design-system';

const GLOSSARY = [
  {
    id: 1,
    title: 'BEMSIGN',
    description:
      'É uma plataforma especializada em assinatura eletrônica na  contratação de serviços e/ou produtos. Está em conformidade com a Medida Provisória 2200/2001.',
  },
  {
    id: 2,
    title: 'DOCUMENTO',
    description:
      'É o instrumento que será assinado pelo Titular para fins de obtenção e/ou validação do serviço e/ou produto pretendido.',
  },
  {
    id: 3,
    title: 'DADOS PESSOAIS',
    description:
      'Informação relacionada a pessoa natural identificada ou identificável;',
  },
  {
    id: 4,
    title: 'DADOS PESSOAIS SENSÍVEIS',
    description:
      'Dado pessoal sobre origem racial ou étnica, convicção religiosa, opinião política, filiação a sindicato ou a organização de caráter religioso, filosófico ou político, dado referente à saúde ou à vida sexual, dado genético ou biométrico, quando vinculado a uma pessoa natural;',
  },
  {
    id: 5,
    title: 'DADOS',
    description:
      'Dados pessoais e dados pessoais sensíveis, quando referidos conjuntamente, ou verificar se por DADOS existem outros tipos de informações a que se referem.',
  },
  {
    id: 6,
    title: 'LGPD',
    description: 'Lei nº 13.709/2018 – Lei Geral de Proteção de Dados.',
  },
  {
    id: 7,
    title: 'TERMO DE USO',
    description: 'O presente instrumento.',
  },
  {
    id: 8,
    title: 'TITULAR',
    description:
      'Pessoa natural a quem se referem os dados pessoais que são objeto de tratamento. Neste Termo de Uso, também entendido como “Usuário”;',
  },
  {
    id: 9,
    title: 'TRATAMENTO',
    description:
      'Toda operação realizada com dados pessoais, como as que se referem a coleta, produção, recepção, classificação, utilização, acesso, reprodução, transmissão, distribuição, processamento, arquivamento, armazenamento, eliminação, avaliação ou controle da informação, modificação, comunicação, transferência, difusão ou extração;',
  },
  {
    id: 10,
    title: 'USUÁRIO',
    description:
      'Significa uma pessoa física capaz de celebrar contrato legalmente vinculante segundo a legislação aplicável, que use o BEMSIGN por si.',
  },
];

export const Glossary: FC = () => {
  const isMobile = useBreakpointValue({ base: true, md: false });

  return (
    <>
      <CustomHeading
        mt={[8, 8, 14]}
        textStyle={isMobile ? 'bold20' : 'bold32'}
        color="secondary.regular"
        as="h3"
      >
        GLOSSÁRIO:
      </CustomHeading>

      <Text textStyle="regular16_20" mt={6}>
        Para o correto entendimento do sentido do presente Termo de Uso, além do
        disposto na legislação pertinente, as expressões grafadas com a letra
        inicial maiúscula terão a seguinte definição:
      </Text>

      <UnorderedList mt={8} pl={[4, 4, 6]} pb={8}>
        {GLOSSARY.map(({ id, title, description }) => (
          <ListItem key={id}>
            <Text
              color="grey.800"
              textStyle={isMobile ? 'bold16' : 'bold24'}
              my={8}
            >
              {title}
            </Text>
            <Text ml={[-5, -5, -7]} textStyle="regular16_20">
              {description}
            </Text>
          </ListItem>
        ))}
      </UnorderedList>
    </>
  );
};
