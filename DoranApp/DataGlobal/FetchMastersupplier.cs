using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using DoranApp.Utils;

namespace DoranApp.DataGlobal
{
    internal static class FetchMastersupplier
    {
        private static readonly BehaviorSubject<List<MastersupplierOptionsDto>> subject =
            new BehaviorSubject<List<MastersupplierOptionsDto>>(new List<MastersupplierOptionsDto>());

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
                var client = new Client();
                var response = await client.Get_Mastersupplier_OptionsAsync();
                NotifyObservers(response.ToList());
                IsRun = false;
            }
            catch (Exception ex)
            {
                IsRun = false;
            }

            IsRun = false;
        }

        public static IDisposable Subscribe(Action<List<MastersupplierOptionsDto>> onNext)
        {
            return new CompositeDisposable(
                subject.Subscribe(new MyObserver(onNext)),
                Disposable.Create(() => { Console.WriteLine("Subscription has been disposed."); })
            );
        }

        private static void NotifyObservers(List<MastersupplierOptionsDto> data)
        {
            subject.OnNext(data);
        }

        private class MyObserver : IObserver<List<MastersupplierOptionsDto>>
        {
            private readonly Action<List<MastersupplierOptionsDto>> _onNext;

            public MyObserver(Action<List<MastersupplierOptionsDto>> onNext)
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

            public void OnNext(List<MastersupplierOptionsDto> value)
            {
                _onNext(value);
            }
        }
    }
}