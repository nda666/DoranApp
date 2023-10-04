using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using DoranApp.Utils;

namespace DoranApp.DataGlobal
{
    internal static class FetchLokasiKota
    {
        private static readonly BehaviorSubject<List<LokasiKota>> subject =
            new BehaviorSubject<List<LokasiKota>>(new List<LokasiKota>());

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
                var rest = new Rest("lokasikota");
                var response = await rest.Get();
                var data = (List<LokasiKota>)response.Response;
                NotifyObservers(data);
            }
            catch (Exception ex)
            {
            }

            IsRun = false;
        }

        public static IDisposable Subscribe(Action<List<LokasiKota>> onNext)
        {
            return new CompositeDisposable(
                subject.Subscribe(new MyObserver(onNext)),
                Disposable.Create(() => { Console.WriteLine("Subscription has been disposed."); })
            );
        }

        private static void NotifyObservers(List<LokasiKota> data)
        {
            subject.OnNext(data);
        }

        private class MyObserver : IObserver<List<LokasiKota>>
        {
            private readonly Action<List<LokasiKota>> _onNext;

            public MyObserver(Action<List<LokasiKota>> onNext)
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

            public void OnNext(List<LokasiKota> value)
            {
                _onNext(value);
            }
        }
    }
}