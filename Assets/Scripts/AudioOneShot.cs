using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioOneShot : MonoBehaviour {
    AudioSource _audioSource;
    bool _isPlayed;

    public void Play() {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Play();
        _isPlayed = true;
        Destroy(transform.gameObject, _audioSource.clip.length);
    }
}
