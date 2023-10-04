using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using DoranApp.Utils;

namespace DoranApp.DataGlobal
{
    internal static class FetchMastergudangBoleTransitOption
    {
        private static readonly string cName = "FetchCommonResultDto";

        private static readonly BehaviorSubject<List<CommonResultDto>> subject =
            new BehaviorSubject<List<CommonResultDto>>(new List<CommonResultDto>());

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
                var rest = new Rest("mastergudang/options");
                var response = await rest.Get(new
                {
                    Aktif = true,
                    Boletransit = true
                });
                var data = (List<CommonResultDto>)response.Response;
                NotifyObservers(data);
            }
            catch (Exception ex)
            {
            }

            IsRun = false;
        }

        public static IDisposable Subscribe(Action<List<CommonResultDto>> onNext)
        {
            return new CompositeDisposable(
                subject.Subscribe(new MyObserver(onNext)),
                Disposable.Create(() => { Console.WriteLine($"Subscription {cName} has been disposed."); })
            );
        }

        private static void NotifyObservers(List<CommonResultDto> data)
        {
            subject.OnNext(data);
        }

        private class MyObserver : IObserver<List<CommonResultDto>>
        {
            private readonly Action<List<CommonResultDto>> _onNext;

            public MyObserver(Action<List<CommonResultDto>> onNext)
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

            public void OnNext(List<CommonResultDto> value)
            {
                _onNext(value);
            }
        }
    }
}