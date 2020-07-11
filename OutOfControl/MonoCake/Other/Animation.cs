using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCake
{
    public class Animation
    {

        public static double Linear(double startValue = 0, double endValue = 1, double startTime = 0, double duration = 1, double currentTime = 0, double currentValue = 0)
        {
            if (currentTime < startTime || currentTime > startTime + duration)
                return currentValue;

            double progress = (currentTime - startTime) / duration;
            return startValue + (endValue - startValue) * progress;
        }

        public static double Parabolic(double startValue = 0, double endValue = 1, double startTime = 0, double duration = 1, double currentTime = 0, double currentValue = 0, double power = 1)
        {
            if (currentTime < startTime || currentTime > startTime + duration)
                return currentValue;

            double progress = (currentTime - startTime) / duration;

            return startValue + (endValue - startValue) * Math.Pow(progress, power);
        }
        public static double Sin(double startValue = 0, double endValue = 1, double startTime = 0, double duration = 1, double currentTime = 0, double currentValue = 0, double curve = 1)
        {
            if (currentTime < startTime || currentTime > startTime + duration)
                return currentValue;

            double progress = (currentTime - startTime) / duration;
            progress = Math.Sin(progress * Math.PI / 2);
            return startValue + (endValue - startValue) * progress;
        }

        public static double Recurrent(double currentValue = 0, double endValue = 1, double startTime = 0, double duration = 1, double currentTime = 0, double divBy = 2)
        {
            if (currentTime < startTime || currentTime > startTime + duration)
                return currentValue;

            return currentValue + (endValue - currentValue) / divBy;
        }

        public static double Trigger(double newValue = 1, double time = 0, double currentTime = 0, double currentValue = 0)
        {
            if (currentTime == time)
            {
                return newValue;
            }
            return currentValue;
        }

        public static double Trigger(double minTime = 0, double maxTime = 1, double currentTime = 0, double newValue = 1, double currentValue = 0)
        {
            if (currentTime >= minTime && currentTime <= maxTime)
            {
                return newValue;
            }
            return currentValue;
        }

        public static bool Binary(bool newValue = true, double time = 0, double currentTime = 0, bool currentValue = false)
        {
            if (currentTime == time)
            {
                return newValue;
            }
            return currentValue;
        }

        public static void RunMethod(Func<int> a)
        {

        }


        public static List<Double> DurationsToTimes(List<Double> durations)
        {
            List<Double> times = new List<double>();

            double sum = 0;
            foreach (var a in durations)
            {
                sum += a;
                times.Add(sum);
            }

            return times;
        }

    }
}
