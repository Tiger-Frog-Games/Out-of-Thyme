
using TMPro;
using UnityEngine;

namespace TigerFrogGames
{
    public class UITimerDisplayer : MonoBehaviour
    {
        #region Variables

        [SerializeField] private TMP_Text TimerText;

        [SerializeField] private EventChannelInt minEvent;
        [SerializeField] private EventChannelInt secEvent;

        private int currentMin;
        private int currentSec;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            minEvent.OnEvent += MinChange;
            secEvent.OnEvent += SecChange;
        }

        private void OnDisable()
        {
            minEvent.OnEvent -= MinChange;
            secEvent.OnEvent -= SecChange;
        }

        #endregion

        #region Methods

        private void MinChange(int i)
        {
            currentMin = i;
            RefreshText();
        }

        private void SecChange(int i)
        {
            currentSec = i;
            RefreshText();
        }

        private void RefreshText()
        {
            int displayMin = currentMin % 60;
            
            int displaySec = currentSec % 60;
            
            if (displaySec < 10)
            {
                TimerText.text = $"{displayMin}:0{displaySec}";
            }
            else
            {
                TimerText.text = $"{displayMin}:{displaySec}";
            }
        }

        #endregion
    }
}