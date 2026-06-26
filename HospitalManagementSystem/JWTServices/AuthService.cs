using Core.Entities;
using Core.Exceptions;
using HospitalManagementSystem.CreateTokenRequest;
using HospitalManagementSystem.JWTModels;
using Microsoft.EntityFrameworkCore;
using Repository.Context;

namespace HospitalManagementSystem.JWTServices
{
    public class AuthService : IAuthService
    {
        private readonly HospitalManagementSystemDbContext _dbContext;
        private readonly ITokenService _tokenService;
        public AuthService(HospitalManagementSystemDbContext dbContext , ITokenService tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }
        public async Task<string> RegisterPatientAsync(RegisterPatientModel registerPatientModel)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var existsUserName = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == registerPatientModel.UserName);
                if (existsUserName is not null)
                    throw new ValidationException("This username is already exists.");

                var existsEmailAddress = await _dbContext.Users.FirstOrDefaultAsync(u => u.EmailAddress == registerPatientModel.EmailAddress);
                if (existsEmailAddress is not null)
                    throw new ValidationException("This emailaddress is already exists.");

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerPatientModel.Password);

                // Get Patient Role
                var patientRole = await _dbContext.Roles.FirstOrDefaultAsync(r => r.RoleName == "Patient");
                if (patientRole is null)
                    throw new ValidationException("Patient role not found.");


                // Create User
                var user = new User
                {
                    UserName = registerPatientModel.UserName,
                    EmailAddress = registerPatientModel.EmailAddress,
                    PasswordHash = hashedPassword,
                    RoleID = patientRole.ID,
                    isActive = true,
                    CreatedAt = DateTime.Now
                };

                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();

                // Create Patient
                var patient = new Patient
                {
                    FullName = registerPatientModel.FullName,
                    PhoneNumber = registerPatientModel.PhoneNumber,
                    EmergencyContactInformation = registerPatientModel.EmergencyContactInformation,
                    Gender = registerPatientModel.Gender,
                    DateOfBirth = registerPatientModel.DateOfBirth,
                    BloodType = registerPatientModel.BloodType,
                    Address = registerPatientModel.Address,
                    RegistrationDate = DateTime.Now,
                    UserID = user.ID
                };

                await _dbContext.Patients.AddAsync(patient);
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();

                return "Patient registered successfully";
            }

            catch(Exception ex)
            {
                await transaction.RollbackAsync();
                return ex.Message;
            }
        }

        public async Task<string> LoginAsync(LoginModel loginModel)
        {
            var existsEmailAddress = await _dbContext.Users.Include(u => u.Role)
                                          .FirstOrDefaultAsync(u => u.EmailAddress == loginModel.EmailAddress);

            if (existsEmailAddress is null)
                throw new ValidationException("This emailaddress or password isn't correct.");

            var verifyPassword = BCrypt.Net.BCrypt.Verify(loginModel.Password, existsEmailAddress.PasswordHash);
            if(!verifyPassword)
                throw new ValidationException("This emailaddress or password isn't correct.");

            var token = await _tokenService.CreateTokenAsync(existsEmailAddress);

            return token;
        }

    }
}
