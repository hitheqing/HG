/********************************************************************
	Created:	2018-12-03
	Filename: 	EventObservable.cs
	Author:		Heq
	QQ:         372058864
*********************************************************************/

using System;

namespace HG
{
    

public class EventObservable : IObservable<object[]>
{
    private readonly PriorityList<PriorityEventObserver> m_observers;

    public EventObservable(int capacity = 1)
    {
        m_observers = new PriorityList<PriorityEventObserver>(capacity);
    }

    public IDisposable Subscribe(IObserver<object[]> observer)
    {
        PriorityEventObserver peo = (PriorityEventObserver) observer;
        m_observers.Add(peo);
        return new Disposable(m_observers, peo);
    }

    public void Notify(object[] args)
    {
        foreach (PriorityEventObserver t in m_observers)
        {
            MainThreadScheduler.Instance.Immediate(() => { t.OnNext(args); });
        }
    }

    public void ShutDown()
    {
        foreach (PriorityEventObserver t in m_observers)
        {
            t.OnCompleted();
        }

        m_observers.Clear();
    }

    private class Disposable : IDisposable
    {
        private readonly PriorityList<PriorityEventObserver> m_list;
        private readonly PriorityEventObserver m_observer;

        public Disposable(PriorityList<PriorityEventObserver> list, PriorityEventObserver observer)
        {
            m_list = list;
            m_observer = observer;
        }

        public void Dispose()
        {
            m_list.Remove(m_observer);
        }
    }
}

public class EventObservable<T> : IObservable<T>
{
    private readonly PriorityList<PriorityEventObserver<T>> m_observers;

    public EventObservable(int capacity = 4)
    {
        m_observers = new PriorityList<PriorityEventObserver<T>>(capacity);
    }

    public IDisposable Subscribe(IObserver<T> observer)
    {
        PriorityEventObserver<T> peo = (PriorityEventObserver<T>) observer;
        m_observers.Add(peo);
        return new Disposable(m_observers, peo);
    }

    public void Notify(T t)
    {
        foreach (PriorityEventObserver<T> observer in m_observers)
        {
            MainThreadScheduler.Instance.Immediate(() => { observer.OnNext(t); });
        }
    }

    public void ShutDown()
    {
        foreach (PriorityEventObserver<T> t in m_observers)
        {
            t.OnCompleted();
        }

        m_observers.Clear();
    }

    private class Disposable : IDisposable
    {
        private readonly PriorityList<PriorityEventObserver<T>> m_list;
        private readonly PriorityEventObserver<T> m_observer;

        public Disposable(PriorityList<PriorityEventObserver<T>> list, PriorityEventObserver<T> observer)
        {
            m_list = list;
            m_observer = observer;
        }

        public void Dispose()
        {
            m_list.Remove(m_observer);
        }
    }
}
}