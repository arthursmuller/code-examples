import { FC } from 'react';

import {
  useBreakpointValue,
  Flex,
  Text,
  Wrap,
  WrapItem,
  WrapProps,
  Center,
} from '@chakra-ui/react';

import { SwipeableContainer, CustomHeading } from '@pcf/design-system';
import { BemImage } from 'features/components/images';

import People from '../assets/icon-people.svg';
import Target from '../assets/icon-target.svg';
import Star from '../assets/icon-star.svg';
import Handshake from '../assets/icon-handshake.svg';
import Team from '../assets/icon-team.svg';
import Cogs from '../assets/icon-cogs.svg';

export interface BadgeProps {
  title: string;
  altDescription: string;
  text?: string;
  icon?: FC;
  animatedIcon?: any;
  iconSize?: string | string[];
  width?: string | string[];
  alignBadge?: 'center' | 'start';
}

export interface BadgesSectionProps {
  badges?: BadgeProps[];
  iconsSize?: string;
  widths?: string | string[];
  chakraProps?: WrapProps;
  alignBadges?: 'center' | 'start';
}

const DEFAULT_BADGES: BadgeProps[] = [
  {
    icon: People,
    title: 'PESSOAS FELIZES E BEM ACOLHIDAS',
    text: 'Valorizamos o bem-estar das pessoas acima de tudo, promovendo um ambiente de acolhimento às diferenças e respeito à liberdade de cada um.',
    altDescription: 'Um grupo de pessoas',
  },
  {
    icon: Target,
    title: 'OBJETIVOS BEM CLAROS PARA TODOS',
    text: 'Olhamos para o futuro executando com eficiência o nosso planejamento presente, com empenho de todas as pessoas da organização.',
    altDescription: 'um dardo em cheio no alvo',
  },
  {
    icon: Team,
    title: 'COOPERAÇÃO PARA ENTREGAR O BEM',
    text: 'Estimulamos um ambiente onde as pessoas sintam-se próximas e confiantes para contribuírem umas com as outras, trabalhando juntas para entregarem seu melhor.',
    altDescription: '3 mãos de pessoas diferentes unidas',
  },
  {
    icon: Star,
    title: 'BEM DEDICADOS AOS RESULTADOS',
    text: 'Focados na obtenção de resultados, estamos sempre em busca de melhorias que otimizem nossa entrega e acelerem nosso crescimento.',
    altDescription: 'uma estrela',
  },
  {
    icon: Cogs,
    title: 'TECNOLOGIA PARA BEM ATENDER',
    text: 'Investimos constantemente para manter uma estrutura de TI robusta e de ponta, pilotada por uma equipe eficiente e de alta performance.',
    altDescription: 'um notebook com engrenagens',
  },
  {
    icon: Handshake,
    title: 'SERIEDADE É O NOSSO MAIOR BEM',
    text: 'Somos éticos no trato com todos e íntegros no cumprimento das regras e normas do negócio. Isso é parte incontestável da nossa identidade!',
    altDescription: 'aperto de mão entre duas pessoas',
  },
];

const Badge: FC<BadgeProps & { alignBadge: 'start' | 'center' }> = ({
  title,
  text,
  icon,
  // animatedIcon,
  iconSize,
  width,
  alignBadge,
  altDescription,
}) => {
  const isMobile = useBreakpointValue({ base: true, md: false });

  const titleTextStyle = isMobile ? 'bold20' : 'bold24';
  const informationTextStyle = isMobile ? 'regular16' : 'regular16';

  return (
    <Flex
      layerStyle="card"
      borderRadius="20px"
      direction="column"
      alignItems={alignBadge}
      textAlign={alignBadge}
      justifyContent={text ? ['start', 'start', 'center'] : 'start'}
      minH={text ? ['212px', '212px', '280px'] : 'fit-content'}
      w={width || ['212px', '212px', '288px']}
      height="100%"
    >
      {icon && (
        <BemImage
          width={iconSize || ['74px', '74px', '90px']}
          height={iconSize || ['74px', '74px', '90px']}
          alt={altDescription}
          src={icon}
        />
      )}

      <Flex direction="column" mt={[1, 1, 3]}>
        <CustomHeading
          textStyle={titleTextStyle}
          color="primary.regular"
          marginBottom={2}
          whiteSpace="pre-wrap"
        >
          {title}
        </CustomHeading>

        {text && (
          <Text
            textStyle={informationTextStyle}
            mt={[0, 0, 3]}
            color="grey.700"
          >
            {text}
          </Text>
        )}
      </Flex>
    </Flex>
  );
};

export const BadgesSection: FC<BadgesSectionProps> = ({
  badges = DEFAULT_BADGES,
  iconsSize,
  widths,
  chakraProps,
  alignBadges = 'start',
}) => {
  const isMobile = useBreakpointValue({ base: true, md: false });

  return isMobile ? (
    <Flex marginBottom="65px" marginX={5}>
      <SwipeableContainer schemeColor="secondary">
        {badges.map((badge) => (
          <Badge key={badge.title} {...badge} alignBadge={alignBadges} />
        ))}
      </SwipeableContainer>
    </Flex>
  ) : (
    <Center>
      <Wrap
        mb="81px"
        spacing={6}
        justify="center"
        mx={[8, 8, 0]}
        {...chakraProps}
      >
        {badges.map((badge) => (
          <WrapItem key={badge.title}>
            <Badge
              {...badge}
              iconSize={iconsSize}
              width={widths}
              alignBadge={alignBadges}
            />
          </WrapItem>
        ))}
      </Wrap>
    </Center>
  );
};
