using DoranApp.Models;
using DoranApp.Utils;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace DoranApp.DataGlobal
{
    internal static class FetchMasterchannelsalesOption
    {
        private static readonly BehaviorSubject<List<MasterchannelsalesOption>> subject = new BehaviorSubject<List<MasterchannelsalesOption>>(new List<MasterchannelsalesOption>());
        private static bool IsRun = false;

        public static async Task Run()
        {
            if (IsRun)
            {
                return;
            }
            IsRun = true;
            var rest = new Rest("masterchannelsales/withtimandsales");
            var response = await rest.Get();
            IsRun = false;
            var data = (List<MasterchannelsalesOption>)response.Response;
            NotifyObservers(data);
        }

        public static IDisposable Subscribe(Action<List<MasterchannelsalesOption>> onNext)
        {
            return new CompositeDisposable(
                subject.Subscribe(new MyObserver(onNext)),
                Disposable.Create(() =>
                {
                    Console.WriteLine("Subscription Lokasi Provinsi Option has been disposed.");
                })
            );
        }

        private static void NotifyObservers(List<MasterchannelsalesOption> data)
        {
            subject.OnNext(data);
        }

        private class MyObserver : IObserver<List<MasterchannelsalesOption>>
        {
            private readonly Action<List<MasterchannelsalesOption>> _onNext;

            public MyObserver(Action<List<MasterchannelsalesOption>> onNext)
            {
                _onNext = onNext;
            }

            public void OnCompleted() { /* Implementation */ }
            public void OnError(Exception error) { /* Implementation */ }

            public void OnNext(List<MasterchannelsalesOption> value)
            {
                _onNext(value);
            }
        }
    }
}
