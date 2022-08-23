using System;
using UnityEngine;

namespace TigerFrogGames
{
    [CreateAssetMenu(menuName = "EventChannel/GameStart")]
    public class EventChannelGameStart : ScriptableObject
    {
        public event Action<StartGameData> OnEvent;

        public void RaiseEvent(StartGameData gd)
        {
            OnEvent?.Invoke(gd);
        }

        public int GetNumberOfLiseners()
        {
            return OnEvent.GetInvocationList().Length;
        }
    }
    
    [Serializable]
    public struct StartGameData
    {
        [field: SerializeField] public int NumberOfPlayers { private set; get; }
        [field: SerializeField] public int Difficulty { private set; get; }
        
        [field: SerializeField] public int StartMin { private set; get; }
        [field: SerializeField] public int StartSec { private set; get; }
        
        public StartGameData(int numberOfPlayers, int difficulty, int startMin, int startSec)
        {
            NumberOfPlayers = numberOfPlayers;
            Difficulty = difficulty;
            
            StartMin = startMin;
            StartSec = startSec;
        }
    }
    
}

