using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer: MonoBehaviour
{
    // Start is called before the first frame update
    
    public AudioSource musicSource;
    public AudioClip[] songs;

    IEnumerator playSongs()
    {
        yield return null;
        var i = 0;
        var n = songs.Length;
        while(true)
        {
            musicSource.clip = songs[i];
            musicSource.Play();
            while (musicSource.isPlaying)
            {
                yield return null;  
            }
            i += 1;
            i %= n;

        } 
    }

    void Start()
    {
        StartCoroutine(playSongs());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
