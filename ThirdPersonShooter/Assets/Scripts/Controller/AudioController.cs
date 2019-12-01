using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    [SerializeField] float delayBetweenClips;

    bool canPlay;
    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        canPlay = true;
    }

    public void Play()
    {       
        if (!canPlay)
            return;

        GameManager.instance.Timer.Add(() => {
            canPlay = true;
        }, delayBetweenClips);

        canPlay = false;

        int clipIndex = Random.Range(0, clips.Length);
        AudioClip audioClip = clips[clipIndex];
        source.PlayOneShot(audioClip);
    }
}
