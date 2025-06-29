using System;
using System.Collections.Generic;using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Scriptable Objects/GameEvent")]
public class GameEvent : ScriptableObject
{
    public void SubscribeToEvent(GameEventListener listener)
    {
        _listeners.Add(listener);
    }

    public void UnsubscribeFromEvent(GameEventListener listener)
    {
        _listeners.Remove(listener);
    }

    public void Raise()
    {
        for (int i =  _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnRaiseEvent();
        }
    }
    #region privates

    private List<GameEventListener> _listeners = new List<GameEventListener>();
    
    #endregion
}