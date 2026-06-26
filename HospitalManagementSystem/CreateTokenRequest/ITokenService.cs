using Core.Entities;

namespace HospitalManagementSystem.CreateTokenRequest
{
    public interface ITokenService
    {
        public Task<string> CreateTokenAsync(User user);
    }
}
