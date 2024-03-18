import { FC } from 'react';

import { Text, Flex, Icon } from '@chakra-ui/react';

import { CustomHeading } from '@pcf/design-system';
import { BadgesSection } from 'features/landing/components/badges-section';
import { LogoBemVerticalIcon } from '@pcf/design-system-icons';

import { WorkingWithUsSection } from './working-with-us-section';

export const Culture: FC = () => {
  return (
    <Flex flexDir="column" mt={4}>
      <Flex
        layerStyle="card"
        p={[6, 6, '40px 38px']}
        mb="76px"
        alignItems="center"
        flexDir={['column', 'column', 'row']}
      >
        <Flex flexDir="column" alignItems="center">
          <CustomHeading
            textStyle="bold32_40"
            color="secondary.mid-light"
            textAlign="center"
          >
            Nosso Manifesto
          </CustomHeading>

          <Icon
            as={LogoBemVerticalIcon}
            w="108px"
            h="138px"
            mt={[6, 6, '48px']}
          />
        </Flex>

        <Text
          textStyle="regular16_20"
          ml={[0, 0, '53px']}
          mt={[6, 6, 0]}
          textAlign={['center', 'center', 'start']}
        >
          Somos uma Promotora de Vendas e Serviços que nasceu em 30 de setembro
          de 2008, especializada em crédito consignado. Mas muito mais do que
          prestar serviços com excelência, ser Bem Promotora é promover o
          bem-estar das pessoas acima de tudo.{' '}
          <Text as="span" textStyle="bold16_20" color="primary.regular">
            E quer melhor jeito de fazer isso do que ajudando-as a realizarem
            suas conquistas?{' '}
          </Text>
          Na Bem usamos a tecnologia, nossa experiência em vencer desafios e o
          talento em aproveitar as melhores oportunidades para gerar satisfação
          e qualidade de vida a todas as pessoas com quem nos relacionamos.{' '}
          <Text as="strong" color="primary.regular">
            É por isso que, tão sério quanto o nosso trabalho, é o nosso desejo
            de oportunizar um ambiente onde todos tenham a chance de evoluir e
            realizar seus sonhos.
          </Text>
        </Text>
      </Flex>

      <Flex flexDir="column">
        <Text
          textStyle="regular24_32"
          color="secondary.mid-dark"
          textAlign="center"
        >
          Nosso propósito:
        </Text>
        <Text
          textStyle="bold32_40"
          color="secondary.mid-dark"
          textAlign="center"
          mt={6}
        >
          Transformar oportunidades em realizações.
        </Text>
        <Text
          textStyle="regular16_20"
          textAlign="center"
          mt={['29px', '29px', '48px']}
          mb="42px"
        >
          O jeito de ser da BEM tem os seguintes pilares culturais:
        </Text>
      </Flex>

      <BadgesSection
        iconsSize="64px"
        widths={['212px', '212px', '335px']}
        chakraProps={{
          maxWidth: '1200px',
        }}
      />

      <WorkingWithUsSection />
    </Flex>
  );
};
