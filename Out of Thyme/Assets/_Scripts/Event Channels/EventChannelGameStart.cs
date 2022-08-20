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
    
    public struct StartGameData
    {
        public int NumberOfPlayers { private set; get; }
        public int Difficulty { private set; get; }


        public StartGameData(int numberOfPlayers, int difficulty)
        {
            NumberOfPlayers = numberOfPlayers;
            Difficulty = difficulty;
        }
    }
    
}

