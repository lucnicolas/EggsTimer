using System;

namespace EggsTimer.Models
{
    public class TimerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public EggsCookingTime Time { get; set; }
    }
}