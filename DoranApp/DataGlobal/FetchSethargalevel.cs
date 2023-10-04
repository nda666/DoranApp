using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using DoranApp.Utils;

namespace DoranApp.DataGlobal
{
    internal static class FetchSethargalevel
    {
        private static readonly BehaviorSubject<List<Sethargalevel>> subject =
            new BehaviorSubject<List<Sethargalevel>>(new List<Sethargalevel>());

        private static bool IsRun = false;

        public static async Task Run()
        {
            if (IsRun)
            {
                return;
            }

            IsRun = true;
            try
            {
                var rest = new Rest("setlevelharga");
                var response = await rest.Get();
                var data = (List<Sethargalevel>)response.Response;
                NotifyObservers(data);
            }
            catch (Exception ex)
            {
            }

            IsRun = false;
        }

        public static IDisposable Subscribe(Action<List<Sethargalevel>> onNext)
        {
            return new CompositeDisposable(
                subject.Subscribe(new MyObserver(onNext)),
                Disposable.Create(() => { Console.WriteLine("Subscription has been disposed."); })
            );
        }

        private static void NotifyObservers(List<Sethargalevel> data)
        {
            subject.OnNext(data);
        }

        private class MyObserver : IObserver<List<Sethargalevel>>
        {
            private readonly Action<List<Sethargalevel>> _onNext;

            public MyObserver(Action<List<Sethargalevel>> onNext)
            {
                _onNext = onNext;
            }

            public void OnCompleted()
            {
                /* Implementation */
            }

            public void OnError(Exception error)
            {
                /* Implementation */
            }

            public void OnNext(List<Sethargalevel> value)
            {
                _onNext(value);
            }
        }
    }
}