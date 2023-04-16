using PMS.Core.Entities;
using PMS.Infrastructure.Dto;

namespace PMS.Infrastructure.Interfaces
{
    public interface IJwtService
    {
        public JwtDto CreateToken(User user);
    }
}