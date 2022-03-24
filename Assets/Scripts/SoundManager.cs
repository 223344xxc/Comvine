using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    swipe,
    blockHit,
    clear,
    startGame,
}

public class SoundManager : MonoBehaviour
{
    public AudioClip[] sounds;

    private void Awake()
    {
    }

    private void Start()
    {
        PlaySound(SoundType.startGame);
    }

    public void PlaySound(SoundType type)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = sounds[(int)type];
        if (type == SoundType.blockHit)
            source.volume = 0.5f;
        
        source.Play();
        

        StartCoroutine(AudioRemover(source));
    }

    IEnumerator AudioRemover(AudioSource source)
    {
        yield return new WaitForSeconds(1);
        Destroy(source);
    }


}
