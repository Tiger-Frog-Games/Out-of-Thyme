using System;
using UnityEngine;

namespace TigerFrogGames
{
    [CreateAssetMenu(menuName = "EventChannel/ItemData")]
    public class EventChannelItemData : ScriptableObject
    {
        public event Action<ItemData> OnEvent;

        public void RaiseEvent(ItemData value)
        {
            OnEvent?.Invoke(value);
        }

        public int GetNumberOfLiseners()
        {
            return OnEvent.GetInvocationList().Length;
        }
    }
}