import { FaqQuestion } from '@pcf/design-system';

interface FaqQuestionWithId extends FaqQuestion {
  id: number;
}

export const faqQuestions: FaqQuestionWithId[] = [
  {
    id: 0,
    question: 'O Que é a Bem Promotora?',
    answer: `
      A Bem promotora é uma instituição financeira, que possui uma gama de produtos voltados para Aposentados e Pensionistas do INSS e servidores públicos, sendo os produtos:

      • Crédito Consignado;
      • Cartão de Crédito Consignado; e
      • Titulo de Capitalização.
    `,
  },
  {
    id: 1,
    question: 'O Que é Crédito Consignado?',
    answer: `
      O crédito consignado é um empréstimo para pessoa física com desconto em folha de pagamento. Com uma das menores taxas do mercado, é ideal para quitar dívidas com juros mais altos ou realizar tudo aquilo que você sempre quis.
    `,
    // Se você é beneficiário do INSS ou funcionário público e deseja saber mais sobre o empréstimo consignado, ou fazer uma simulação, clique aqui.
  },
  {
    id: 2,
    question:
      'Qual a taxa de Juros para o Crédito Consignado da Bem Promotora?',
    answer: `
      O Crédito Consignado da Bem, conta com taxas de no máximo 2,05% ao mês. Contudo, a definição da taxa para cada cliente depende de uma análise de crédito feita pela instituição, que variam de acordo com o valor do seu empréstimo e do prazo que você escolher para o pagamento.
    `,
  },
  // {
  //   question: 'Como faço para solicitar o Crédito Consignado da Bem Promotora?',
  //   answer: `
  //     Para contratar um de nossos produtos, basta clicar aqui e seguir o nosso tutorial.
  //   `,
  // },
  {
    id: 3,
    question: 'Qual valor máximo de Crédito Consignado posso solicitar?',
    answer: `
        Você pode solicitar um valor que corresponda a até 30% do seu salário líquido. Esse valor é chamado de margem consignável.

        Este cálculo é feito de acordo com a margem consignável informada pelo site do órgão ao qual pertence o servidor.

        Caso o interessado seja aposentado ou pensionista do INSS, consultamos a margem de acordo com o valor de seu benefício.
    `,
  },
  {
    id: 4,
    question:
      'Como é feita a análise do meu crédito para liberação ou não do valor solicitado?',
    answer: `
      A Análise é feita através de uma mesa de crédito especializada, que realiza a consulta da margem consignável, que para beneficiários do INSS, é realizada através do CPF e funcionários públicos, feito por meio do número da sua matrícula e CPF.
    `,
    // Se você é beneficiário do INSS ou funcionário público e deseja saber mais sobre o empréstimo consignado, ou fazer uma simulação, clique aqui.
  },
  {
    id: 5,
    question: 'Posso obter Crédito Consignado caso tenha restrição em meu CPF?',
    answer: `
      Sim, você pode obter o Crédito Consignado mediante análise de crédito e do seu órgão pagador, mesmo estando negativado. Isso porque o desconto é realizado diretamente em sua folha de pagamento.
    `,
  },
  {
    id: 6,
    question:
      'Quanto tempo leva para a aprovação da minha solicitação de Crédito Consignado?',
    answer: `
    A aprovação da solicitação de crédito consignado depende do órgão do governo responsável pela liberação de sua folha de pagamento, e pode levar de 1 a 15 dias.
    `,
  },
  {
    id: 7,
    question: 'Como vou receber o empréstimo do Crédito Consignado.',
    answer: `
    O valor do empréstimo é depositado na conta corrente do titular. Dependendo da sua instituição ou órgão responsável pela liberação da sua folha de pagamento, pode ser que a disponibilização do crédito deva ser feita diretamente na conta corrente cadastrada no contracheque, ou seja, na mesma conta onde você já recebe o seu salário.
    `,
  },
  {
    id: 8,
    question: 'Posso refinanciar o meu empréstimo consignado?',
    answer: `
      Sim, você pode refinanciar o seu contrato de empréstimo consignado, quando tiver objetivo de reduzir/aumentar o valor das parcelas/prazos, ou liberação de um novo valor.
      Para refinanciar o seu Empréstimo Consignado é necessário que o seu convênio esteja ativo bem como o contrato.
    `,
  },
  {
    id: 9,
    question: 'Posso pagar minhas parcelas antes da data de vencimento?',
    answer: `
      Sim, você pode antecipar o pagamento de quantas parcelas desejar ou até mesmo quitar o valor total do seu crédito consignado.

      Basta entrar em contato com a gente e solicitar a antecipação: 3003 0511 (capitais e regiões metropolitanas) ou 0800 72 00 011 (demais localidades).
    `,
  },
  // {
  //   question: 'Como faço a assinatura eletrônica do meu contrato de Crédito Consignado?',
  //   answer: `
  //     XXXXXX.
  //   `,
  // },
  {
    id: 10,
    question:
      'Como faço portabilidade do meu Crédito Consignado para a Bem Promotora?',
    answer: `
      Para fazer a portabilidade do seu Crédito Consignado, você deve solicitar o valor total da dívida à instituição de origem do financiamento e informar esse valor à nova instituição, no caso a Bem, além do número do contrato e os demais dados solicitados;

      Se você quer fazer a portabilidade do seu contrato aberto em outra instituição, entre em contato conosco: 3003 0511 (capitais e regiões metropolitanas) ou 0800 72 00 011 (demais localidades).
    `,
  },
  {
    id: 11,
    question: 'O Que é Cartão de Crédito Consignado?',
    answer: `
      O Cartão de Crédito Consignado, trata-se de um cartão de crédito cujo valor mínimo da fatura é descontado diretamente do seu salário líquido ou benefício do INSS.
    `,
  },
  {
    id: 12,
    question: 'Como é calculado o meu limite do Cartão de Crédito Consignado?',
    answer: `
      O Limite do Cartão Consignado varia de acordo com o seu convênio (INSS ou SIAPE), sendo baseado no RMC (Reserva de Margem Consignável).
    `,
  },
  {
    id: 13,
    question: 'Como solicito aumento do limite do meu Cartão Consignado?',
    answer: `
      O Aumento do seu limite do Cartão Consignado está ligado diretamente à margem consignável disponível, sendo possível realizar caso houver um aumento no salário.

      Caso você obteve aumento no salário e deseja solicitar um aumento no limite do seu Cartão Consignado, entre em contato conosco: 3003 0511 (capitais e regiões metropolitanas) ou 0800 72 00 011 (demais localidades)
    `,
  },
  {
    id: 14,
    question:
      'Como posso obter informações sobre minha fatura do Cartão Consignado?',
    answer: `
      Para obter informações da sua fatura, entre em contato conosco: 3003 0511 (capitais e regiões metropolitanas) ou 0800 72 00 011 (demais localidades).
    `,
  },
  {
    id: 15,
    question:
      'O que devo fazer em caso de perda ou roubo do meu Cartão Consignado?',
    answer: `
      Em situações de perda/roubo do seu cartão ou se desconhecer lançamentos em sua fatura, entre em contato conosco imediatamente no telefone 3003 0511 (capitais e regiões metropolitanas) ou 0800 72 00 011 (demais localidades).
    `,
  },
];
