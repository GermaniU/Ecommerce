using System.Collections;
using Ecommerce.Application.Persistence;
using Ecommerce.Persistence;
using Ecommerce.Persistence.Repositories;

namespace Ecommerce.Application.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EcommerceDbContext _context;
        private Hashtable? _Repositories;

        public UnitOfWork(EcommerceDbContext context)
        {
            _context = context;
        }

        public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_Repositories is null)
            {
                _Repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if (!_Repositories.ContainsKey(type))
            {
                var repositoryType = typeof(RepositoryBase<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                _Repositories.Add(type, repositoryInstance);
            }

            return (IAsyncRepository<TEntity>)_Repositories[type]!;
        }

        public async Task<int> Complete()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al realizar la transacción", ex);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}