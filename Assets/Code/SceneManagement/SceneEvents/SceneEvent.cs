using UnityEngine;
using System.Collections;
using System;

public class SceneEvent
{
    public delegate void evenHandler();
    public event evenHandler Event;

    string eventName;

    public SceneEvent(string eventName)
    {
        this.eventName = eventName;
    }

    public void InvokeEvent()
    {
        Event?.Invoke();
    }

    public void UnSubscribeAll()
    {
        if (Event != null)
        {
            Delegate[] clients = Event.GetInvocationList();

            foreach (Delegate d in clients)
            {
                Event -= (evenHandler)d;
            }
        }
    }
}

public class SceneEventInt
{
    public delegate void evenHandler(int amount);
    public event evenHandler OnEvent;

    string eventName;

    public SceneEventInt(string eventName)
    {
        this.eventName = eventName;
    }

    public void CallEvent(int amount)
    {
        OnEvent?.Invoke(amount);
    }

    public void UnSubscribeAll()
    {
        if (OnEvent != null)
        {
            Delegate[] clients = OnEvent.GetInvocationList();

            foreach (Delegate d in clients)
            {
                OnEvent -= (evenHandler)d;
            }
        }
    }
}
