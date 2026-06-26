using Core.Entities;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BCrypt.Net;

namespace Repository
{
    public class HospitalSystemDataSeeding
    {
        public static async Task SeedAsync(HospitalManagementSystemDbContext dbContext)
        {
            // Roles

            if (!dbContext.Roles.Any())
            {
                var roles = File.ReadAllText("../Repository/DataSeed/roles.json");
                var rolesData = JsonSerializer.Deserialize<List<Role>>(roles);
                if (rolesData?.Count > 0)
                {
                    foreach (var role in rolesData)
                    {
                        await dbContext.Roles.AddAsync(role);
                    }
                }
                await dbContext.SaveChangesAsync();
            }
            //////////////////////////////////////////////////////////////////////////////
            // Departments

            if (!dbContext.Departments.Any())
            {
                var departments = File.ReadAllText("../Repository/DataSeed/departments.json");
                var departmentsData = JsonSerializer.Deserialize<List<Department>>(departments);
                if (departmentsData?.Count > 0)
                {
                    foreach (var department in departmentsData)
                    {
                        await dbContext.Departments.AddAsync(department);
                    }
                }
                await dbContext.SaveChangesAsync();
            }

            //////////////////////////////////////////////////////////////////////////////
            // Users

            if (!dbContext.Users.Any())
            {
                var users = File.ReadAllText("../Repository/DataSeed/users.json");
                var usersData = JsonSerializer.Deserialize<List<User>>(users);
                if (usersData?.Count > 0)
                {
                    foreach (var user in usersData)
                    {
                        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
                        await dbContext.Users.AddAsync(user);
                    }
                }
                await dbContext.SaveChangesAsync();
            }

            //////////////////////////////////////////////////////////////////////////////
            // Doctors

            if (!dbContext.Doctors.Any())
            {
                var doctors = File.ReadAllText("../Repository/DataSeed/doctors.json");
                var doctorsData = JsonSerializer.Deserialize<List<Doctor>>(doctors);
                if (doctorsData?.Count > 0)
                {
                    foreach (var doctor in doctorsData)
                    {
                        await dbContext.Doctors.AddAsync(doctor);
                    }
                }
                await dbContext.SaveChangesAsync();
            }

            //////////////////////////////////////////////////////////////////////////////
            // Patients
            if (!dbContext.Patients.Any())
            {
                var patients = File.ReadAllText("../Repository/DataSeed/patients.json");
                var patientsData = JsonSerializer.Deserialize<List<Patient>>(patients);
                if (patientsData?.Count > 0)
                {
                    foreach (var patient in patientsData)
                    {
                        await dbContext.Patients.AddAsync(patient);
                    }
                }
                await dbContext.SaveChangesAsync();
            }

            //////////////////////////////////////////////////////////////////////////////
            // Appointments

            if (!dbContext.Appointments.Any())
            {
                var appointments = File.ReadAllText("../Repository/DataSeed/appointments.json");
                var appointmentsData = JsonSerializer.Deserialize<List<Appointment>>(appointments);
                if (appointmentsData?.Count > 0)
                {
                    foreach (var appointment in appointmentsData)
                    {
                        await dbContext.Appointments.AddAsync(appointment);
                    }
                }
                await dbContext.SaveChangesAsync();
            }

            //////////////////////////////////////////////////////////////////////////////
            // MedicalRecords

            if (!dbContext.MedicalRecords.Any())
            {
                var medicalrecords = File.ReadAllText("../Repository/DataSeed/medicalrecords.json");
                var medicalrecordsData = JsonSerializer.Deserialize<List<MedicalRecord>>(medicalrecords);
                if (medicalrecordsData?.Count > 0)
                {
                    foreach (var medicalRecord in medicalrecordsData)
                    {
                        await dbContext.MedicalRecords.AddAsync(medicalRecord);
                    }
                }
                await dbContext.SaveChangesAsync();
            }

            //////////////////////////////////////////////////////////////////////////////
            // Prescriptions

            if (!dbContext.Prescriptions.Any())
            {
                var prescriptions = File.ReadAllText("../Repository/DataSeed/prescriptions.json");
                var prescriptionsData = JsonSerializer.Deserialize<List<Prescription>>(prescriptions);
                if (prescriptionsData?.Count > 0)
                {
                    foreach (var prescription in prescriptionsData)
                    {
                        await dbContext.Prescriptions.AddAsync(prescription);
                    }
                }
                await dbContext.SaveChangesAsync();
            }

            //////////////////////////////////////////////////////////////////////////////
            // LaboratoryTests

            if (!dbContext.LaboratoryTests.Any())
            {
                var laboratorytests = File.ReadAllText("../Repository/DataSeed/laboratorytests.json");
                var laboratorytestsData = JsonSerializer.Deserialize<List<LaboratoryTest>>(laboratorytests);
                if (laboratorytestsData?.Count > 0)
                {
                    foreach (var laboratoryTest in laboratorytestsData)
                    {
                        await dbContext.LaboratoryTests.AddAsync(laboratoryTest);
                    }
                }
                await dbContext.SaveChangesAsync();
            }

            //////////////////////////////////////////////////////////////////////////////
            // Bills

            if (!dbContext.Bills.Any())
            {
                var bills = File.ReadAllText("../Repository/DataSeed/bills.json");
                var billsData = JsonSerializer.Deserialize<List<Bill>>(bills);
                if (billsData?.Count > 0)
                {
                    foreach (var bill in billsData)
                    {
                        await dbContext.Bills.AddAsync(bill);
                    }
                }
                await dbContext.SaveChangesAsync();
            }

            //////////////////////////////////////////////////////////////////////////////
            // Payments

            if (!dbContext.Payments.Any())
            {
                var payments = File.ReadAllText("../Repository/DataSeed/payments.json");
                var paymentsData = JsonSerializer.Deserialize<List<Payment>>(payments);
                if (paymentsData?.Count > 0)
                {
                    foreach (var payment in paymentsData)
                    {
                        await dbContext.Payments.AddAsync(payment);
                    }
                }
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
