using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    
    [SerializeField] private GameEvent _event;
    [SerializeField] private UnityEvent _raised;

    private void OnEnable()
    {
        _event.SubscribeToEvent(this);
    }

    private void OnDisable()
    {
        _event.UnsubscribeFromEvent(this);
    }

    public void OnRaiseEvent()
    {
        _raised?.Invoke();
    }
}