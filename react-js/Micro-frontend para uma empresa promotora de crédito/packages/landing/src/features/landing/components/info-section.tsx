import { Children, FC } from 'react';

import { Flex, Box, Text, BoxProps, FlexProps } from '@chakra-ui/react';

import { CustomHeading } from '@pcf/design-system';

interface InfoSectionTitleProps {
  subtitle?: string;
  headingProps?: BoxProps;
}

interface InfoSectionDescriptionProps {
  afterText?: string;
  showSlogan?: boolean;
}

interface InfoSectionComposition {
  Title: FC<InfoSectionTitleProps>;
  Description: FC<InfoSectionDescriptionProps>;
}

const InfoSectionTitle: FC<InfoSectionTitleProps> = ({
  subtitle = 'Bem Promotora',
  headingProps = {},
  children,
}) => {
  return (
    <Box>
      <CustomHeading
        w={['100%', '100%', '100%', '100%', '363px']}
        ml={[0, 0, 0, 0, '110px']}
        mr={[0, 0, 0, 0, '75px']}
        as="h2"
        textStyle={['bold24', 'bold24', 'bold40']}
        marginBottom={[2, 2, 6]}
        {...headingProps}
      >
        <Box as="span" color="secondary.mid-dark">
          {children}
        </Box>{' '}
        <br />
        <Box as="span" color="primary.regular">
          {subtitle}
        </Box>
      </CustomHeading>
    </Box>
  );
};

const InfoSectionDescription: FC<InfoSectionDescriptionProps> = ({
  afterText = 'Quem tem Crédito Consignado, fica bem!',
  showSlogan = true,
  children,
}) => {
  return (
    <Box mr={[0, 0, 0, '70px']}>
      {children}

      {showSlogan && (
        <Text
          textStyle="regular20"
          color="grey.700"
          mt={[2, 2, 2, 8]}
          lineHeight="28px"
        >
          A Bem Promotora é referência em agilidade e atendimento, contando com
          os sistemas e processos mais ágeis do mercado.
        </Text>
      )}

      {afterText && (
        <Text
          textStyle="bold20"
          color="grey.700"
          mt={[2, 2, 2, 8]}
          lineHeight="28px"
        >
          {afterText}
        </Text>
      )}
    </Box>
  );
};

const InfoSection: FC<FlexProps> & InfoSectionComposition = ({
  children,
  ...flexProps
}) => {
  const moreThanOneChildren = Children.count(children) > 1;

  return (
    <Flex
      direction={['column', 'column', 'column', 'column', 'row']}
      layerStyle="card"
      alignItems={[
        'flex-start',
        'flex-start',
        'flex-start',
        'flex-start',
        moreThanOneChildren ? 'center' : 'flex-start',
      ]}
      p={[6, 6, 8]}
      marginTop={['-100px', '-100px', '-20px', '-20px']}
      marginX={[5, 5, '74px']}
      minH={moreThanOneChildren ? '368px' : 0}
      marginBottom={moreThanOneChildren ? '56px' : 0}
      {...flexProps}
    >
      {children}
    </Flex>
  );
};

InfoSection.Title = InfoSectionTitle;
InfoSection.Description = InfoSectionDescription;

export { InfoSection };
