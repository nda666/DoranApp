using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using DoranApp.Utils;

namespace DoranApp.DataGlobal
{
    internal static class FetchLokasiProvinsiOption
    {
        private static readonly BehaviorSubject<List<LokasiProvinsi>> subject =
            new BehaviorSubject<List<LokasiProvinsi>>(new List<LokasiProvinsi>());

        private static bool IsRun = false;

        public static async Task Run()
        {
            if (IsRun)
            {
                return;
            }

            IsRun = true;
            var rest = new Rest("lokasiprovinsi/withkota");
            var response = await rest.Get();
            IsRun = false;
            var data = (List<LokasiProvinsi>)response.Response;
            NotifyObservers(data);
        }

        public static IDisposable Subscribe(Action<List<LokasiProvinsi>> onNext)
        {
            return new CompositeDisposable(
                subject.Subscribe(new MyObserver(onNext)),
                Disposable.Create(() =>
                {
                    Console.WriteLine("Subscription Lokasi Provinsi Option has been disposed.");
                })
            );
        }

        private static void NotifyObservers(List<LokasiProvinsi> data)
        {
            subject.OnNext(data);
        }

        private class MyObserver : IObserver<List<LokasiProvinsi>>
        {
            private readonly Action<List<LokasiProvinsi>> _onNext;

            public MyObserver(Action<List<LokasiProvinsi>> onNext)
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

            public void OnNext(List<LokasiProvinsi> value)
            {
                _onNext(value);
            }
        }
    }
}