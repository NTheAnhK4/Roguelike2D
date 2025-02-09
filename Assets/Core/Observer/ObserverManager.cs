using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObserverManager
{
    private static readonly Dictionary<EventId, Action<object>> evenManager = new Dictionary<EventId, Action<object>>();

    public static void Attach(EventId eventId, Action<object> callback)
    {
        evenManager.TryAdd(eventId, null);
        evenManager[eventId] += callback;
        if(evenManager[eventId] == null) Debug.Log("Can't ");
    }

    public static void Detach(EventId eventId, Action<object> callback)
    {
        if (!evenManager.ContainsKey(eventId))
        {
            Debug.Log("Even Manager does'nt contain key " + eventId.ToString());
            return;
        }

        if (evenManager[eventId] == null)
        {
            Debug.Log(eventId.ToString() + " doesn't have any event");
            return;
        }

        evenManager[eventId] -= callback;
    }

    public static void Notify(EventId eventId, object param = null)
    {
        if (!evenManager.ContainsKey(eventId))
        {
            Debug.LogWarning("Doesn't contain key " + eventId.ToString() + " in the observer");
            return;
        }
        evenManager[eventId]?.Invoke(param);
    }
}

public enum EventId
{
    Lose,
    NewGame
}
