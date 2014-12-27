using System.Threading.Tasks;
using AngularJSAuthentication.Data.Entities;

namespace AngularJSAuthentication.Data.Interface
{
    public interface IGlassCredentialRepository
    {
        Task<GlassCredential> FindCredential(string glass);

        Task SaveCredential(GlassCredential credentail);
    }
}
