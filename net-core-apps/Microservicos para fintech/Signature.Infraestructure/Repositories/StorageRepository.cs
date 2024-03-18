using Signature.Domain.Abstractions;
using Signature.Domain.AggregatesModel.SignatureAggregate;
using Signature.Infraestructure.Persistence;

namespace Signature.Infraestructure.Repositories
{
    public class SignatureRepository : ISignatureRepository
    {
        private readonly SignatureContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        public SignatureRepository(SignatureContext context) => (_context) = (context);
    }
}
