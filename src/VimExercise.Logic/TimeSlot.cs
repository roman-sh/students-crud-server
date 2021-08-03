namespace VimExercise.Logic
{
    using System;
    public record TimeSlot
    {
        /// <summary>
        /// represents a continuous range of time 
        /// </summary>
        /// <param name="startTime">the start of time range</param>
        /// <param name="endTime">the end of the time range</param>
        public TimeSlot(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            if(startTime > endTime)
                throw new ArgumentException("The end of the time slot is before the start of it", nameof(endTime));
            StartTime = startTime;
            EndTime = endTime;
        }
        /// <summary>
        /// the end  of the time range
        /// </summary>
        public DateTimeOffset EndTime
        {
            get;
        }
        /// <summary>
        /// the start of the time range
        /// </summary>
        public DateTimeOffset StartTime
        {
            get;
        }
        /// <summary>
        /// checks weather the given time is contained in the time slot
        /// </summary>
        /// <param name="nestedTime">the time to check if it is being contained</param>
        /// <returns><see langword="true"/>if the time is contained in the time slot, otherwise false</returns>
        public bool Contains(DateTimeOffset nestedTime)
        {
            return nestedTime <= this.EndTime && nestedTime >= this.StartTime;
        }
    }
}
