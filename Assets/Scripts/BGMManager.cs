using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [SerializeField] private AudioClip bgmClip;
    private AudioSource audio_source;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        audio_source = gameObject.AddComponent<AudioSource>();
        audio_source.clip = bgmClip;
        audio_source.volume = 0.2f;
        audio_source.Play();
    }
}
