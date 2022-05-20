using System;
using System.Diagnostics;
using System.Threading;
using Xamarin.Essentials;

namespace EggsTimer.Services
{
    public class VibratorService
    {
        private bool isVibrating;

        public event EventHandler<bool> IsVibratingChanged;

        public bool IsVibrating
        {
            get => isVibrating;
            private set
            {
                if (isVibrating == value)
                    return;

                isVibrating = value;
                IsVibratingChanged?.Invoke(this, isVibrating);
            }
        }

        public void Vibrate(double duration = 500)
        {
            if (IsVibrating)
                return;

            Debug.WriteLine($">>>>> {Environment.TickCount} VibratorService Vibrate({duration}) Start");

            IsVibrating = true;
            Vibration.Vibrate(duration);

            ThreadPool.QueueUserWorkItem((state) =>
            {
                Thread.Sleep((int)duration);
                IsVibrating = false;

                Debug.WriteLine($">>>>> {Environment.TickCount} VibratorService Vibrate({duration}) Stop");
            });
        }

        public void Cancel()
        {
            if (!IsVibrating)
                return;

            Vibration.Cancel();
            IsVibrating = false;
        }
    }
}