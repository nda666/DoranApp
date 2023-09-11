using DoranApp.Models;
using DoranApp.Utils;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace DoranApp.DataGlobal
{
    internal static class FetchLokasiProvinsiOption
    {
        private static readonly BehaviorSubject<List<LokasiProvinsiOption>> subject = new BehaviorSubject<List<LokasiProvinsiOption>>(new List<LokasiProvinsiOption>());
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
            var data = (List<LokasiProvinsiOption>)response.Response;
            NotifyObservers(data);
        }

        public static IDisposable Subscribe(Action<List<LokasiProvinsiOption>> onNext)
        {
            return new CompositeDisposable(
                subject.Subscribe(new MyObserver(onNext)),
                Disposable.Create(() =>
                {
                    Console.WriteLine("Subscription Lokasi Provinsi Option has been disposed.");
                })
            );
        }

        private static void NotifyObservers(List<LokasiProvinsiOption> data)
        {
            subject.OnNext(data);
        }

        private class MyObserver : IObserver<List<LokasiProvinsiOption>>
        {
            private readonly Action<List<LokasiProvinsiOption>> _onNext;

            public MyObserver(Action<List<LokasiProvinsiOption>> onNext)
            {
                _onNext = onNext;
            }

            public void OnCompleted() { /* Implementation */ }
            public void OnError(Exception error) { /* Implementation */ }

            public void OnNext(List<LokasiProvinsiOption> value)
            {
                _onNext(value);
            }
        }
    }
}
