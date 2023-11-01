using System;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using DoranApp.Utils;

namespace DoranApp.DataGlobal
{
    internal abstract class FetchBase<T>
    {
        protected bool IsRun = false;
        protected BehaviorSubject<T> subject = new BehaviorSubject<T>(default(T));

        protected abstract string RestUrl { get; }

        protected T Data { get; set; }


        public async Task<FetchBase<T>> Run()
        {
            if (IsRun)
            {
                return this;
            }

            var rest = new Rest(RestUrl);
            var response = await rest.Get();
            Data = response.Response;
            IsRun = true;
            NotifyObservers();
            return this;
        }

        public IDisposable Subscribe(Action<T> onNext)
        {
            return new CompositeDisposable(
                subject.Subscribe(new MyObserver(onNext)),
                Disposable.Create(() => { Console.WriteLine("Subscription has been disposed."); })
            );
        }

        protected void NotifyObservers()
        {
            subject.OnNext(Data);
        }

        private class MyObserver : IObserver<T>
        {
            private readonly Action<T> _onNext;

            public MyObserver(Action<T> onNext)
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

            public void OnNext(T value)
            {
                _onNext(value);
            }
        }
    }
}