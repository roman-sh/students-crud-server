using System;
using System.Collections.Generic;
using System.Linq;

namespace VimExercise.Logic
{
    /// <summary>
    /// represents a Schedule of free time slots
    /// </summary>
    public class Schedule
    {
        private readonly ICollection<FreeTimeSlot> freeTimeSlots;
        /// <summary>
        /// creates a Schedule instance
        /// </summary>
        /// <param name="freeTimeSlots">the list of all free time slots in the schedule</param>
        public Schedule(IEnumerable<FreeTimeSlot> freeTimeSlots)
        {
            this.freeTimeSlots = freeTimeSlots.ToList();
        }
        /// <summary>
        /// checks weather an appointment can be made at a given time.
        /// </summary>
        /// <param name="timeForAppointement">the time in which the appointment needs to be set</param>
        /// <returns>true if there is time in the schedule for an appointment, otherwise false</returns>
        public bool CanScheduleAppointment(DateTimeOffset timeForAppointement)
        {
            FreeTimeSlot freeTimeSlotAtAppointemntTime = freeTimeSlots
                .Where(freeTimeSlot => freeTimeSlot.Contains(timeForAppointement))
                .SingleOrDefault();
            if(freeTimeSlotAtAppointemntTime is null)
                return false;
            return true;
        }
    }
}
