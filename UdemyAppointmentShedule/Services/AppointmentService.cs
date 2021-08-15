using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using UdemyAppointmentShedule.Models;
using UdemyAppointmentShedule.Models.ViewModels;
using UdemyAppointmentShedule.Utility;

namespace UdemyAppointmentShedule.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _db;

        public AppointmentService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<int> AddUpdate(AppointmentViewModel model)
        {
            DateTime startDate = DateTime.Parse(model.StartDate, CultureInfo.CreateSpecificCulture("en-EN"));
            DateTime endDate = DateTime.Parse(model.StartDate, CultureInfo.CreateSpecificCulture("en-EN")).AddMinutes(Convert.ToDouble(model.Duration));
            if (model != null && model.Id > 0)
            {
                //update
                return 1;
            }
            else
            {
                //create
                Appointment appointment = new Appointment()
                {

                    Title = model.Title,
                    Description = model.Description,
                    StartDate = startDate,
                    EndDate = endDate,
                    Duration = model.Duration,
                    DoctorId = model.DoctorId,
                    PatientId = model.PatientId,
                    IsDoctorApproved = false,
                    AdminId = model.AdminId
                };
                _db.Appointments.Add(appointment);
                await _db.SaveChangesAsync();
                return 2;
            }
        }

        public List<DoctorVM> GetDoctorList()
        {
            var doctors = (from user in _db.Users
                           join usersRoles in _db.UserRoles on user.Id equals usersRoles.UserId
                           join roles in _db.Roles.Where(x => x.Name == Helper.Doctor) on usersRoles.RoleId equals roles.Id
                           select new DoctorVM
                           {
                               Id = user.Id,
                               Name = user.Name,
                           }).ToList() ;
            return doctors;
        }

        public List<PatientVM> GetPatientList()
        {
            var patients = (from user in _db.Users
                           join usersRoles in _db.UserRoles on user.Id equals usersRoles.UserId
                           join roles in _db.Roles.Where(x => x.Name == Helper.Patient) on usersRoles.RoleId equals roles.Id
                           select new PatientVM
                           {
                               Id = user.Id,
                               Name = user.Name,
                           }).ToList();
            return patients;
        }
    }
}
