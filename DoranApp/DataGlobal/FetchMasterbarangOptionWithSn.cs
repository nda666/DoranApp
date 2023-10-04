using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using DoranApp.Utils;

namespace DoranApp.DataGlobal
{
    public static class FetchMasterbarangOptionWithSn
    {
        private static readonly BehaviorSubject<List<MasterbarangOptionWithSnDto>> subject =
            new BehaviorSubject<List<MasterbarangOptionWithSnDto>>(new List<MasterbarangOptionWithSnDto>());

        private static bool IsRun = false;

        public static async Task Run()
        {
            if (IsRun)
            {
                // return;
            }

            IsRun = true;
            try
            {
                var rest = new Rest("masterbarang/options");
                var response = await rest.Get(new
                {
                    BrgAktif = true
                });
                var data = (List<MasterbarangOptionWithSnDto>)response.Response;
                NotifyObservers(data);
            }
            catch (Exception ex)
            {
            }

            IsRun = false;
        }

        public static IDisposable Subscribe(Action<List<MasterbarangOptionWithSnDto>> onNext)
        {
            return new CompositeDisposable(
                subject.Subscribe(new MyObserver(onNext)),
                Disposable.Create(() => { Console.WriteLine("Subscription masterbarang has been disposed."); })
            );
        }

        private static void NotifyObservers(List<MasterbarangOptionWithSnDto> data)
        {
            subject.OnNext(data);
        }

        private class MyObserver : IObserver<List<MasterbarangOptionWithSnDto>>
        {
            private readonly Action<List<MasterbarangOptionWithSnDto>> _onNext;

            public MyObserver(Action<List<MasterbarangOptionWithSnDto>> onNext)
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

            public void OnNext(List<MasterbarangOptionWithSnDto> value)
            {
                _onNext(value);
            }
        }
    }
}