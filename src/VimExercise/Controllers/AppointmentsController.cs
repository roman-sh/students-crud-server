using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using VimExercise.Logic;

namespace VimExercise.Web.Controllers
{
    /// <summary>
    /// represents a controller for scheduling appointments and searching for suitable doctors. 
    /// </summary>
    [Controller]
    [Route("[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly ILogger<AppointmentsController> _logger;
        private readonly IDoctorRepository doctorRepository;
        /// <summary>
        /// creates a new AppointmentsController instance
        /// </summary>
        /// <param name="logger">the logger of errors, info and other messages that are generated as a result of requests</param>
        /// <param name="doctorRepository">the repository for doctors used to answer the requests</param>
        public AppointmentsController(ILogger<AppointmentsController> logger, IDoctorRepository doctorRepository)
        {
            _logger = logger;
            this.doctorRepository = doctorRepository;
        }
        /// <param name="specialty">the specialty the doctor needs for the appointment</param>
        /// <param name="date">the time searched for the appointment</param>
        /// <param name="minScore">the minimum score that a doctor needs</param>
        /// <returns>all suitable doctors for an appointment according to the query parameters, in a descending sorted manner by their score. (200 if requests was successful, otherwise 400)</returns>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get(string specialty, long date, double minScore)
        {
            _logger.LogInformation($"Requested available doctor. Search parameters:{Environment.NewLine}" +
                $"specialty: {specialty}, date (unix): {date}, minimum score: {minScore}.");
            try
            {

                return Ok(doctorRepository.GetSuitableDoctorsSorted(DateTimeOffset.FromUnixTimeMilliseconds(date), new Specialty(specialty), minScore)
                    .Select(doc => doc.Name));

            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// sets an appointment  with a given doctor on a given date.
        /// </summary>
        /// <param name="appoitmentDto">the appointment data</param>
        /// <returns>200 if successful, otherwise 400</returns>
        [HttpPost]
        public ActionResult Post(AppoitmentDto appoitmentDto)
        {
            Doctor doctor = null;
            try
            {

                doctor = doctorRepository.TryGetById(appoitmentDto.Name);
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
            if(doctor is null)
            {
                _logger.LogError($"Requested appointment with doctor {appoitmentDto.Name}, name doesn't exists in the system. " +
                    $"Responded with 400 code.");
                return BadRequest("Doctor doesn't exist.");
            }
            if(doctor.Schedule.CanScheduleAppointment(DateTimeOffset.FromUnixTimeMilliseconds(appoitmentDto.Date)))
            {
                _logger.LogInformation($"Requested appointment with doctor {appoitmentDto.Name}. Appointment was set.");
                return Ok();
            }
            _logger.LogError($"Requested appointment with doctor {appoitmentDto.Name}. Doctor does not accept appointments in the requested time." +
                $"Responded with 400 code.");
            return BadRequest("Doctor does not accept appointments in the requested time");
        }
    }
}
