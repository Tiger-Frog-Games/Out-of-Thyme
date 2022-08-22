using System;
using UnityEngine;

namespace TigerFrogGames
{
    [CreateAssetMenu(menuName = "EventChannel/Request")]
    public class EventChannelRequests : ScriptableObject
    {
        public event Action<Request> OnEvent;

        public void RaiseEvent(Request value)
        {
            OnEvent?.Invoke(value);
        }

    }
}