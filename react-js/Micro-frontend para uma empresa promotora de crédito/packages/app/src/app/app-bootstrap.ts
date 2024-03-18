import { api } from '@pcf/core';
import printBuildInfo from 'print-build-info';

printBuildInfo();

if (
  // build
  process.env.NODE_ENV === 'production' &&
  // servidor de produção
  process.env.REACT_APP_IS_PRODUCTION !== 'true'
) {
  /* eslint-disable */
  console.info('Carregando informações da API ...');
  console.info(process.env.REACT_APP_PLATAFORMA_CLIENTE_API);

  api
    .get('/about')
    .then((response) => {
      console.info(response.data.retorno);
    })
    .catch((error) => {
      console.error(error);
    });
  /* eslint-enable */
}
