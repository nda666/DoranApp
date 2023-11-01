using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using DoranApp.Utils;

namespace DoranApp.DataGlobal
{
    internal static class FetchSalesOption
    {
        private static readonly BehaviorSubject<List<SalesOptionDto>> subject =
            new BehaviorSubject<List<SalesOptionDto>>(new List<SalesOptionDto>());

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
                var rest = new Rest("sales/nama");
                var response = await rest.Get();
                var data = (List<SalesOptionDto>)response.Response;
                NotifyObservers(data);
                IsRun = false;
            }
            catch (Exception ex)
            {
                IsRun = false;
                throw;
            }

            IsRun = false;
        }

        public static IDisposable Subscribe(Action<List<SalesOptionDto>> onNext)
        {
            return new CompositeDisposable(
                subject.Subscribe(new MyObserver(onNext)),
                Disposable.Create(() => { Console.WriteLine("Subscription Sales Option has been disposed."); })
            );
        }

        private static void NotifyObservers(List<SalesOptionDto> data)
        {
            subject.OnNext(data);
        }

        private class MyObserver : IObserver<List<SalesOptionDto>>
        {
            private readonly Action<List<SalesOptionDto>> _onNext;

            public MyObserver(Action<List<SalesOptionDto>> onNext)
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

            public void OnNext(List<SalesOptionDto> value)
            {
                _onNext(value);
            }
        }
    }
}