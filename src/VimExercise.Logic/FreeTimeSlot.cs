using System;


namespace VimExercise.Logic
{
    /// <inheritdoc/>
    public record FreeTimeSlot : TimeSlot
    {
        /// <inheritdoc/>
        public FreeTimeSlot(DateTimeOffset startTime, DateTimeOffset endTime) : base(startTime, endTime) { }
    }
}
