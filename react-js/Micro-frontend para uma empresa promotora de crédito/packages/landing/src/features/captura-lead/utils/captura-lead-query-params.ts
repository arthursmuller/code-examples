import { ParsedQs } from 'qs';

type translatedQueryParams = {
  origemRequisicaoPalavraChave?: string | null;
  origemRequisicaoMidia?: string | null;
  origemRequisicaoConteudo?: string | null;
  origemRequisicaoTermo?: string | null;
  origemRequisicaoCampanha?: string | null;
};

const getQueryParamValue = (value): string | null => {
  if (typeof value === 'string' && value) {
    return value;
  }
  return null;
};

export const translateQueryParams = (
  queryParams: ParsedQs,
): translatedQueryParams => {
  const {
    keyword,
    utm_medium, // eslint-disable-line
    utm_content, // eslint-disable-line
    utm_term, // eslint-disable-line
    utm_campaign, // eslint-disable-line
  } = queryParams;

  return {
    origemRequisicaoPalavraChave: getQueryParamValue(keyword),
    origemRequisicaoMidia: getQueryParamValue(utm_medium), // eslint-disable-line
    origemRequisicaoConteudo: getQueryParamValue(utm_content), // eslint-disable-line
    origemRequisicaoTermo: getQueryParamValue(utm_term),// eslint-disable-line
    origemRequisicaoCampanha: getQueryParamValue(utm_campaign), // eslint-disable-line
  };
};
