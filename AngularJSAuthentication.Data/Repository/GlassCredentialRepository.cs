using System;
using System.Threading.Tasks;
using AngularJSAuthentication.Data.Entities;
using AngularJSAuthentication.Data.Infrastructure;
using AngularJSAuthentication.Data.Interface;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace AngularJSAuthentication.Data.Repository
{
    public class GlassCredentialRepository 
        : MongoRepository<GlassCredential, string>, IGlassCredentialRepository
    {
        private bool _disposed;

        private readonly MongoRepository<GlassCredential> _repository;

        public  GlassCredentialRepository(MongoUrl mongoUrl)
        {
            _repository = new MongoRepository<GlassCredential>(); 
        }

        public GlassCredentialRepository(string connectionString)
            : base(connectionString)
        {
            _repository = new MongoRepository<GlassCredential>(connectionString);

            var pack = new ConventionPack();
            pack.Add(new CamelCaseElementNameConvention());
            pack.Add(new IgnoreIfNullConvention(true));

            ConventionRegistry.Register("camel case", pack, t => true);
        }

        public Task<GlassCredential> FindCredential(string userid)
        {
            ThrowIfDisposed();

            var query = Query.EQ("userId", userid);

            var result = collection.FindOneAs<GlassCredential>(query);
            return Task.FromResult(result);
        }

        public Task SaveCredential(GlassCredential credentail)
        {
            ThrowIfDisposed();
            if (credentail == null)
                throw new ArgumentNullException("credential");

            collection.Insert(credentail);
            return Task.FromResult(true);
        }

        /// <summary>
        ///     Throws if disposed.
        /// </summary>
        /// <exception cref="System.ObjectDisposedException"></exception>
        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);
        }
    }
}
