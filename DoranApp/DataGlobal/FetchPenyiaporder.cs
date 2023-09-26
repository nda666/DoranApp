using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using DoranApp.Utils;

namespace DoranApp.DataGlobal
{
    internal static class FetchPenyiaporder
    {
        private static readonly BehaviorSubject<List<Penyiaporder>> subject =
            new BehaviorSubject<List<Penyiaporder>>(new List<Penyiaporder>());

        private static bool IsRun = false;

        public static async Task Run()
        {
            if (IsRun)
            {
                return;
            }

            IsRun = true;
            var rest = new Rest("penyiaporder");
            var response = await rest.Get();
            IsRun = false;
            var data = (List<Penyiaporder>)response.Response;
            NotifyObservers(data);
        }

        public static IDisposable Subscribe(Action<List<Penyiaporder>> onNext)
        {
            return new CompositeDisposable(
                subject.Subscribe(new MyObserver(onNext)),
                Disposable.Create(() => { Console.WriteLine("Subscription has been disposed."); })
            );
        }

        private static void NotifyObservers(List<Penyiaporder> data)
        {
            subject.OnNext(data);
        }

        private class MyObserver : IObserver<List<Penyiaporder>>
        {
            private readonly Action<List<Penyiaporder>> _onNext;

            public MyObserver(Action<List<Penyiaporder>> onNext)
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

            public void OnNext(List<Penyiaporder> value)
            {
                _onNext(value);
            }
        }
    }
}