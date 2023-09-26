using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using DoranApp.Utils;

namespace DoranApp.DataGlobal
{
    internal static class FetchMasterchannelsalesOption
    {
        private static readonly BehaviorSubject<List<MasterchannelsalesOptionDto>> subject =
            new BehaviorSubject<List<MasterchannelsalesOptionDto>>(new List<MasterchannelsalesOptionDto>());

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
            var data = (List<MasterchannelsalesOptionDto>)response.Response;
            NotifyObservers(data);
        }

        public static IDisposable Subscribe(Action<List<MasterchannelsalesOptionDto>> onNext)
        {
            return new CompositeDisposable(
                subject.Subscribe(new MyObserver(onNext)),
                Disposable.Create(() =>
                {
                    Console.WriteLine("Subscription Lokasi Provinsi Option has been disposed.");
                })
            );
        }

        private static void NotifyObservers(List<MasterchannelsalesOptionDto> data)
        {
            subject.OnNext(data);
        }

        private class MyObserver : IObserver<List<MasterchannelsalesOptionDto>>
        {
            private readonly Action<List<MasterchannelsalesOptionDto>> _onNext;

            public MyObserver(Action<List<MasterchannelsalesOptionDto>> onNext)
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

            public void OnNext(List<MasterchannelsalesOptionDto> value)
            {
                _onNext(value);
            }
        }
    }
}