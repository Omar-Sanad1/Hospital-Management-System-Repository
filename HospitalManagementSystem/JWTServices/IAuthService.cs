using HospitalManagementSystem.JWTModels;

namespace HospitalManagementSystem.JWTServices
{
    public interface IAuthService
    {
        public Task<string> RegisterPatientAsync(RegisterPatientModel registerPatientModel);
        public Task<string> LoginAsync(LoginModel loginModel);
    }
}
