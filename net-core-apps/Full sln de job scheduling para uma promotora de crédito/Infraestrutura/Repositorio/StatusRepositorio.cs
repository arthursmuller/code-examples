using System;
using System.Threading.Tasks;
using B.Repositorio;
using Dapper;
using Dominio.Repositorio;

namespace Infraestrutura.Repositorio
{
    public class StatusRepositorio : IStatusRepositorio
    {
        private readonly IConexaoBanco _db;

        public StatusRepositorio(IConexaoBanco db)
        {
            _db = db;
        }

        public async Task<string> BuscarStatusBanco()
        {
            var (comando, conexao) = _db.ObterComandoSQL(this, "StatusBanco", "SCAFFOLD");

            return await conexao.QueryFirstOrDefaultAsync<string>(comando);
        }
    }
}