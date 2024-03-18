using Microsoft.EntityFrameworkCore;
using ExternalEntities.Domain.AggregatesModel.UserAggregate;
using System.Threading.Tasks;
using ExternalEntities.Infraestructure.Persistence;
using ExternalEntities.Domain.Abstractions;
using MySqlConnector;

namespace ExternalEntities.Infraestructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ExternalEntitiesContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        public UserRepository(ExternalEntitiesContext context) => (_context) = (context);

        public async Task<int> AddIgnore(ApplicationUser user)
        {
            string query = @"INSERT INTO ApplicationUser (Id, Cpf, CreatedDate, UpdateDate, UserUpdate)
                     VALUES (@id, @cpf, @createdDate, @updateDate, @userUpdate)
                     ON DUPLICATE KEY UPDATE Cpf = @cpf";

            var result = await _context.Database.ExecuteSqlRawAsync(query,
                new MySqlParameter("@id", user.Id),
                new MySqlParameter("@cpf", user.Cpf),
                new MySqlParameter("@createdDate", user.CreatedDate),
                new MySqlParameter("@updateDate", user.UpdateDate),
                new MySqlParameter("@userUpdate", user.UserUpdate)
            );

            return result;
        } 
        public async Task<ApplicationUser> Get(int userId) =>  await _context.Users.Include(u => u.Scores).ThenInclude(e => e.History).FirstOrDefaultAsync(u => u.Id == userId);
        public async Task<ApplicationUser> GetByCpf(string cpf) =>  await _context.Users.Include(u => u.Scores).ThenInclude(e => e.History).FirstOrDefaultAsync(u => u.Cpf == cpf);

        public Task<ApplicationUser> Delete(int businessId)
        {
            throw new System.NotImplementedException();
        }
    }
}
