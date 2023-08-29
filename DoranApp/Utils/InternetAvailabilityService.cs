namespace DoranApp.Utils
{
    using System;
    using System.Net.NetworkInformation;
    using System.Reactive.Linq;

    public static class InternetAvailabilityService
    {
        public static bool isOnline;

        public static bool? forceStatus = null;

        public static IObservable<bool> CheckInternetAvailability()
        {
            return Observable.Interval(TimeSpan.FromSeconds(5))
            .StartWith(-1L)
            .Select(_ => IsInternetAvailable());
        }

        private static bool IsInternetAvailable()
        {
            if (forceStatus.HasValue)
            {
                isOnline = (bool)forceStatus;
                return isOnline;
            }
            try
            {
                Ping ping = new Ping();
                PingReply reply = ping.Send("8.8.8.8", 1000);
                isOnline = reply != null && reply.Status == IPStatus.Success;
                return isOnline;
            }
            catch (Exception)
            {
                isOnline = false;
                return false;
            }
        }
    }
}