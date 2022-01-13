using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {
    #region Singleton

    protected static AudioManager s_Instance;

    public static AudioManager Instance {
        [RuntimeInitializeOnLoadMethod]
        get {
            if (s_Instance == null) {
                Debug.Log("AudioManager: Create new instance.");
                // s_Instance = ScriptableObject.CreateInstance<AudioManager>();
                CreateDefault();
            }
            return s_Instance;
        }
    }

    static void CreateDefault() {
        GameObject go = new GameObject("AudioManager", typeof(AudioManager), typeof(AudioSource));
        DontDestroyOnLoad(go);
        s_Instance = go.GetComponent<AudioManager>();
        // reduce fps
        // #if UNITY_EDITOR
        //     Application.targetFrameRate = -1;
        //     Debug.Log("Using Editor performance cap for my own sanity");
        // #endif
    }

    void OnEnable() {
        Debug.Log("AudioManager Enable");
        _init();
    }

    #endregion
    #region Data

    private AudioSource _backGroundMusic;

    #endregion
    #region Method

    void _init() {
        _backGroundMusic = GetComponent<AudioSource>();
    }
    #endregion

    #region Interface

    public void StartBackgroundMusic() {
        Debug.Log("play");
        _backGroundMusic = GetComponent<AudioSource>();
        _backGroundMusic.clip = Resources.Load<AudioClip>("Sounds/background");
        _backGroundMusic.volume = 0.5f;
        _backGroundMusic.loop = true;
        _backGroundMusic.Play();
    }

    public void Play(string audioName, float volume = 1.0f, bool isMuteBackgoundMusic = false, bool keepOnChangeScene = false) {
        GameObject go = new GameObject("AudioOneShot", typeof(AudioOneShot), typeof(AudioSource));
        AudioSource audioSource = go.GetComponent<AudioSource>();
        AudioClip clip = Resources.Load<AudioClip>("Sounds/" + audioName);

        if(keepOnChangeScene) {
            DontDestroyOnLoad(go);
        }
        if(isMuteBackgoundMusic){
            float originVolume = _backGroundMusic.volume;
            _backGroundMusic.volume = 0.1f;
            StartCoroutine(UnmuteBackgoundMusic(clip.length, originVolume));
        }
        _backGroundMusic.PlayOneShot(clip, volume);
        audioSource.clip = clip;
        audioSource.volume = volume;
        go.GetComponent<AudioOneShot>().Play();
    }

    IEnumerator UnmuteBackgoundMusic(float time, float volume = 0.5f){
        yield return new WaitForSeconds(time);
        _backGroundMusic.volume = volume;
    }

    #endregion
}