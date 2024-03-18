import { FC } from 'react';

import { Center, Flex, Text, Icon } from '@chakra-ui/react';

import { TelephoneFillIcon } from '@pcf/design-system-icons';
import { CustomHeading } from '@pcf/design-system';

export interface ContactLine {
  number: string;
  title: string;
}

const AsLink: FC = ({ children }) => <a href={`tel:${children}`}>{children}</a>;

const ContactLine: FC<ContactLine> = ({ number, title }) => (
  <Flex alignItems="baseline">
    <Center
      marginRight={4}
      color="white"
      background="secondary.gradient"
      borderRadius="12px"
      padding="3px"
    >
      <Icon as={TelephoneFillIcon} />
    </Center>

    <Flex direction="column">
      <Text textStyle="bold24" color="primary.regular" marginRight={1}>
        <AsLink>{number}</AsLink>
      </Text>
      <Text textStyle="regular16" color="grey.700">
        {title}
      </Text>
    </Flex>
  </Flex>
);

export const ContactsSection: FC<{
  contacts: ContactLine[];
  subLabel?: boolean | string;
}> = ({ contacts, subLabel = true }) => (
  <Center marginBottom={[8, 8, 20]} marginTop={[0, 0, 12]} marginX={8}>
    <Flex direction="column">
      <Flex direction="column" marginBottom={4}>
        <CustomHeading
          as="h4"
          textStyle="bold32"
          marginRight={1}
          color="secondary.mid-dark"
        >
          Central de Relacionamento com Clientes
        </CustomHeading>
        {subLabel && (
          <Text
            textStyle={['regular14', 'regular14', 'regular20']}
            color="grey.700"
          >
            {typeof subLabel === 'string'
              ? subLabel
              : '(Vida Segura Premiada – Seguro AP):'}
          </Text>
        )}
      </Flex>
      <Flex direction="row" flexWrap="wrap" style={{ gap: '20px' }}>
        {!!contacts &&
          contacts.map((line, index) => (
            <ContactLine {...line} key={`${index}`} />
          ))}
      </Flex>
    </Flex>
  </Center>
);
