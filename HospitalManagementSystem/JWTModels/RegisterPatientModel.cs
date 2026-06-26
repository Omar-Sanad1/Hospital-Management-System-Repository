namespace HospitalManagementSystem.JWTModels
{
    public class RegisterPatientModel
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string BloodType { get; set; }
        public string Address { get; set; }
        public string EmergencyContactInformation { get; set; }
    }
}
