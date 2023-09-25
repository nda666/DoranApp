using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using DoranApp.Models;
using DoranApp.Utils;

namespace DoranApp.DataGlobal
{
    internal static class FetchMastergudangBoleTransitOption
    {
        private static readonly string cName = "FetchMastergudangOption";

        private static readonly BehaviorSubject<List<MastergudangOption>> subject =
            new BehaviorSubject<List<MastergudangOption>>(new List<MastergudangOption>());

        private static bool IsRun = false;

        public static async Task Run()
        {
            if (IsRun)
            {
                return;
            }

            IsRun = true;
            var rest = new Rest("mastergudang/options");
            var response = await rest.Get(new
            {
                Aktif = true,
                Boletransit = true
            });
            IsRun = false;
            var data = (List<MastergudangOption>)response.Response;
            NotifyObservers(data);
        }

        public static IDisposable Subscribe(Action<List<MastergudangOption>> onNext)
        {
            return new CompositeDisposable(
                subject.Subscribe(new MyObserver(onNext)),
                Disposable.Create(() => { Console.WriteLine($"Subscription {cName} has been disposed."); })
            );
        }

        private static void NotifyObservers(List<MastergudangOption> data)
        {
            subject.OnNext(data);
        }

        private class MyObserver : IObserver<List<MastergudangOption>>
        {
            private readonly Action<List<MastergudangOption>> _onNext;

            public MyObserver(Action<List<MastergudangOption>> onNext)
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

            public void OnNext(List<MastergudangOption> value)
            {
                _onNext(value);
            }
        }
    }
}