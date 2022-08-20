using System;
using UnityEngine;

namespace TigerFrogGames
{
    [CreateAssetMenu(menuName = "EventChannel/Float")]
    public class EventChannelFloat : ScriptableObject
    {
        
        public event Action<float> OnEvent;

        public void RaiseEvent(float value)
        {
            OnEvent?.Invoke(value);
        }

        public int GetNumberOfLiseners()
        {
            return OnEvent.GetInvocationList().Length;
        }

    }
}