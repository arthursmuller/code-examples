import { Response, Server } from 'miragejs'; //eslint-disable-line

export function createFakeServer(): Server {
  const server = new Server({
    routes() {
      this.get('/produtos', () => {
        return new Response(400, {}, { errors: ['some error'] });
      });
      this.get('/intencoes-operacao', () => {
        return new Response(
          200,
          {},
          {
            retorno: [],
            alertas: [],
            erros: [],
          },
        );
      });
      this.get('/feature-flags', () => {
        return new Response(
          200,
          {},
          {
            retorno: [
              { habilitado: true, chave: 'TEST' },
              { habilitado: true, chave: 'NEXT_PUBLIC_FEATURE_ACESSAR_CONTA' },
              { habilitado: true, chave: 'ACESSAR_CONTA' },
              { habilitado: true, chave: 'SIMULAR_NOVO' },
              { habilitado: true, chave: 'WHATSAPP' },
              { habilitado: true, chave: 'CRIAR_CONTA' },
              { habilitado: true, chave: 'LOGIN_SOCIAL' },
              { habilitado: true, chave: 'PORTABILIDADE' },
              { habilitado: true, chave: 'CARTAO' },
              { habilitado: true, chave: 'REFIN' },
              { habilitado: true, chave: 'INCLUSAO_PROPOSTA_NOVA' },
              {
                habilitado: true,
                chave: 'TELEFONE_CRIACAO_VALIDACAO_TELEFONEMA',
              },
              { habilitado: true, chave: 'TELEFONE_CRIACAO_VALIDACAO_SMS' },
              {
                habilitado: false,
                chave: 'TELEFONE_CRIACAO_VALIDACAO_WHATSAPP',
              },
              {
                habilitado: false,
                chave: 'TELEFONE_CRIACAO_VALIDACAO',
              },
            ],
            alertas: [],
            erros: [],
          },
        );
      });
      // this.post('/usuarios/recuperacao-senha', () => {
      //   return new Response(500, {}, { errors: ['some error'] });
      // });
      // this.post('/leads', () => {
      //   return new Response(500, {}, { errors: ['some error'] });
      // });
      // this.get('/usuarios/recuperacao-senha/token', () => {
      //   return new Response(
      //     200,
      //     {},
      //     {
      //       retorno: true,
      //       alertas: [],
      //       erros: [],
      //     },
      //   );
      // });
      // this.get('clientes/autenticado/telefones', () => {
      //   // return new Response(400, {}, { errors: ['some error'] });
      //   return new Response(
      //     200,
      //     {},
      //     {
      //       retorno: [
      //         {
      //           id: 83,
      //           idCliente: 2,
      //           ddd: '51',
      //           fone: '996124054',
      //           principal: true,
      //           tipoTelefone: 2,
      //         },
      //         {
      //           id: null,
      //           idCliente: 0,
      //           ddd: '',
      //           fone: '',
      //           principal: false,
      //           tipoTelefone: 0,
      //         },
      //       ],
      //       alertas: [],
      //       erros: [],
      //     },
      //   );
      // });
      // this.post('/usuarios/recuperacao-senha/token', () => {
      //   return new Response(
      //     200,
      //     {},
      //     {
      //       retorno: {
      //         nome: 'Fagner',
      //         email: 'teste@gmail.com',
      //         token:
      //           'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c',
      //       },
      //       alertas: [],
      //       erros: [],
      //     },
      //   );
      // });

      // this.post('/cliente/autenticado/beneficios-inss/autorizacoes', () => {
      //   return new Response(
      //     200,
      //     {},
      //     {
      //       retorno: { idConsultaBeneficio: 5 },
      //       alertas: [],
      //       erros: [],
      //     },
      //   );
      // });

      // this.post('login-social/2', () => {
      //   return new Response(
      //     200,
      //     {},
      //     {
      //       retorno: {
      //         nome: 'fagner',
      //         email: 'fagnerschwalm@gmail.com',
      //         token:
      //           'eyJhbGciOiJSUzI1NiIsImtpZCI6ImI2ZjhkNTVkYTUzNGVhOTFjYjJjYjAwZTFhZjRlOGUwY2RlY2E5M2QiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJhY2NvdW50cy5nb29nbGUuY29tIiwiYXpwIjoiMTA0NDA1NjM0MzI4Ni1sY3AwZXBiOGkwMjVvcm45OWFzZTk3NWtndGJ1ZWtlZC5hcHBzLmdvb2dsZXVzZXJjb250ZW50LmNvbSIsImF1ZCI6IjEwNDQwNTYzNDMyODYtbGNwMGVwYjhpMDI1b3JuOTlhc2U5NzVrZ3RidWVrZWQuYXBwcy5nb29nbGV1c2VyY29udGVudC5jb20iLCJzdWIiOiIxMTgwODA3NDI5NTE1OTM4MDQ5OTgiLCJlbWFpbCI6ImZhZ25lcnNjaHdhbG1AZ21haWwuY29tIiwiZW1haWxfdmVyaWZpZWQiOnRydWUsImF0X2hhc2giOiJsZDhiSUIxbFN0dDJHSWZKMTZHaTZnIiwibmFtZSI6IkZhZ25lciBTY2h3YWxtIiwicGljdHVyZSI6Imh0dHBzOi8vbGgzLmdvb2dsZXVzZXJjb250ZW50LmNvbS9hLS9BT2gxNEdqVTA1TTFtcmFlb1MxVDNVR1FJb2lSUEI1cEFyb2U5bU5hWDNFMlh3PXM5Ni1jIiwiZ2l2ZW5fbmFtZSI6IkZhZ25lciIsImZhbWlseV9uYW1lIjoiU2Nod2FsbSIsImxvY2FsZSI6InB0LUJSIiwiaWF0IjoxNjI1NTgzNDI5LCJleHAiOjE2MjU1ODcwMjksImp0aSI6Ijg2NDFlOTFjMzc1NmMwMDE1ZTQ1MmNhNTE2NzE3MjE0MGVkYTI0MmIifQ.N8Hu-usDphb4Ds0OGa0bhiZejKudTwYWN03TB4oug4eNoGRCKtrrXHf5RIRD8FHd3j_YcijGuS9gn57khyTdqxzysJsfJD2m_5Dah_pSMN9_gWYx3_SypL_qKqhUyNGu6Y1nlOacMj6pIPkZ2EMhUp6Q0m7YRRcucasbvr9uWK5KCK3Tozkup2Y0WFLa30XhAVfPojUn-WcDYJvlPkC0-hfqrUrb5CJldrUWne0-oKmjL-RszESlH9n5mrhq9LgRIqMAj_dW11VpsbWYt7v5Zf8CP6rGTo4bL8Oq2EbgcZjaD5qNgW1dgZRc8Db3R7HcfVLNZM7M82wByHKQnHTIOg',
      //         usuarioCadastrado: false,
      //       },
      //       alertas: [],
      //       erros: [],
      //     },
      //   );
      // });

      this.passthrough('*');
    },
    urlPrefix: process.env.REACT_APP_PLATAFORMA_CLIENTE_API,
    environment: process.env.NODE_ENV,
    trackRequests: true,
  });

  return server;
}
