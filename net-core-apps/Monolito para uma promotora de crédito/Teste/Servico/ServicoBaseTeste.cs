using Aplicacao;
using B.Mensagens.Implementacoes;
using B.Mensagens.Interfaces;
using Dominio;
using Infraestrutura;
using Microsoft.EntityFrameworkCore;
using SharedKernel.ValueObjects.v2;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Teste.Servico
{
    public class ServicoTesteBase
    {
        protected const string CPF_USUARIO_TESTE = "19614414030";
        protected const string EMAIL_USUARIO_TESTE = "unithe@test.com";
        protected const string NOME_USUARIO_TESTE = "Uni, the Test";
        private const string AccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFydGh1ciIsIm5hbWVpZCI6IjEiLCJyb2xlIjoiIiwiVXN1YXJpb1RlbmFudCI6IiIsIm5iZiI6MTY0Njc3MTEwNSwiZXhwIjoxNjQ2Nzg1NTA1LCJpYXQiOjE2NDY3NzExMDV9.T0_GsWtCfnfYlmSWvl-O_TQ0dCan2xBlxaInRBt3tTY";
        
        protected readonly IBemMensagens _mensagens;
        protected readonly IUsuarioLogin _usuarioLogin;
        protected readonly PlataformaClienteContexto _contexto;

        public static bool MapperRegistred { get; set; }
        private static object _lockMapper = new object();

        public ServicoTesteBase()
        {
            var builder = new DbContextOptionsBuilder<PlataformaClienteContexto>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            _contexto = new PlataformaClienteContexto(builder.Options);
            _mensagens = new BemMensagens();
            _usuarioLogin = new UsuarioLoginDominio { IdUsuario = 1, AccessToken = AccessToken };
        }

        protected UsuarioDominio CriarUsuarioTeste()
        {
            var usuario = new UsuarioDominio(NOME_USUARIO_TESTE, EMAIL_USUARIO_TESTE, true, new CPF(CPF_USUARIO_TESTE), "1234ABcd", new ClienteDominio(NOME_USUARIO_TESTE));

            _contexto.Usuarios.Add(usuario);
            _contexto.SaveChanges();

            return usuario;
        }

        protected async Task<UsuarioDominio> CriarUsuarioTesteAsync()
        {
            var usuario = new UsuarioDominio(NOME_USUARIO_TESTE, EMAIL_USUARIO_TESTE, true, new CPF(CPF_USUARIO_TESTE), "1234ABcd", new ClienteDominio(NOME_USUARIO_TESTE));

            await _contexto.Usuarios.AddAsync(usuario);
            await _contexto.SaveChangesAsync();

            return usuario;
        }

        protected async Task<UsuarioDominio> CriarUsuarioTesteEmailNaoConfirmadoAsync()
        {
            var usuario = new UsuarioDominio(NOME_USUARIO_TESTE, "test@test.com" , false, new CPF("19614414031"), "1234ABcd", new ClienteDominio(NOME_USUARIO_TESTE));

            await _contexto.Usuarios.AddAsync(usuario);
            await _contexto.SaveChangesAsync();

            return usuario;
        }

        protected async Task<IEnumerable<T>> getEntidades<T>() where T : class
            => await _contexto.Set<T>().ToListAsync();

        protected void InstanciarAdapter()
        {
            lock (_lockMapper)
            {
                if (!MapperRegistred)
                {
                    AutoMapper.Mapper.Reset();
                    Adapter.Mapear();

                    MapperRegistred = true;
                }
            }
        }

        protected void SaveChanges() => _contexto.SaveChanges();
        protected async Task SaveChangesAsync() => await _contexto.SaveChangesAsync();
        protected async Task AddRangeAndSaveAsync<T>(T[] items) where T : class
        {
            await _contexto.AddRangeAsync(items);
            await SaveChangesAsync();
        }

        protected void AddRangeAndSave<T>(T[] items) where T : class
        {
             _contexto.AddRange(items);
            SaveChanges();
        }
    }
}
