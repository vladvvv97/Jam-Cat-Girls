using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get => _instance; private set => _instance = value; }
    private static AudioManager _instance;
    public MusicManager Music;
    public SoundsManager Sounds;

    public enum eAudioNames
    {
        Add, Alert, Back,
        Button, Collision,
        DestroyFirst, DestroySecond,
        Drop, GameOver, Spend, Swipe,
        InGameOST, MainMenuOST
    };

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(this.gameObject); }
        DontDestroyOnLoad(this.gameObject);

        Music.InitializeMusic();
        Sounds.InitializeSounds();

        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    private void OnActiveSceneChanged(Scene current, Scene next)
    {
        switch (next.buildIndex)
        {
            case 0:
                AudioManager.Instance.Music.StopMusic();
                AudioManager.Instance.Music.PlayMusic(eAudioNames.MainMenuOST);
                break;

            case 1:
                AudioManager.Instance.Music.StopMusic();
                AudioManager.Instance.Music.PlayMusic(eAudioNames.InGameOST);
                break;

            default:
                return;
        }
    }

    [System.Serializable]
    public class AudioClips
    {
        public eAudioNames name;
        public AudioClip audioClip;
    }

    [System.Serializable]
    public class MusicManager
    {
        [SerializeField] private string musicManagerName;
        [SerializeField] private bool isMute = false;
        [SerializeField] [Range(0.0f, 1.0f)] private float musicVolume = 1f;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Toggle musicToggle;
        [SerializeField] private AudioSource audioSource;
        public AudioClips[] audioClips;

        private string IsMute { get => musicManagerName + "IsMute"; }
        private string MusicVolume { get => musicManagerName + "Volume"; }
        public void InitializeMusic()
        {
            audioSource.playOnAwake = true;
            audioSource.loop = true;

            if (PlayerPrefs.HasKey(IsMute))
            {
                isMute = System.Convert.ToBoolean(PlayerPrefs.GetString(IsMute));

                audioSource.mute = isMute;

                musicToggle.SetIsOnWithoutNotify(isMute);
            }
            if (PlayerPrefs.HasKey(MusicVolume))
            {
                musicVolume = PlayerPrefs.GetFloat(MusicVolume);

                audioSource.volume = musicVolume;

                musicSlider.value = musicVolume;
            }
        }
        public void MuteMusic()
        {
            isMute = !isMute;

            audioSource.mute = isMute;

            PlayerPrefs.SetString(IsMute, isMute.ToString());
            PlayerPrefs.Save();
        }
        public void MuteMusic(bool value)
        {
            audioSource.mute = value;
        }

        public void ChangeMusicVolume(Slider slider)
        {
            musicVolume = slider.value;

            audioSource.volume = musicVolume;

            PlayerPrefs.SetFloat(MusicVolume, musicVolume);
            PlayerPrefs.Save();
        }

        public void PlayMusic(eAudioNames audioName)
        {
            foreach (var audioClip in audioClips)
            {
                if (audioClip.name == audioName)
                {
                    audioSource.clip = audioClip.audioClip;
                    audioSource.loop = true;
                    audioSource.Play();
                    return;
                }
            }
        }
        public void StopMusic()
        {
            audioSource.Stop();
        }
        public void PauseMusic()
        {
            audioSource.Pause();
        }
        public void UnPauseMusic()
        {
            audioSource.UnPause();
        }
    }

    [System.Serializable]
    public class SoundsManager
    {
        [SerializeField] private string soundsManagerName;
        [SerializeField] private bool isMute = false;
        [SerializeField] [Range(0.0f, 1.0f)] private float soundsVolume = 1f;
        [SerializeField] private Slider soundsSlider;
        [SerializeField] private Toggle soundsToggle;
        [SerializeField] private AudioSource audioSource;
        public AudioClips[] audioClips;

        private string IsMute { get => soundsManagerName + "IsMute"; }
        private string SoundsVolume { get => soundsManagerName + "Volume"; }
        public void InitializeSounds()
        {
            if (PlayerPrefs.HasKey(IsMute))
            {
                isMute = System.Convert.ToBoolean(PlayerPrefs.GetString(IsMute));

                audioSource.mute = isMute;

                soundsToggle.SetIsOnWithoutNotify(isMute);
            }
            if (PlayerPrefs.HasKey(SoundsVolume))
            {
                soundsVolume = PlayerPrefs.GetFloat(SoundsVolume);

                audioSource.volume = soundsVolume;

                soundsSlider.value = soundsVolume;
            }
        }
        public void MuteSounds()
        {
            isMute = !isMute;

            audioSource.mute = isMute;

            PlayerPrefs.SetString(IsMute, isMute.ToString());
            PlayerPrefs.Save();
        }

        public void ChangeSoundsVolume(Slider slider)
        {
            soundsVolume = slider.value;

            audioSource.volume = soundsVolume;

            PlayerPrefs.SetFloat(SoundsVolume, soundsVolume);
            PlayerPrefs.Save();
        }

        public void PlaySound(eAudioNames audioName)
        {
            foreach (var audioClip in audioClips)
            {
                if (audioClip.name == audioName)
                {
                    audioSource.PlayOneShot(audioClip.audioClip);
                    return;
                }
            }
        }
    }
}

