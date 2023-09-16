using DoranApp.Models;
using DoranApp.Utils;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace DoranApp.DataGlobal
{
    internal static class FetchSetlevelharga
    {
        private static readonly BehaviorSubject<List<Setlevelharga>> subject = new BehaviorSubject<List<Setlevelharga>>(new List<Setlevelharga>());
        private static bool IsRun = false;

        public static async Task Run()
        {
            if (IsRun)
            {
                return;
            }
            IsRun = true;
            var rest = new Rest("setlevelharga");
            var response = await rest.Get();
            IsRun = false;
            var data = (List<Setlevelharga>)response.Response;
            NotifyObservers(data);
        }

        public static IDisposable Subscribe(Action<List<Setlevelharga>> onNext)
        {
            return new CompositeDisposable(
                subject.Subscribe(new MyObserver(onNext)),
                Disposable.Create(() =>
                {
                    Console.WriteLine("Subscription has been disposed.");
                })
            );
        }

        private static void NotifyObservers(List<Setlevelharga> data)
        {
            subject.OnNext(data);
        }

        private class MyObserver : IObserver<List<Setlevelharga>>
        {
            private readonly Action<List<Setlevelharga>> _onNext;

            public MyObserver(Action<List<Setlevelharga>> onNext)
            {
                _onNext = onNext;
            }

            public void OnCompleted() { /* Implementation */ }
            public void OnError(Exception error) { /* Implementation */ }

            public void OnNext(List<Setlevelharga> value)
            {
                _onNext(value);
            }
        }
    }
}
