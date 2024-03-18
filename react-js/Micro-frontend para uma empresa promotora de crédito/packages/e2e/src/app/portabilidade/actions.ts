import { Page } from '@playwright/test';

export const fillPortForm = async (
  page: Page,
  refin = false,
): Promise<void> => {
  if (refin) {
    await page.click('text=Simular Portabilidade e Refinanciamento');
  } else {
    await page.click('text=Portar meu documento');
  }

  // Click label[role="checkbox"]:has-text("Matrícula 1758329596 | INSSBanco:0746 - BANCO MODAL SATipo de Conta:NormalAgênci")
  await page.click(
    'label[role="checkbox"]:has-text("Matrícula 1758329596 | INSSBanco:0746 - BANCO MODAL SATipo de Conta:NormalAgênci")',
  );
  // Click text=Continuar
  await page.click('text=Continuar');
  // Click text=OrigemNão sabe como proceder? Clique aqui!ContratoSaldo DevedorQuantidade Parcel >> input
  await page.click(
    'text=OrigemNão sabe como proceder? Clique aqui!ContratoSaldo DevedorQuantidade Parcel >> input',
  );
  // Click text=0900 - ABBC
  await page.click('text=0900 - ABBC');
  // Click input[name="contrato"]
  await page.click('input[name="contrato"]');
  // Fill input[name="contrato"]
  await page.fill('input[name="contrato"]', '2');
  // Click input[name="saldoDevedor"]
  await page.click('input[name="saldoDevedor"]');
  // Fill input[name="saldoDevedor"]
  await page.fill('input[name="saldoDevedor"]', 'R$ 500,000');
  // Press Tab
  await page.press('input[name="saldoDevedor"]', 'Tab');
  // Fill input[name="parcelas"]
  await page.fill('input[name="parcelas"]', '84');
  // Press Tab
  await page.press('input[name="parcelas"]', 'Tab');
  // Fill input[name="parcelasPagas"]
  await page.fill('input[name="parcelasPagas"]', '24');
  // Press Tab
  await page.press('input[name="parcelasPagas"]', 'Tab');
  // Fill input[name="prestacao"]
  await page.fill('input[name="prestacao"]', 'R$ 13,000');
  // Click text=Simular Portabilidade
  await page.click('text=Simular Portabilidade');

  if (refin) {
    await page.click('label[role="checkbox"]');
  }

  // Click text=Finalizar
  await page.click('text=Finalizar');
};

export const data = {
  idUsuario: 16,
  idTipoOperacao: 3,
  idProduto: 1,
  prestacao: 130,
  valorAuxilioFinanceiro: 0,
  impostoOperacaoFinanceira: 0,
  primeiroVencimento: '2021-10-08T00:00:00',
  taxaAno: 19.5552850198,
  taxaMes: 1.4995378768,
  prazo: 60,
  valorFinanciado: 5000,
  idRendimentoCliente: 7,
  custoEfetivoTotalAno: 19.852231178,
  custoEfetivoTotalMes: 1.5205223397,
  contratos: [{ contrato: '0000000002' }],
  portabilidade: {
    idBancoOriginador: 37,
    prazoRestante: 60,
    prazoTotal: 84,
    saldoDevedor: 5000,
    prazoRefinanciamento: null,
  },
};

export const dataRefin = {
  idUsuario: 16,
  idTipoOperacao: 3,
  idProduto: 1,
  prestacao: 130,
  valorAuxilioFinanceiro: 0,
  custoEfetivoTotalAno: 19.852231178,
  custoEfetivoTotalMes: 1.5205223397,
  impostoOperacaoFinanceira: 0,
  primeiroVencimento: '2021-10-08T00:00:00',
  taxaAno: 19.5552850198,
  taxaMes: 1.4995378768,
  prazo: 60,
  valorFinanciado: 5000,
  idRendimentoCliente: 7,
  contratos: [{ contrato: '0000000002' }],
  portabilidade: {
    idBancoOriginador: 37,
    prazoRestante: 60,
    prazoTotal: 84,
    saldoDevedor: 5000,
    planoRefinanciamento: 'KCBN',
    prazoRefinanciamento: 72,
    valorPrestacaoRefinanciamento: 130,
  },
};
