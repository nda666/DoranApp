using DoranApp.Models;
using DoranApp.Utils;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace DoranApp.DataGlobal
{
    internal static class FetchSalesOption
    {
        private static readonly BehaviorSubject<List<SalesOption>> subject = new BehaviorSubject<List<SalesOption>>(new List<SalesOption>());
        private static bool IsRun = false;

        public static async Task Run()
        {
            if (IsRun)
            {
                return;
            }
            IsRun = true;
            var rest = new Rest("sales/options");
            var response = await rest.Get();
            IsRun = false;
            var data = (List<SalesOption>)response.Response;
            NotifyObservers(data);
        }

        public static IDisposable Subscribe(Action<List<SalesOption>> onNext)
        {
            return new CompositeDisposable(
                subject.Subscribe(new MyObserver(onNext)),
                Disposable.Create(() =>
                {
                    Console.WriteLine("Subscription Lokasi Provinsi Option has been disposed.");
                })
            );
        }

        private static void NotifyObservers(List<SalesOption> data)
        {
            subject.OnNext(data);
        }

        private class MyObserver : IObserver<List<SalesOption>>
        {
            private readonly Action<List<SalesOption>> _onNext;

            public MyObserver(Action<List<SalesOption>> onNext)
            {
                _onNext = onNext;
            }

            public void OnCompleted() { /* Implementation */ }
            public void OnError(Exception error) { /* Implementation */ }

            public void OnNext(List<SalesOption> value)
            {
                _onNext(value);
            }
        }
    }
}
