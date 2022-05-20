using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using EggsTimer.Models;
using Xamarin.Forms;

namespace EggsTimer.Services
{
    public class TimerService
    {
        public async Task<TimerModel> Create(EggsCookingTime time)
        {
            return new TimerModel(){ Name = Enum.GetName(typeof(EggsCookingTime), time), Time = time};
        }

        public async Task<TimerModel> Delete(TimerModel model)
        {
            return null;
        }
    }
}