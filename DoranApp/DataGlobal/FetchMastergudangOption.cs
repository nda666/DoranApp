using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using DoranApp.Utils;

namespace DoranApp.DataGlobal
{
    internal static class FetchMastergudangOption
    {
        private static readonly string cName = "FetchMastergudangOption";

        private static readonly BehaviorSubject<List<MastergudangOptionDto>> subject =
            new BehaviorSubject<List<MastergudangOptionDto>>(new List<MastergudangOptionDto>());

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
                Aktif = true
            });
            IsRun = false;
            var data = (List<MastergudangOptionDto>)response.Response;
            NotifyObservers(data);
        }

        public static IDisposable Subscribe(Action<List<MastergudangOptionDto>> onNext)
        {
            return new CompositeDisposable(
                subject.Subscribe(new MyObserver(onNext)),
                Disposable.Create(() => { Console.WriteLine($"Subscription {cName} has been disposed."); })
            );
        }

        private static void NotifyObservers(List<MastergudangOptionDto> data)
        {
            subject.OnNext(data);
        }

        private class MyObserver : IObserver<List<MastergudangOptionDto>>
        {
            private readonly Action<List<MastergudangOptionDto>> _onNext;

            public MyObserver(Action<List<MastergudangOptionDto>> onNext)
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

            public void OnNext(List<MastergudangOptionDto> value)
            {
                _onNext(value);
            }
        }
    }
}