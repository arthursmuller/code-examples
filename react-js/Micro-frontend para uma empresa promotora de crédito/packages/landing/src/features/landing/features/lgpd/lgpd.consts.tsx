import { Box, ListItem, Text, UnorderedList } from '@chakra-ui/react';

import { FaqQuestion } from '@pcf/design-system';

export const lgpdQuestions: FaqQuestion[] = [
  {
    question: 'Mas o que são dados pessoais?',
    answer: (
      <Box>
        <Text>
          Dados Pessoais são quaisquer informações relacionadas a uma pessoa
          identificada ou identificável.
        </Text>

        <Text mt={6}>
          O seu nome, o CPF e o endereço são dados pessoais, mas vai muito além
          disso. São também exemplos de dados pessoais:
        </Text>

        <UnorderedList mt={6} ml={8}>
          <ListItem>seu endereço de e-mail;</ListItem>
          <ListItem>os sites que você acessou;</ListItem>
          <ListItem>
            quanto dinheiro você possui em conta corrente ou aplicado;
          </ListItem>
          <ListItem>
            se você já utilizou um determinado serviço ou comprou um produto
            específico;
          </ListItem>
          <ListItem>seus gostos e preferências em geral.</ListItem>
        </UnorderedList>
      </Box>
    ),
  },
  {
    question: 'E o que são dados pessoais sensíveis?',
    answer: (
      <Box>
        <Text>
          Alguns dados são protegidos de forma mais rigorosa pela LGPD por
          revelarem a intimidade das pessoas. São os chamados Dados Pessoais
          sensíveis, que incluem:
        </Text>

        <UnorderedList mt={6} ml={8}>
          <ListItem>origem racial ou étnica;</ListItem>
          <ListItem>
            convicção religiosa, opinião política, filiação a sindicato ou a
            organização de caráter religioso, filosófico ou político;
          </ListItem>
          <ListItem>dado referente à saúde ou à vida sexual; e</ListItem>
          <ListItem>dado genético ou biométrico.</ListItem>
        </UnorderedList>
      </Box>
    ),
  },
  {
    question: 'Quais os princípios de tratamentos de dados na LGPD?',
    answer: (
      <Box>
        <Text>
          Quem realiza Tratamento de Dados Pessoais precisa obedecer aos
          seguintes Princípios:
        </Text>

        <UnorderedList mt={6} ml={8}>
          <ListItem>
            <strong>Finalidade e Adequação</strong> – o tratamento de dados deve
            se dar para um propósito legítimo e específico, informado à pessoa;
            outros usos dos mesmos dados para outros propósitos não são
            permitidos;
          </ListItem>
          <ListItem>
            <strong>Necessidade</strong> – deve-se realizar o tratamento mínimo
            necessário para a realização da finalidade, sem dados excessivos;
          </ListItem>
          <ListItem>
            <strong>Qualidade, Livre Acesso e Transparência</strong> – as
            pessoas devem possuir informações claras, precisas e facilmente
            acessíveis sobre o tratamento dos seus dados, os quais devem estar
            corretos e atualizados;
          </ListItem>
          <ListItem>
            <strong>Segurança e Prevenção</strong> – devem ser adotadas medidas
            técnicas e administrativas para proteger os dados pessoais e
            prevenir a ocorrência de incidentes;
          </ListItem>
          <ListItem>
            <strong>Não-discriminação</strong> – o tratamento não pode ser
            realizado para fins discriminatórios, ilícitos ou abusivos;
          </ListItem>
          <ListItem>
            <strong>Responsabilização e Prestação de Contas</strong> – quem
            trata dados pessoais deve estar preparado para demonstrar a
            conformidade com a LGPD e a eficácia das medidas adotadas para a sua
            proteção.
          </ListItem>
        </UnorderedList>
      </Box>
    ),
  },
  {
    question: 'Quem são os titulares de dados pessoais?',
    answer: `
    São as pessoas a quem os dados se referem, as quais passam a ser consideradas os “donos dos dados”. Essa noção é talvez o eixo central da LGPD: de agora em diante, mesmo se capturados pelas empresas, por meio de  sua infraestrutura e tecnologia, tais dados – por regra – pertencem sempre às pessoas a eles relacionadas.
    `,
  },
  {
    question: 'Quem são os agentes de tratamento dos dados?',
    answer: (
      <Box>
        <Text>
          São as empresas (mais comum) ou pessoas que realizam o Tratamento de
          Dados Pessoais e que, na forma da LGPD, precisam estar preparados para
          prestar contas de que assim o fazem observando os seus requisitos.
        </Text>

        <Text mt={6}>
          Os Agentes de Tratamento possuem diversos Deveres, dentre os quais os
          de nomear um Encarregado do Tratamento de Dados Pessoais, documentar
          as Operações de Tratamento de Dados Pessoais, reportar incidentes,
          adotar medidas de segurança (técnicas e administrativas) e,
          principalmente, enquadrar os usos de Dados Pessoais nas hipóteses
          legais.
        </Text>

        <Text mt={6}>Os Agentes de Tratamento dividem-se em:</Text>

        <UnorderedList mt={6} ml={8}>
          <ListItem>
            <strong>Controladores</strong>: aqueles que tomam as decisões sobre
            como o Tratamento de Dados se dará, naturalmente assumindo maiores
            responsabilidades (como a empresa que define uma iniciativa de
            e-mail marketing para contatar clientes que não fazem compras a
            determinado período); e
          </ListItem>
          <ListItem>
            <strong>Operadores</strong>: aqueles que realizam Tratamentos de
            Dados em nome de um Controlador (por exemplo, uma empresa de
            informática ou o software contratado para realizar esse envio de
            e-mails, sem qualquer participação na escolha de quem receberá qual
            mensagem e qual conteúdo).
          </ListItem>
        </UnorderedList>
      </Box>
    ),
  },
  {
    question: 'Quais são os direitos dos titulares de dados pessoais?',
    answer: (
      <Box>
        <Text>
          A partir da vigência da LGPD, os Titulares de Dados passarão a ter –
          em especial – os seguintes direitos:
        </Text>

        <UnorderedList mt={6} ml={8}>
          <ListItem>
            <strong>Titularidade sobre os seus Dados Pessoais</strong>, podendo
            dispor livremente dos mesmos e controlar o seu uso, mediante
            consentimento livre, informado e inequívoco.
          </ListItem>
          <ListItem>
            <strong>
              Informação clara, adequada e ostensiva sobre o Tratamento dos seus
              Dados Pessoais,
            </strong>{' '}
            incluindo:
            <UnorderedList ml={8}>
              <ListItem>finalidade específica do Tratamento;</ListItem>
              <ListItem>forma e duração do Tratamento;</ListItem>
              <ListItem>
                identificação do Controlador e informações de contato;
              </ListItem>
              <ListItem>
                existência de compartilhamento com terceiros e qual a sua
                finalidade;
              </ListItem>
              <ListItem>
                responsabilidades dos Agentes que realizarão o Tratamento; e
              </ListItem>
              <ListItem>
                possibilidade de não fornecer consentimento e consequências
                dessa negativa.
              </ListItem>
            </UnorderedList>
          </ListItem>
          <ListItem>
            Possibilidade de obter{' '}
            <strong>
              cópia eletrônica e integral dos Dados Pessoais tratados
            </strong>
            , quando o Tratamento tiver origem no seu consentimento ou em
            contrato.
          </ListItem>
          <ListItem>
            <strong>Correção</strong> de Dados Pessoais incompletos, inexatos ou
            desatualizados.
          </ListItem>
          <ListItem>
            <strong>Anonimização, bloqueio ou eliminação</strong> de dados
            desnecessários, excessivos ou tratados em desconformidade com a
            LGPD.
          </ListItem>
          <ListItem>
            <strong>Exclusão</strong> de Dados Pessoais.
          </ListItem>
          <ListItem>
            <strong>Portabilidade</strong> de Dados Pessoais para a outra
            empresa.
          </ListItem>
          <ListItem>
            <strong>Revisão, por pessoa natural,</strong> de decisões tomadas
            unicamente com base em tratamento automatizado dos Dados Pessoais.
          </ListItem>
        </UnorderedList>
      </Box>
    ),
  },
];
