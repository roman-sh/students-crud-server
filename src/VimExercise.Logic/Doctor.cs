using System;
using System.Collections.Generic;

namespace VimExercise.Logic
{
    /// <summary>
    /// represents a doctor (provider)
    /// </summary>
    public record Doctor(string Name, IEnumerable<Specialty> Specialties, Schedule Schedule, double Score) : IComparable<Doctor>
    {
        /// <inheritdoc/>
        public int CompareTo(Doctor other)
        {
            return Score.CompareTo(other.Score);
        }
        /// <summary>
        /// determents weather an appointment can be made with the doctor in a given time.
        /// </summary>
        /// <param name="timeForAppointement">the time of the appointment</param>
        /// <returns>true if appointment can be made, otherwise false</returns>
        public bool CanScheduleAppointment(DateTimeOffset timeForAppointement)
        {
            return Schedule.CanScheduleAppointment(timeForAppointement);
        }
    }
}
