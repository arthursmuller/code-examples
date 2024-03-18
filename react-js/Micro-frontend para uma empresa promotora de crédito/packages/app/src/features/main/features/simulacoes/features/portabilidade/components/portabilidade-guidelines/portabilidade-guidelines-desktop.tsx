import { FC } from 'react';

import { Button, Flex, Icon, Text, Image, IconButton } from '@chakra-ui/react';
import { motion } from 'framer-motion';

import { CustomHeading } from '@pcf/design-system';
import { HelpIcon, StatusCloseErrorIcon } from '@pcf/design-system-icons';

import Flow from './flow.svg';
import { PortabilidadeGuidelinesProps } from './portabilidade-guideline-props';

const PortabilidadeGuidelines: FC<PortabilidadeGuidelinesProps> = ({
  onClose,
}) => (
  <Flex
    height="100%"
    overflow="hidden"
    paddingTop="130px"
    as={motion.div}
    initial={{ opacity: 0, x: 150 }}
    animate={{
      opacity: 1,
      x: 0,
      transition: { duration: 0.5 },
    }}
    exit={{ opacity: 0, x: 150, flex: 0, transition: { duration: 0.5 } }}
  >
    <Flex marginLeft="70px" maxWidth="500px">
      <Flex direction="column" overflowY="auto" paddingRight={2}>
        <Flex
          direction="column"
          height="fit-content"
          paddingLeft={10}
          paddingRight={5}
          paddingY={10}
          color="grey.100"
          border="1px solid"
          borderColor="grey.100"
          borderRadius="24px"
          position="relative"
          marginBottom={5}
        >
          <IconButton
            aria-label="close"
            variant="link"
            size="sm"
            position="absolute"
            top={4}
            right={2}
            onClick={onClose}
            icon={<Icon as={StatusCloseErrorIcon} fill="grey.100" />}
          />

          <CustomHeading as="h3" textStyle="bold20" marginBottom="8px">
            Como Funciona
          </CustomHeading>

          <Text>
            Na Portabilidade você traz contratos de outros bancos para a Bem.
            Com possibilidade de reduzir a sua parcela e incluir uma intenção de
            refinanciamento.
          </Text>

          <Image src={Flow} minWidth={['300px', '300px', '340px']} />

          <Flex
            alignItems="center"
            height="56px"
            marginX={[4, 4, 0]}
            marginTop={[-3, -3, 0]}
          >
            <Icon as={HelpIcon} marginRight="12px" />
            <Button
              variant="link"
              as="a"
              color="primary.regular"
              target="_blank"
              textDecoration="underline"
              disabled
            >
              Não sabe como proceder? Clique aqui!
            </Button>
          </Flex>
        </Flex>
      </Flex>
    </Flex>
  </Flex>
);

export default PortabilidadeGuidelines;
