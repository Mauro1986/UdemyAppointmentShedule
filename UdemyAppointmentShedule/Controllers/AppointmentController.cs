using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyAppointmentShedule.Services;
using UdemyAppointmentShedule.Utility;

namespace UdemyAppointmentShedule.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentservice)
        {
            _appointmentService = appointmentservice;
        }
        public IActionResult Index()
        {
            ViewBag.Duration = Helper.GetTimeDropDown();
           ViewBag.DoctorList= _appointmentService.GetDoctorList();
           ViewBag.PatientList= _appointmentService.GetPatientList();
            return View();
        }
    }
}
