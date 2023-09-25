using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using DoranApp.Models;
using DoranApp.Utils;

namespace DoranApp.DataGlobal
{
    public static class FetchMasterbarangOptionWithSn
    {
        private static readonly BehaviorSubject<List<MasterbarangOptionWithSn>> subject =
            new BehaviorSubject<List<MasterbarangOptionWithSn>>(new List<MasterbarangOptionWithSn>());

        private static bool IsRun = false;

        public static async Task Run()
        {
            if (IsRun)
            {
                // return;
            }

            IsRun = true;
            var rest = new Rest("masterbarang/options");
            var response = await rest.Get(new
            {
                BrgAktif = true
            });
            var data = (List<MasterbarangOptionWithSn>)response.Response;
            NotifyObservers(data);

            IsRun = false;
        }

        public static IDisposable Subscribe(Action<List<MasterbarangOptionWithSn>> onNext)
        {
            return new CompositeDisposable(
                subject.Subscribe(new MyObserver(onNext)),
                Disposable.Create(() => { Console.WriteLine("Subscription masterbarang has been disposed."); })
            );
        }

        private static void NotifyObservers(List<MasterbarangOptionWithSn> data)
        {
            subject.OnNext(data);
        }

        private class MyObserver : IObserver<List<MasterbarangOptionWithSn>>
        {
            private readonly Action<List<MasterbarangOptionWithSn>> _onNext;

            public MyObserver(Action<List<MasterbarangOptionWithSn>> onNext)
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

            public void OnNext(List<MasterbarangOptionWithSn> value)
            {
                _onNext(value);
            }
        }
    }
}