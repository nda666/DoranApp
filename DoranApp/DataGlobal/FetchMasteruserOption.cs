using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using ConsoleDump;
using DoranApp.Utils;

namespace DoranApp.DataGlobal
{
    internal static class FetchMasteruserOption
    {
        private static readonly BehaviorSubject<List<MasteruserOptionDto>> subject =
            new BehaviorSubject<List<MasteruserOptionDto>>(new List<MasteruserOptionDto>());

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
                var response = await client.Get_User_OptionsAsync();
                NotifyObservers(response.ToList());
                IsRun = false;
            }
            catch (Exception ex)
            {
                ex.Dump();
                IsRun = false;
            }

            IsRun = false;
        }

        public static IDisposable Subscribe(Action<List<MasteruserOptionDto>> onNext)
        {
            return new CompositeDisposable(
                subject.Subscribe(new MyObserver(onNext)),
                Disposable.Create(() => { Console.WriteLine("Subscription has been disposed."); })
            );
        }

        private static void NotifyObservers(List<MasteruserOptionDto> data)
        {
            subject.OnNext(data);
        }

        private class MyObserver : IObserver<List<MasteruserOptionDto>>
        {
            private readonly Action<List<MasteruserOptionDto>> _onNext;

            public MyObserver(Action<List<MasteruserOptionDto>> onNext)
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

            public void OnNext(List<MasteruserOptionDto> value)
            {
                _onNext(value);
            }
        }
    }
}