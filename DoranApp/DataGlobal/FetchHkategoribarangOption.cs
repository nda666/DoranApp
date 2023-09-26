using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using DoranApp.Utils;

namespace DoranApp.DataGlobal
{
    internal static class FetchHkategoribarangOption
    {
        private static readonly string cName = "FetchHkategoribarangOption";

        private static readonly BehaviorSubject<List<HkategoribarangOptionDto>> subject =
            new BehaviorSubject<List<HkategoribarangOptionDto>>(new List<HkategoribarangOptionDto>());

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
            var data = (List<HkategoribarangOptionDto>)response.Response;
            NotifyObservers(data);
        }

        public static IDisposable Subscribe(Action<List<HkategoribarangOptionDto>> onNext)
        {
            return new CompositeDisposable(
                subject.Subscribe(new MyObserver(onNext)),
                Disposable.Create(() => { Console.WriteLine($"Subscription {cName} has been disposed."); })
            );
        }

        private static void NotifyObservers(List<HkategoribarangOptionDto> data)
        {
            subject.OnNext(data);
        }

        private class MyObserver : IObserver<List<HkategoribarangOptionDto>>
        {
            private readonly Action<List<HkategoribarangOptionDto>> _onNext;

            public MyObserver(Action<List<HkategoribarangOptionDto>> onNext)
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

            public void OnNext(List<HkategoribarangOptionDto> value)
            {
                _onNext(value);
            }
        }
    }
}