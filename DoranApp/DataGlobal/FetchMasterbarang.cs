using DoranApp.Models;
using DoranApp.Utils;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace DoranApp.DataGlobal
{
    internal static class FetchMasterbarang
    {
        private static readonly BehaviorSubject<List<Masterbarang>> subject = new BehaviorSubject<List<Masterbarang>>(new List<Masterbarang>());
        private static bool IsRun = false;

        public static async Task Run()
        {
            if (IsRun)
            {
                return;
            }
            IsRun = true;
            var rest = new Rest("masterbarang");
            var response = await rest.Get();
            IsRun = false;
            var data = (List<Masterbarang>)response.Response;
            NotifyObservers(data);
        }

        public static IDisposable Subscribe(Action<List<Masterbarang>> onNext)
        {
            return new CompositeDisposable(
                subject.Subscribe(new MyObserver(onNext)),
                Disposable.Create(() =>
                {
                    Console.WriteLine("Subscription has been disposed.");
                })
            );
        }

        private static void NotifyObservers(List<Masterbarang> data)
        {
            subject.OnNext(data);
        }

        private class MyObserver : IObserver<List<Masterbarang>>
        {
            private readonly Action<List<Masterbarang>> _onNext;

            public MyObserver(Action<List<Masterbarang>> onNext)
            {
                _onNext = onNext;
            }

            public void OnCompleted() { /* Implementation */ }
            public void OnError(Exception error) { /* Implementation */ }

            public void OnNext(List<Masterbarang> value)
            {
                _onNext(value);
            }
        }
    }
}
