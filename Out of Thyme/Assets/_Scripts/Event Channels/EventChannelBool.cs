using System;
using UnityEngine;

namespace TigerFrogGames
{
    [CreateAssetMenu(menuName = "EventChannel/Bool")]
    public class EventChannelBool : ScriptableObject
    {
        public event Action<bool> OnEvent;

        public void RaiseEvent(bool value)
        {
            OnEvent?.Invoke( value );
        }

        public int GetNumberOfLiseners()
        {
            return OnEvent.GetInvocationList().Length;
        }
    }
}
