using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoSingleton<AudioManager> {

    #region data

    private AudioSource _backGroundMusic;

    #endregion

    #region MonoBehaviour
    void Awake() {
        _backGroundMusic = gameObject.AddComponent<AudioSource>();
    }

    #endregion

    #region public method

    public void StartBackgroundMusic() {
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

    #endregion

    #region private method

    private void _init() {
        _backGroundMusic = GetComponent<AudioSource>();
    }

    private IEnumerator UnmuteBackgoundMusic(float time, float volume = 0.5f){
        yield return new WaitForSeconds(time);
        _backGroundMusic.volume = volume;
    }

    #endregion

}