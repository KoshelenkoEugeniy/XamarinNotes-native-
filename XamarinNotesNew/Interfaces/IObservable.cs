﻿using System;
namespace XamarinNotesNew.Interfaces
{
    public interface IObservable
    {
        void RegisterObserver(IObserver newObserver);
        void RemoveObserver(IObserver currentObserver);
        void NotifyObservers(string answer);
    }

    public interface IObserver
    {
        void Update(Object myObject, string answer);
    }
}
