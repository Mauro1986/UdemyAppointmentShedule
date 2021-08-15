using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyAppointmentShedule.Models.ViewModels;

namespace UdemyAppointmentShedule.Services
{
    public interface IAppointmentService
    {
        public List<DoctorVM> GetDoctorList();
       public List<PatientVM> GetPatientList();
       public Task<int> AddUpdate(AppointmentViewModel model);

    }
}
