using System;
using UnityEngine;

namespace TigerFrogGames
{
    [CreateAssetMenu(menuName = "EventChannel/Base")]
    public class EventChannel : ScriptableObject
    {
        public event Action OnEvent;

        public void RaiseEvent()
        {
            OnEvent?.Invoke();
        }

        public int GetNumberOfLiseners()
        {
            return OnEvent.GetInvocationList().Length;
        }
    }
}