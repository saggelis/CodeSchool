using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FlipWeen.Common.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Transactions;
using FlipWeen.Common;

namespace FlipWeen.Data
{
    /// <summary>
    /// A generic repository for working with data in the database
    /// </summary>
    /// <typeparam name="T">A POCO that represents an Entity Framework entity</typeparam>
    public class DataRepository : IDataRepository
    {
        /// <summary>
        /// The context object for the database
        /// </summary>
        private IDbContext _context;
    
        /// <summary>
        /// Initializes a new instance of the GenericRepository class
        /// </summary>
        /// <param name="context">The Entity Framework ObjectContext</param>
        public DataRepository(IDbContext context)
        {
            _context = context;
           
        }

        public IQueryable<TEntity> GetQuery<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return GetQuery<TEntity>().AsEnumerable();
        }

        public IEnumerable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return GetQuery<TEntity>().Where(predicate).AsEnumerable();
        }

        public TEntity Single<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return GetQuery<TEntity>().Single<TEntity>(predicate);
        }

        public TEntity First<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return GetQuery<TEntity>().Where(predicate).FirstOrDefault();
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Add(entity);
        }

        public virtual void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Set<TEntity>().Attach(entity);
            }
            _context.Set<TEntity>().Remove(entity);
        }

        public virtual void Delete<TEntity>(object id) where TEntity : class
        {
            TEntity entityToDelete = _context.Set<TEntity>().Find(id);
            Delete(entityToDelete);
        }
        

        void IDataRepository.SaveChanges()
        {
            using (var transaction = new TransactionScope())
            {
                 _context.SaveChanges();
                transaction.Complete();
            }
            
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUser user,ApplicationUserManager manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync( user, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
