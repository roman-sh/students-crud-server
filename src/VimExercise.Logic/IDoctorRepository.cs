using System;
using System.Collections.Generic;

namespace VimExercise.Logic
{
    /// <inheritdoc/>
    public interface IDoctorRepository : IRepository<Doctor>
    {
        /// <param name="appoitmentTime">the time searched for the appointment</param>
        /// <param name="requiredSpecialty">the specialty the doctor needs for the appointment</param>
        /// <param name="scoreThreshold">the minimum score that a doctor needs</param>
        /// <returns>all suitable doctors for an appointment according to the query parameters, in a descending sorted manner by their score</returns>
        IEnumerable<Doctor> GetSuitableDoctorsSorted(DateTimeOffset appoitmentTime, Specialty requiredSpecialty, double scoreThreshold);
    }
}
