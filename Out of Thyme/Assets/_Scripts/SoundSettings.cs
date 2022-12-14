using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace TigerFrogGames
{
    public class SoundSettings : MonoBehaviour
    {
        #region Variables

        [SerializeField] private float minSound, MaxSound;

        [SerializeField] private AudioMixer theMixer;

        [SerializeField] private TMP_Text mainLabel, musicLabel, sfxLabel;
        [SerializeField] private Slider mainSlider, musicSlider, sfxSlider;

        #endregion

        #region Unity Methods

        private void Start()
        {

            if (PlayerPrefs.HasKey("MasterVol"))
            {
                theMixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol"));
            }

            if (PlayerPrefs.HasKey("MusicVol"))
            {
                theMixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVol"));
            }

            if (PlayerPrefs.HasKey("SFXVol"))
            {
                theMixer.SetFloat("SFXVol", PlayerPrefs.GetFloat("SFXVol"));
            }

            float vol = 0f;
            

            theMixer.GetFloat("MasterVol", out vol);
            mainSlider.value = vol;
            
            theMixer.GetFloat("MusicVol", out vol);
            musicSlider.value = vol;

            theMixer.GetFloat("SFXVol", out vol);
            sfxSlider.value = vol;

            mainLabel.text = Mathf.RoundToInt(normalizeFloat(mainSlider.value)).ToString();
            musicLabel.text = Mathf.RoundToInt(normalizeFloat(musicSlider.value)).ToString();
            sfxLabel.text = Mathf.RoundToInt(normalizeFloat(sfxSlider.value)).ToString();
        }

        #endregion

        #region Methods

        public void SetMasterVol()
        {
            mainLabel.text = Mathf.RoundToInt(normalizeFloat(mainSlider.value)).ToString();

            theMixer.SetFloat("MasterVol", mainSlider.value);

            PlayerPrefs.SetFloat("MasterVol", mainSlider.value);
        }

        public void SetMusicVol()
        {
            musicLabel.text = Mathf.RoundToInt(normalizeFloat(musicSlider.value)).ToString();

            theMixer.SetFloat("MusicVol", musicSlider.value);

            PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
        }

        public void SetSFXVol()
        {
            sfxLabel.text = Mathf.RoundToInt(normalizeFloat(sfxSlider.value )).ToString();

            theMixer.SetFloat("SFXVol", sfxSlider.value);

            PlayerPrefs.SetFloat("SFXVol", sfxSlider.value);
        }

        private float normalizeFloat(float floatIn)
        {
            return ( ((floatIn - minSound) / ( MaxSound - minSound )) * 100 );
        }

        #endregion
    }
}