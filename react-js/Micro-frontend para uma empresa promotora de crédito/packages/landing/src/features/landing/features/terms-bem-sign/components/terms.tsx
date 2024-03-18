import { FC } from 'react';

import {
  ListItem,
  OrderedList,
  UnorderedList,
  Text,
  Box,
} from '@chakra-ui/react';

const TERMS = [
  {
    id: 1,
    text: 'ACEITAÇÃO',
    items: [
      {
        id: 1,
        text: 'Este documento contém informações legais importantes à utilização da BEMSIGN, especificando as hipóteses de tratamento de seus dados       pessoais durante a utilização de nossos serviços, portanto é       indispensável a sua leitura e compreensão. CASO NÃO ACEITE QUALQUER DAS DISPOSIÇÕES AQUI CONTIDAS, POR FAVOR DESCONTINUE O ACESSO.',
      },
      {
        id: 2,
        text: 'Ao acessar o BEMSIGN ou utilizar quaisquer de suas funcionalidades, o usuário manifesta a sua vontade ao presente Termo de Uso e Privacidade de forma livre, expressa e informada, declarando sua plena, integral e irrestrita concordância com as condições nele previstas.',
      },
    ],
  },
  {
    id: 2,
    text: 'ACESSO E CADASTRO',
    items: [
      {
        id: 1,
        text: 'Para acesso e utilização das funcionalidades/serviços da BEMSIGN, o USUÁRIO deve estar conectado à rede mundial de computadores, utilizando-se de web browser (navegador) atualizado.',
      },
      {
        id: 2,
        text: 'Para utilização da BEMSIGN, o USUÁRIO deverá efetuar cadastro prévio através de algum canal de relacionamento da BEM PROMOTORA. A veracidade dos dados informados no cadastro será verificada e na hipótese de haver dados incorretos ou inverídicos, a conta poderá ser bloqueada ou cancelada definitivamente, sem prejuízo de outras medidas que a BEM PROMOTORA julgar necessárias e oportunas, sem necessidade de comunicação prévia, não cabendo ao usuário qualquer indenização ou ressarcimento.',
      },
      {
        id: 3,
        text: 'O cadastramento para utilização da BEMSIGN não será cobrado.',
      },
      {
        id: 4,
        text: 'Após a realização do cadastro, o USUÁRIO passará a utilizar-se do número de celular e dados cadastrados, para dispor de todos os recursos disponibilizados pela BEMSIGN. As credenciais de acesso são de uso exclusivo, pessoal e intransferível do USUÁRIO que realizou o cadastro.',
      },
      {
        id: 5,
        text: 'A BEM PROMOTORA poderá exigir através de mecanismos específicos de autenticação, a validação de identidade do USUÁRIO, podendo utilizar-se para tal, de meios como o número de telefone informado pelo USUÁRIO.',
      },
      {
        id: 6,
        text: 'O USUÁRIO assume neste ato integral responsabilidade pela guarda, sigilo e segurança de suas credenciais de acesso, devendo tomar todas as cautelas necessárias para evitar o seu uso indevido por terceiros.',
      },
      {
        id: 7,
        text: 'Ao utilizar a BEMSIGN, o USUÁRIO declara estar plenamente ciente de que será o único responsável pelas operações realizadas através de suas credenciais de acesso.',
      },
      {
        id: 8,
        text: 'O USUÁRIO fica ciente que, sempre que houver suspeitas ou indícios de ato ilícito ou descumprimento às regras deste Termo de Uso, a BEMSIGN se reserva o direito de bloquear o acesso, suspendê-lo temporariamente ou cancelá-lo definitivamente, sem prejuízo de outras medidas que entender necessárias e oportunas, sem necessidade de notificação prévia do USUÁRIO.',
      },
    ],
  },
  {
    id: 3,
    text: 'DO ARMAZENAMENTO DOS DOCUMENTOS',
    items: [
      {
        id: 1,
        text: 'Os documentos serão criptografados e armazenados com segurança e privacidade pelo período de 5 (cinco) anos. Expirado o período de armazenamento definido acima, os documentos poderão ser deletados sem aviso prévio ao usuário.',
      },
    ],
  },
  {
    id: 4,
    text: 'TRATAMENTO E EXCLUSÃO DE DADOS PESSOAIS, E CONECTIVIDADE DA BEMSIGN',
    items: [
      {
        id: 1,
        text: 'DADOS PESSOAIS COLETADOS E FINALIDADES',
        items: [
          {
            id: 1,
            text: 'Durante a execução dos serviços disponibilizados na BEMSIGN, poderão ser coletados os seguintes dados pessoais do USUÁRIO:',
          },
          {
            id: 2,
            text: 'Nome completo, data de nascimento e CPF: Utilizado para fins cadastrais, bem como verificação de autenticidade e validação de identidade e em mecanismos de prevenção a fraude e garantia de segurança do usuário.',
          },
          {
            id: 3,
            text: 'Biometria Facial: Utilizada como método de manifestação da vontade ao fim do processo de assinatura, demonstrando a concordância do usuário com as condições estabelecidas.',
          },
          {
            id: 4,
            text: 'Número de celular e e-mail: Utilizados para verificação de autenticidade e validação de identidade e para fins cadastrais.',
          },
          {
            id: 5,
            text: 'Durante os serviços disponibilizados na BEMSIGN, poderão ser coletados os seguintes sistêmicos do dispositivo utilizado pelo USUÁRIO:',
          },
          {
            id: 6,
            text: 'Senha, código de acesso, IP e geolocalização: para validação de identidade, autenticação da operação a ser realizada e em mecanismos de prevenção a fraude e garantia de segurança do USUÁRIO.',
          },
          {
            id: 7,
            text: 'Todos os dados acima descritos poderão ser gravados e atribuídos permanentemente ao documento que o USUÁRIO assinar, bem como constarem em laudo probatório da operação.',
          },
        ],
      },
      {
        id: 2,
        text: 'HIPÓTESES DE COMPARTILHAMENTO DE DADOS',
        items: [
          {
            id: 1,
            text: 'Os Dados coletados e as atividades registradas podem ser compartilhados, sempre respeitando o envio do mínimo de informações necessárias para atingir as finalidades:',
          },
          {
            id: 2,
            text: 'Com as empresas parceiras e terceiros necessários à prestação dos serviços, como por exemplo empresas que nos apoiem no processo de prevenção de fraudes e verificação de identidade, sempre exigindo de tais empresas e terceiros o cumprimento das diretrizes de segurança e proteção de dados previstas neste Termo;',
          },
          {
            id: 3,
            text: 'Com autoridades judiciais, administrativas ou governamentais competentes, sempre que houver determinação legal, requerimento, requisição ou ordem judicial; e',
          },
          {
            id: 4,
            text: 'De forma automática, em caso de movimentações societárias, como fusão, aquisição e incorporação.',
          },
          {
            id: 5,
            text: 'Caso empresas terceirizadas realizem o tratamento em nome da BEM PROMOTORA de quaisquer Dados Pessoais coletados, as mesmas respeitarão as condições aqui estipuladas e as normas de segurança da informação, obrigatoriamente.',
          },
          {
            id: 6,
            text: 'Os Dados também poderão ser compartilhados com o banco parceiro e/ou terceiros que precisem ter acesso a tais informações com a finalidade de concluir a operação de concessão de crédito ou contratação de serviços e/ou produtos, ou ainda para cumprir normas que lhe são aplicáveis, tais como órgãos reguladores, serviços e/ou produtos de compensação, serviços de avaliação de perfil e risco de fraude do usuário, dentre outros que possam ser necessários durante o processo de concessão do crédito ao usuário.',
          },
        ],
      },
      {
        id: 3,
        text: 'ARMAZENAMENTO E TÉRMINO DO TRATAMENTO DOS DADOS COLETADOS',
        items: [
          {
            id: 1,
            text: 'Os Dados Pessoais coletados e os registros de atividades serão armazenados em ambiente seguro e controlado por um prazo mínimo de acordo com a finalidade de cada um, sendo que:',
          },
          {
            id: 2,
            text: 'Os Dados utilizados para fins cadastrais serão armazenados pelo período de 5 (cinco) anos após o término da relação, com base no disposto no artigo 43, parágrafo 1º, do Código de Defesa do Consumidor.',
          },
          {
            id: 3,
            text: 'Os Dados utilizados para fins de identificação digital, como autenticação, checagem de identidade e verificação da operação, serão armazenados pelo prazo de 6 (seis) meses a partir da coleta, nos termos do disposto no artigo 15 do Marco Civil da Internet.',
          },
          {
            id: 4,
            text: 'Quanto aos demais Dados, haverá o término do tratamento nas hipóteses do artigo 15 da LGPD, ou seja, dentre outros, quando alcançada a finalidade para a qual foram coletados; se deixarem de ser necessários ou pertinentes ao alcance da finalidade almejada; ou por meio de comunicação de exclusão por parte do Usuário.',
          },
          {
            id: 5,
            text: 'Todos os dados coletados serão armazenados em servidores localizados no Brasil.',
          },
        ],
      },
      {
        id: 4,
        text: 'DIREITOS DO TITULAR DE DADOS PESSOAIS',
        content: (
          <Box my={4}>
            <Text my={4}>
              Para fins de atendimento aos direitos consagrados nos artigos 17 e
              seguintes da LGPD, o USUÁRIO terá direito a:
            </Text>

            <OrderedList pl={[4, 4, 6]} spacing={4}>
              <ListItem>
                a) confirmação da existência de tratamento de dados;
              </ListItem>
              <ListItem>b) acesso aos dados;</ListItem>
              <ListItem>
                c) correção de dados incompletos, inexatos ou desatualizados;
              </ListItem>
              <ListItem>
                d) anonimização, bloqueio ou eliminação de dados desnecessários,
                excessivos ou tratados em desconformidade com o disposto na
                referida Lei;
              </ListItem>
              <ListItem>
                e) portabilidade dos dados a outro fornecedor de serviço ou
                produto, mediante requisição expressa, de acordo com a
                regulamentação da autoridade competente, observados os segredos
                comercial e industrial;
              </ListItem>
              <ListItem>
                f) eliminação dos dados pessoais tratados com o consentimento do
                titular, exceto nas hipóteses previstas na referida Lei;
              </ListItem>
              <ListItem>
                g) informação das entidades públicas e privadas com as quais o
                controlador realizou uso compartilhado de dados;
              </ListItem>
              <ListItem>
                h) informação sobre a possibilidade de não fornecer
                consentimento e sobre as consequências da negativa;
              </ListItem>
              <ListItem>
                i) revogação do consentimento, nos termos da referida Lei.
              </ListItem>
            </OrderedList>

            <Text my={4}>
              As manifestações para exercício desses direitos deverão ser
              remetidas ao endereço eletrônico {'<dpo@bempromotora.com.br>'}.
            </Text>

            <UnorderedList pl={[4, 4, 6]} spacing={4}>
              <ListItem>
                O USUÁRIO declara-se ciente que ao retirar seu consentimento
                para finalidades fundamentais para o funcionamento da BEMSIGN e
                serviços, tais ambientes e serviços poderão ficar indisponíveis.
              </ListItem>
              <ListItem>
                Caso o USUÁRIO solicite a exclusão de seus Dados Pessoais, pode
                ocorrer que os Dados precisem ser mantidos por período superior
                ao pedido de exclusão, nos termos do artigo 16 da LGPD, para (i)
                cumprimento de obrigação legal ou regulatória, (ii) estudo por
                órgão de pesquisa, e (iii) transferência a terceiro (respeitados
                os requisitos de tratamento de dados dispostos na mesma Lei). Em
                todos os casos mediante a anonimização dos Dados Pessoais, desde
                que possível.
              </ListItem>
              <ListItem>
                {' '}
                Findos o prazo de manutenção e a necessidade legal, os Dados
                Pessoais serão excluídos com uso de métodos de descarte seguro,
                ou utilizados de forma anonimizada para fins estatísticos.
              </ListItem>
            </UnorderedList>
          </Box>
        ),
      },
      {
        id: 5,
        text: 'DEMAIS CONSIDERAÇÕES SOBRE O TRATAMENTO DE DADOS',
        items: [
          {
            id: 1,
            text: 'Todos os dados coletados são tratados como confidenciais. Portanto, a BEMSIGN somente os utilizará da forma expressamente descrita no presente Termo de Uso e Privacidade, sendo vedada a utilização dos dados para qualquer outro fim.',
          },
          {
            id: 2,
            text: 'Muitos dos serviços prestados dependem diretamente de alguns Dados informados no item 4.1.1., principalmente Dados cadastrais. Caso o USUÁRIO opte por não fornecer alguns desses Dados, a BEM PROMOTORA pode ficar impossibilitada de prestar total ou parcialmente os serviços ao USUÁRIO.',
          },
          {
            id: 3,
            text: 'O USUÁRIO é o único responsável pela precisão, veracidade ou falta dela em relação aos Dados fornecidos ou pela sua desatualização. Sendo de sua responsabilidade garantir a exatidão ou mantê-los atualizados.',
          },
          {
            id: 4,
            text: 'Na hipótese de o USUÁRIO realizar upload de algum documento que contenha vírus, erros, cavalos de troia ou quaisquer problemas similares, o documento poderá ser excluído sem qualquer aviso prévio. Caso o documento infectado seja enviado ao USUÁRIO, antes da identificação do problema, a BEMSIGN não poderá ser responsabilizada.',
          },
        ],
      },
    ],
  },
  {
    id: 5,
    text: 'DO ACORDO DE NÍVEL DE SERVIÇO OU SLA (SERVICE LEVEL AGREEMENT)',
    items: [
      {
        id: 1,
        text: 'Denomina-se acordo de nível de serviço ou SLA (Service Level Agreement), para efeito do presente Termo de Uso, o nível de desempenho técnico do serviço prestado proposto pela BEMSIGN, sendo certo que tal acordo não representa diminuição de responsabilidade da BEMSIGN, mas sim indicador de excelência técnica, uma vez que em informática não existe garantia integral (100%) de nível de serviço.',
      },
      {
        id: 2,
        text: 'A BEMSIGN, desde que observadas as obrigações previstas no presente Termo de Uso, tem condição técnica de oferecer e se propõe a manter, em cada mês civil, um SLA (Service Level Agreement) – acordo de nível de serviço ou garantia de desempenho) de 98,00%.',
      },
    ],
  },
  {
    id: 6,
    text: 'OBRIGAÇÕES E RESPONSABILIDADES DO USUÁRIO',
    content: (
      <Box>
        <UnorderedList>
          <ListItem my={4}>
            Constituem obrigações e responsabilidades do USUÁRIO:
          </ListItem>

          <OrderedList pl={[4, 4, 6]} spacing={4}>
            <ListItem>
              a) Sempre que solicitado, fornecer informações verdadeiras,
              precisas, atualizadas e completas, principalmente no ato do seu
              cadastramento.
            </ListItem>
            <ListItem>
              b) Utilizar a BEMSIGN única e exclusivamente com intuito e fim
              lícitos, sendo vedada qualquer outra utilização, principalmente as
              estranhas à sua finalidade original.
            </ListItem>
            <ListItem>
              c) Ler atentamente o documento a ser assinado eletronicamente e as
              condições para assinatura, uma vez que a BEMSIGN não poderá, sob
              qualquer hipótese, alterar o conteúdo do documento que será
              assinado.
            </ListItem>
            <ListItem>
              d) Não violar quaisquer direitos de propriedade industrial e
              intelectual da BEMSIGN e de terceiros na utilização da ferramenta,
              incluindo a aplicação de engenharia reversa, desmontagem,
              descompilação ou qualquer outra tentativa para descobrir os
              respectivos códigos-fonte, no todo ou em parte.
            </ListItem>
            <ListItem>
              e) Não utilizar a BEMSIGN para cometer e/ou tentar cometer atos
              que tenham como objetivo: alterar o conteúdo da BEMSIGN; obter
              acesso não autorizado a outro computador, servidor ou rede;
              interromper serviço, servidores, ou rede de computadores através
              de qualquer método ilícito.
            </ListItem>
            <ListItem>
              f) Não burlar qualquer sistema de autenticação ou de segurança;
              vigiar terceiros; acessar informações confidenciais de terceiros,
              de qualquer natureza.
            </ListItem>
            <ListItem>
              g) Respeitar toda e qualquer legislação brasileira vigente e
              aplicável à utilização da BEMSIGN, bem como qualquer norma ou lei
              aplicável no país de onde se origina o acesso do usuário.
            </ListItem>
            <ListItem>
              h) Respeitar todas as condições estabelecidas no presente
              instrumento e políticas de privacidade e segurança.
            </ListItem>
          </OrderedList>
        </UnorderedList>
      </Box>
    ),
  },
  {
    id: 7,
    text: 'INTEGRIDADE E AUTENTICIDADE',
    items: [
      {
        id: 1,
        text: 'A integridade do documento é realizada através:',
      },
      {
        id: 2,
        text: 'Uso do Carimbo do Tempo para garantir o momento da assinatura do USUÁRIO Uso de NTP (Network Time Protocol). Criptografia dos documentos para garantir que não haja alteração nos dados após a assinatura eletrônica Hash.',
      },
      {
        id: 3,
        text: 'A autenticidade do documento é realizada através:',
      },
      {
        id: 4,
        text: 'Geolocalização: verificação das coordenadas no momento da assinatura com o endereço do USUÁRIO.',
      },
      {
        id: 5,
        text: 'IP: Endereço de internet atribuído pela operadora para o dispositivo do usuário;',
      },
      {
        id: 6,
        text: 'Foto:',
        items: [
          {
            id: 1,
            text: 'Liveness Detection para garantir que é uma pessoa e não foto de foto;',
          },
          {
            id: 2,
            text: 'Facematch para garantir que a pessoa que está assinando é a mesma do documento de identidade. Aplicação de IA para verificação de semelhança entre faces;',
          },
          {
            id: 3,
            text: 'Biometria facial e CPF são confrontados para fins de mitigar possíveis fraudes.',
          },
        ],
      },
    ],
  },
  {
    id: 8,
    text: 'NÃO REPÚDIO',
    items: [
      {
        id: 1,
        text: 'O USUÁRIO reconhece, aprova, e declara confiar no método utilizado pela BEMSIGN para inserção da assinatura eletrônica, e estar seguro do grau de confidencialidade, integridade, e autenticidade dos serviços. Dessa forma, declara desde já, estar de acordo, nos termos do artigo 10, §2º da MP 2.200-2/01, com método adotado para assinar eletronicamente qualquer documento dentro da solução BEMSIGN.',
      },
      {
        id: 2,
        text: 'As partes declaram ainda que o contrato eletrônico é um negócio jurídico firmado em meio eletrônico, amplo e válido, tanto na legislação geral quanto em âmbito específico no Banco Central do Brasil.',
      },
    ],
  },
  {
    id: 9,
    text: 'DISPOSIÇÕES GERAIS',
    items: [
      {
        id: 1,
        text: 'A BEMSIGN emprega todos os esforços razoáveis de acordo com o atual estado da técnica para garantir a segurança de seus sistemas na guarda de referidos dados, para tanto adota as seguintes precauções: software de proteção contra acesso não autorizado aos sistemas; o acesso de pessoas aos locais onde as informações são armazenadas depende de autorização prévia; aqueles que entram em contato com as informações deverão se comprometer a manter sigilo absoluto.',
      },
      {
        id: 2,
        text: 'A BEMSIGN reserva a si o direito de modificar a ferramenta e os serviços a qualquer momento, sem aviso prévio, inclusive realizar alterações periódicas relativas a manutenção ou melhoria da solução.',
      },
      {
        id: 3,
        text: 'Este Termo de Responsabilidade de Uso representa o total entendimento entre as partes no que diz respeito ao uso da presente solução. A BEMSIGN poderá revisá-lo a qualquer momento, sem aviso prévio, mantendo-se o controle de alterações à disposição do usuário.',
      },
      {
        id: 4,
        text: 'A BEM PROMOTORA é responsável nos limites deste Termo de uso e privacidade, somente pelo processo de assinatura eletrônica ocorrido dentro da BEMSIGN, sendo de responsabilidade do banco parceiro os demais procedimentos prévios e posteriores relacionados à contratação dos serviços e produtos e concessão de crédito, bem como pela disponibilização destes após a sua devida contratação.',
      },
      {
        id: 5,
        text: 'Os servidores da BEMSIGN estão hospedados no datacenter da Microsoft Azure, que não nos garante excelência técnica integral (100%), motivo pelo qual a BEMSIGN não poderá ser responsabilizada por eventuais problemas gerados por falha na prestação dos serviços prestados pela Microsoft.',
      },
    ],
  },
  {
    id: 10,
    text: 'LEI APLICÁVEL E FORO',
    items: [
      {
        id: 1,
        text: 'Este Termo de Uso será regido, interpretado e executado de acordo com as leis da República Federativa do Brasil, independentemente dos conflitos dessas leis com lei de outros estados ou países, sendo competente o Foro da Comarca de PORTO ALEGRE/RS, Capital, para dirimir qualquer dúvida decorrente deste instrumento, salvo ressalva específica de competência pessoal, territorial ou funcional pela legislação aplicável.',
      },
      {
        id: 2,
        text: 'O USUÁRIO consente, expressamente, com a competência desse juízo, e renúncia, neste ato, à competência de qualquer outro foro, por mais privilegiado que seja ou venha a ser.',
      },
    ],
  },
];

export const recursiveUnorderedListRender = (
  items: any,
): null | React.ReactNode => {
  if (items?.length) {
    return (
      <UnorderedList spacing={4}>
        {items.map(
          ({ id: idSubItem, text: textSubItem, items: subItems, content }) => {
            return (
              <ListItem key={idSubItem}>
                <Text mb={subItems?.length ? 4 : 0}>{textSubItem}</Text>
                {subItems?.length && recursiveUnorderedListRender(subItems)}
                {content}
              </ListItem>
            );
          },
        )}
      </UnorderedList>
    );
  }

  if (items?.content) {
    return items.content;
  }

  return null;
};

export const Terms: FC = () => {
  return (
    <OrderedList spacing={6}>
      {TERMS.map(({ id, text, items, content }) => (
        <ListItem
          key={id}
          sx={{
            '::marker': {
              color: 'primary.regular',
              textStyle: 'regular16',
            },
          }}
        >
          <>
            <Text textStyle="regular16" color="primary.regular" my={4}>
              {text}
            </Text>
            {recursiveUnorderedListRender(items)}
            {content}
          </>
        </ListItem>
      ))}
    </OrderedList>
  );
};
