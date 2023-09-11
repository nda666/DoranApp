using DoranApp.Models;
using DoranApp.Utils;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace DoranApp.DataGlobal
{
    internal static class FetchHkategoribarangOption
    {
        private static readonly string cName = "FetchHkategoribarangOption";
        private static readonly BehaviorSubject<List<HkategoribarangOption>> subject = new BehaviorSubject<List<HkategoribarangOption>>(new List<HkategoribarangOption>());
        private static bool IsRun = false;

        public static async Task Run()
        {
            if (IsRun)
            {
                return;
            }
            IsRun = true;
            var rest = new Rest("hkategoribarang/with-dkategoribarang");
            var response = await rest.Get(new
            {
                Aktif = true
            });
            IsRun = false;
            var data = (List<HkategoribarangOption>)response.Response;
            NotifyObservers(data);
        }

        public static IDisposable Subscribe(Action<List<HkategoribarangOption>> onNext)
        {
            return new CompositeDisposable(
                subject.Subscribe(new MyObserver(onNext)),
                Disposable.Create(() =>
                {
                    Console.WriteLine($"Subscription {cName} has been disposed.");
                })
            );
        }

        private static void NotifyObservers(List<HkategoribarangOption> data)
        {
            subject.OnNext(data);
        }

        private class MyObserver : IObserver<List<HkategoribarangOption>>
        {
            private readonly Action<List<HkategoribarangOption>> _onNext;

            public MyObserver(Action<List<HkategoribarangOption>> onNext)
            {
                _onNext = onNext;
            }

            public void OnCompleted() { /* Implementation */ }
            public void OnError(Exception error) { /* Implementation */ }

            public void OnNext(List<HkategoribarangOption> value)
            {
                _onNext(value);
            }
        }
    }
}
