using System;
using UnityEngine;

namespace TigerFrogGames
{
    [CreateAssetMenu(menuName = "EventChannel/Int")]
    public class EventChannelInt : ScriptableObject
    {
        public event Action<int> OnEvent;

        public void RaiseEvent(int value)
        {
            OnEvent?.Invoke(value);
        }

        public int GetNumberOfLiseners()
        {
            return OnEvent.GetInvocationList().Length;
        }
    }
}