import { FC } from 'react';

import { Center, Flex, Icon, Text } from '@chakra-ui/react';

interface PortabilidadeTypeOptionProps {
  title: string;
  descriptions: { text: string; icon: FC }[];
}

export const PortabilidadeTypeOption: FC<PortabilidadeTypeOptionProps> = ({
  title,
  descriptions,
  children,
}) => (
  <Flex flexGrow={1} direction="column" marginBottom={2}>
    <Flex
      direction="column"
      alignItems="center"
      justifyContent="center"
      borderRadius="24px"
      border={['none', 'none', '3px solid']}
      borderColor="secondary.regular"
      padding={5}
      marginBottom={[0, 0, 5]}
      color="primary.regular"
    >
      <Text
        textStyle={['bold20', 'bold20', 'bold24']}
        textAlign="center"
        marginBottom={5}
      >
        {title}
      </Text>

      {descriptions?.map(({ text, icon }, index) => (
        <Flex
          direction={['row', 'row', 'column']}
          alignItems="center"
          justifyContent="center"
          marginBottom={5}
          key={`${index}`}
          color="secondary.regular"
        >
          <Center
            borderRadius="full"
            border="2px solid"
            borderColor="secondary.regular"
            padding={4}
            marginBottom={[0, 0, 4]}
            marginX={2}
          >
            <Icon as={icon} width={9} height={9} />
          </Center>

          <Text
            textStyle={['regular16', 'regular16', 'regular20']}
            textAlign={['start', 'start', 'center']}
          >
            {text}
          </Text>
        </Flex>
      ))}
    </Flex>

    {children}
  </Flex>
);
