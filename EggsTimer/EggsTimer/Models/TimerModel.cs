using System;

namespace EggsTimer.Models
{
    public class TimerModel
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public double Duration { get; set; }
    }
}