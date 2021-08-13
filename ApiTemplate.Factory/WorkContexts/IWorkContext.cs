using System.Threading.Tasks;
using ApiTemplate.Core.Entities.Users;

namespace ApiTemplate.Factory.WorkContexts
{
    public interface IWorkContext
    {
        AppUser CurrentUser { get; }
    }
}