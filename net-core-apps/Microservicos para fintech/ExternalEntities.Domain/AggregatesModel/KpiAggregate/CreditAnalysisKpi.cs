using RichEnumeration;
using System;
using System.Collections.Generic;
namespace ExternalEntities.Domain.AggregatesModel.KpiAggregate
{
    public class AnalysisDashboardField : Enumeration
    {
        public string Description { get; set; }
        public string Type { get; set; }
        public string MsgBom { get; set; }
        public string MsgMedio { get; set; }
        public string MsgRuim { get; set; }
        public string MsgGeral { get; set; }


        public static AnalysisDashboardField GeneralAnalysis = new AnalysisDashboardField(1, "Análise geral", "", AnalysisDashboardFieldType.MainIndicator.Name, "Baseado na inteligência artificial desse modelo de crédito e em todos os dados obtidos, esse CPF possúi um baixo risco de inadimplência.", "Baseado na inteligência artificial desse modelo de crédito e em todos os dados obtidos, esse CPF possúi um médio risco de inadimplência.", "Baseado na inteligência artificial desse modelo de crédito e em todos os dados obtidos, esse CPF possúi um alto risco de inadimplência.");
        public static AnalysisDashboardField PaymentCommitmentScore = new AnalysisDashboardField(2, "Pontualidade de pagamento dos últimos 12 meses", "", AnalysisDashboardFieldType.Indicator.Name, "A pontualidade de pagamento nos últimos 12 meses é considerado boa com base em todas as contas pagas.", "A pontualidade de pagamento nos últimos 12 meses é considerado relativamente média com base em todas as contas pagas.", "A pontualidade de pagamento nos últimos 12 meses é considerado ruím com base em todas as contas pagas.");
        public static AnalysisDashboardField AnalysisTrustAndConfiability = new AnalysisDashboardField(3, "Qualidade e confiança da análise de crédito", "", AnalysisDashboardFieldType.Indicator.Name, "O CPF possúi uma grande quantidade de dados para análise. Incidindo em maior confiança das informações apresentadas.", "O CPF possúi uma quantidade decente de dados para análise. Incidindo em uma confiança média sobre a análise de crédito.", "Não consideramos as informações dessa análise suficiente.");
        public static AnalysisDashboardField QualityOfFinancialOperations = new AnalysisDashboardField(4, "Qualidade das operações financeiras", "", AnalysisDashboardFieldType.Indicator.Name, "A quantidade de operações financeiras desse CPF incide em maior histórico dentro do sistema financeiro caso bem sucedidas.", "A quantidade de operações financeiras desse CPF é relativamente boa.", "A quantidade de operações financeiras desse CPF é relativamente baixa. Incidindo em menor histórico de crédito.");
        public static AnalysisDashboardField ProfileScore = new AnalysisDashboardField(5, "Relacionamento com o mercado", "", AnalysisDashboardFieldType.Indicator.Name, "O relacionamento de mercado baseado em operações financeiras passadas e ativas é considerado boa.", "O relacionamento de mercado baseado em operações financeiras passadas e ativas é considerado média.", "O relacionamento de mercado baseado em operações financeiras passadas e ativas é considerado ruim.");
        public static AnalysisDashboardField ActiveProtests = new AnalysisDashboardField(6, "Protestos ativos", "", AnalysisDashboardFieldType.Indicator.Name, "A não existência de protestos indica um bom relacionamento de consumo.", "Protestos podem ou não ser signficativo em uma análise, portanto o número de protestos pode não ser relevante.", "O número de protestos é relevante para uma análise");
        public static AnalysisDashboardField ActiveDebtsAmount = new AnalysisDashboardField(7, "Dívidas ativas", "", AnalysisDashboardFieldType.Indicator.Name, "As dívidas ativas não representam risco para dívidas futuras.", "Com base nas dívidas ativas, representados também por o uso atual de produtos financeiros, se entende que pode haver divergência no entendimento da representatividade da dívida.", "As dívidas ativas garantem uma representatividade na previsão de pagamento desse cpf.");
        public static AnalysisDashboardField LoanAmountSuggested = new AnalysisDashboardField(8, "Credito indicado", "", AnalysisDashboardFieldType.Insight.Name, "");
        public static AnalysisDashboardField ActiveLoansAmount = new AnalysisDashboardField(9, "Crédito em utilização", "", AnalysisDashboardFieldType.Insight.Name, "");
        public static AnalysisDashboardField CreditLimit = new AnalysisDashboardField(10, "Limited de crédito", "", AnalysisDashboardFieldType.Insight.Name, "");
        public static AnalysisDashboardField ChequeEspecial = new AnalysisDashboardField(11, "Cheque especial", "", AnalysisDashboardFieldType.Flag.Name, "Esse CPF utilizou Cheque Especial recentemente.");
        public static AnalysisDashboardField CurrentDefaultDebts = new AnalysisDashboardField(12, "Dívidas ativas", "", AnalysisDashboardFieldType.Flag.Name, "Esse CPF possúi dívidas ativas.");

        public AnalysisDashboardField(int id, string name, string description, string type, string msgBom, string msgMedio, string msgRuim) : base(id, name)
            => (Description, Type, MsgBom, MsgMedio, MsgRuim) = (description, type, msgBom, msgMedio, msgRuim);

        public AnalysisDashboardField(int id, string name, string description, string type,  string msgGeral) : base(id, name)
            => (Description, Type, MsgGeral) = (description, type, msgGeral);

        public static IEnumerable<AnalysisDashboardField> List() =>
               new[] { 
                    GeneralAnalysis,
                    PaymentCommitmentScore,
                    AnalysisTrustAndConfiability,
                    QualityOfFinancialOperations,
                    ProfileScore,
                    ActiveProtests,
                    ActiveDebtsAmount,
                    LoanAmountSuggested,
                    ActiveLoansAmount,
                    CreditLimit,
                    ChequeEspecial,
                    CurrentDefaultDebts, 
               };
    }
    public class AnalysisDashboardFieldType : Enumeration
    {
        public string Description { get; set; }

        public static AnalysisDashboardFieldType MainIndicator = new AnalysisDashboardFieldType(1, "MainIndicators", "As métricas são calculadas a partir de múltiplas base de dados. Utilizando de informações de pendências de relatórios de crédito e cadastro positivo, com mais controle e segurança nas métricas.");
        public static AnalysisDashboardFieldType Indicator = new AnalysisDashboardFieldType(2, "Indicators", "Indicadores utilizados na análise comumente utilizados em análises do mercado.");
        public static AnalysisDashboardFieldType Insight = new AnalysisDashboardFieldType(3, "Insights", "Usamos nossa inteligência para fazer sugestões com base em critérios.");
        public static AnalysisDashboardFieldType Flag = new AnalysisDashboardFieldType(4, "Flags", "Flags são pontos críticos negativos que consideramos em uma análise.");

        public AnalysisDashboardFieldType(int id, string name, string description) : base(id, name)
            => (Description) = (description);

        public static IEnumerable<AnalysisDashboardFieldType> List() =>
               new[] { MainIndicator, Indicator, Insight, Flag };
    }
}
