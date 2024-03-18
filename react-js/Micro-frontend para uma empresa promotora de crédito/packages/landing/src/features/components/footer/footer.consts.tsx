import { Icon, Stack, Text, Button, Link } from '@chakra-ui/react';

import {
  RecruitmentOldIcon,
  PhonecallIcon,
  TabPropostasInativaIcon,
  SpeechbubbleIcon,
} from '@pcf/design-system-icons';
import { RoutesEnum } from 'app/routes/routes.enum';

const BEM_PROMOTORA_URL = 'https://www.bempromotora.com.br';

export const LIST_FOOTER_ITEMS = [
  {
    icon: (
      <Icon
        as={TabPropostasInativaIcon}
        w="23px"
        h="28px"
        color="primary.light"
      />
    ),
    title: 'Termos',
    items: [
      {
        id: 1,
        text: 'Acesso do Correspondente',
        link: `https://bemweb.bempromotora.com.br/autenticacao/login`,
      },
      {
        id: 2,
        text: 'Política de Privacidade',
        link: RoutesEnum.PrivacyPolicy,
        isInternalLink: true,
      },
      {
        id: 3,
        text: 'Termos e Condições',
        link: RoutesEnum.TermsAndConditions,
        isInternalLink: true,
      },
      {
        id: 4,
        text: 'Bemsign',
        link: RoutesEnum.BemSign,
        isInternalLink: true,
      },
    ],
  },
  {
    icon: <Icon as={RecruitmentOldIcon} boxSize={8} color="primary.light" />,
    title: 'Não encontrou o que procurava?',
    items: [
      {
        id: 1,
        text: 'Sobre a Bem',
        link: RoutesEnum.About,
        isInternalLink: true,
      },
      {
        id: 2,
        text: 'Encontre Uma Loja',
        link: RoutesEnum.Stores,
        isInternalLink: true,
      },
      {
        id: 3,
        text: 'Trabalhe Conosco',
        link: RoutesEnum.WorkingWithUs,
        isInternalLink: true,
      },
      {
        id: 4,
        text: 'Seja Correspondente',
        link: RoutesEnum.Correspondent,
        isInternalLink: true,
      },
      {
        id: 5,
        text: 'Site Institucional',
        link: `${BEM_PROMOTORA_URL}`,
      },
      {
        id: 6,
        text: 'Meus dados',
        link: RoutesEnum.PersonalData,
        isInternalLink: true,
      },
      {
        id: 7,
        text: 'Mapa do site',
        link: `${BEM_PROMOTORA_URL}/mapa-do-site`,
      },
      {
        id: 8,
        text: 'Governança Corporativa',
        link: RoutesEnum.CooperativeGovernance,
        isInternalLink: true,
      },
      {
        id: 9,
        text: 'LGPD',
        link: RoutesEnum.LGPD,
        isInternalLink: true,
      },
    ],
  },
  {
    icon: <Icon as={PhonecallIcon} w="26px" h="26px" color="primary.light" />,
    title: 'Atendimento para Clientes',
    items: [
      // será usado no futuro
      // {
      //   id: 1,
      //   content: (
      //     <Text as="p" textStyle="regular16" color="white">
      //       Perguntas frequentes
      //     </Text>
      //   ),
      // },
      // {
      //   id: 2,
      //   content: (
      //     <Text as="p" textStyle="regular16" color="white">
      //       Canal de denúncia
      //     </Text>
      //   ),
      // },
      {
        id: 3,
        content: (
          <Stack direction={['column', 'column', 'row']} spacing={[0, 0, 1]}>
            <Text as="p" textStyle="regular16" color="white">
              Capitais e regiões metropolitanas:
            </Text>
            <Text as="p" textStyle="regular16" color="white">
              3003 0511
            </Text>
          </Stack>
        ),
      },
      {
        id: 4,
        content: (
          <Stack direction={['column', 'column', 'row']} spacing={[0, 0, 1]}>
            <Text as="p" textStyle="regular16" color="white">
              Demais localidades:
            </Text>
            <Text as="p" textStyle="regular16" color="white">
              0800 72 00 011
            </Text>
          </Stack>
        ),
      },
      {
        id: 5,
        content: (
          <Stack direction={['column', 'column', 'row']} spacing={[0, 0, 1]}>
            <Text as="p" textStyle="regular16" color="white">
              Deficientes auditivos/fala:
            </Text>
            <Text as="p" textStyle="regular16" color="white">
              0800 648 1907
            </Text>
          </Stack>
        ),
      },
      {
        id: 6,
        content: (
          <Stack direction="column" spacing={0}>
            <Text as="p" textStyle="regular16" color="white">
              SAC: 0800 646 1515
            </Text>
            <Link
              color="primary.light"
              isExternal
              href="https://www.banrisul.com.br/bob/link/seg/bobw00hn_sac_seg.aspx?secao_id=35"
            >
              Envie sua mensagem
              <Icon
                ml={2}
                as={SpeechbubbleIcon}
                color="primary.light"
                w="13px"
                h="13px"
              />
            </Link>
          </Stack>
        ),
      },
      {
        id: 7,
        content: (
          <Stack direction="column" spacing={0}>
            <Text as="p" textStyle="regular16" color="white">
              Ouvidoria: 0800 644 2200
            </Text>
            <Link
              color="primary.light"
              isExternal
              href="https://www.banrisul.com.br/bob/link/seg/bobw00hn_ouvidoria_seg.aspx?secao_id=35"
            >
              Envie sua mensagem
              <Icon
                ml={2}
                as={SpeechbubbleIcon}
                color="primary.light"
                w="13px"
                h="13px"
              />
            </Link>
          </Stack>
        ),
      },
      {
        id: 8,
        content: (
          <Button
            colorScheme="grey"
            mt="20px"
            as={Link}
            isExternal
            href="https://chat2.banrisul.com.br:7090/voswebchatserver/pages/clientPopup.jsp?flagid=crcbem"
            rightIcon={<Icon as={SpeechbubbleIcon} color="primary.regular" />}
          >
            Atendimento online
          </Button>
        ),
      },
    ],
  },
];

export const BEM_PROMOTORA_LINKS = {
  title: '',
  icon: null,
  items: [
    {
      id: 1,
      text: 'Empréstimo Consignado',
      link: RoutesEnum.CreditoConsignado,
      isInternalLink: true,
    },
    {
      id: 2,
      text: 'Funcionário Público',
      link: RoutesEnum.FuncionarioPublico,
      isInternalLink: true,
    },
    {
      id: 3,
      text: 'Aposentados e Pensionistas',
      link: RoutesEnum.AposentadosPensionistas,
      isInternalLink: true,
    },
    {
      id: 4,
      text: 'Cartão Consignado',
      link: RoutesEnum.CartaoConsignado,
      isInternalLink: true,
    },
    {
      id: 5,
      text: 'Título de Capitalização',
      link: RoutesEnum.TituloCapitalizao,
      isInternalLink: true,
    },
  ],
};
