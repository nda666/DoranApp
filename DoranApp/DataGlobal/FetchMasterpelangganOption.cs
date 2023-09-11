using DoranApp.Models;
using DoranApp.Utils;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace DoranApp.DataGlobal
{
    internal static class FetchMasterpelangganOption
    {
        private static readonly BehaviorSubject<List<MasterpelangganOption>> subject = new BehaviorSubject<List<MasterpelangganOption>>(new List<MasterpelangganOption>());
        private static bool IsRun = false;

        public static async Task Run()
        {
            if (IsRun)
            {
                return;
            }
            IsRun = true;
            var rest = new Rest("masterpelanggan/nama");
            var response = await rest.Get();
            IsRun = false;
            var data = (List<MasterpelangganOption>)response.Response;
            NotifyObservers(data);
        }

        public static IDisposable Subscribe(Action<List<MasterpelangganOption>> onNext)
        {
            return new CompositeDisposable(
                subject.Subscribe(new MyObserver(onNext)),
                Disposable.Create(() =>
                {
                    Console.WriteLine("Subscription has been disposed.");
                })
            );
        }

        private static void NotifyObservers(List<MasterpelangganOption> data)
        {
            subject.OnNext(data);
        }

        private class MyObserver : IObserver<List<MasterpelangganOption>>
        {
            private readonly Action<List<MasterpelangganOption>> _onNext;

            public MyObserver(Action<List<MasterpelangganOption>> onNext)
            {
                _onNext = onNext;
            }

            public void OnCompleted() { /* Implementation */ }
            public void OnError(Exception error) { /* Implementation */ }

            public void OnNext(List<MasterpelangganOption> value)
            {
                _onNext(value);
            }
        }
    }


}
