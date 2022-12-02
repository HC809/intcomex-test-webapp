using intcomex_test_webapp.DL.Entities;
using intcomex_test_webapp.DL.IntcomexDBContext;
using Microsoft.EntityFrameworkCore.Storage;

namespace intcomex_test_webapp.DAL
{
    public class UnitOfWork : IAsyncDisposable
    {
        private IntcomexDbContext context = new IntcomexDbContext();
        private IDbContextTransaction objTran;

        private GenericRepository<User> userRepository;
        private GenericRepository<ContactType> contactTypeRepository;

        public GenericRepository<User> UserRepository
        {
            get
            {
                if (userRepository == null) userRepository = new GenericRepository<User>(context);

                return userRepository;
            }
        }

        public GenericRepository<ContactType> ContactTypeRepository
        {
            get
            {
                if (contactTypeRepository == null) contactTypeRepository = new GenericRepository<ContactType>(context);

                return contactTypeRepository;
            }
        }

        public async Task<int> SaveAsync() => await context.SaveChangesAsync();
        public async Task<IDbContextTransaction> CreateTransactionAsync() => objTran = await context.Database.BeginTransactionAsync();
        public async Task CommitAsync() => await objTran.CommitAsync();
        public async Task RollbackAsync()
        {
            await objTran.RollbackAsync();
            await objTran.DisposeAsync();
        }

        private bool disposed = false;

        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

        protected virtual async ValueTask DisposeAsync(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing) await context.DisposeAsync();
                this.disposed = true;
            }
        }
    }
}
