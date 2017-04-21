using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playlist : MonoBehaviour {

    Object[] MyMusic;

    void Awake()
    {
        MyMusic = Resources.LoadAll("Music", typeof(AudioClip));
        GetComponent<AudioSource>().clip = MyMusic[0] as AudioClip;
    }
    // Use this for initialization
    void Start () {
        GetComponent<AudioSource>().Play();
	}
	
	// Update is called once per frame
	void Update () {
        if (!GetComponent<AudioSource>().isPlaying)
        {
            PlayRandomMusic();
        }
	}

    void PlayRandomMusic()
    {
        GetComponent<AudioSource>().clip = MyMusic[Random.Range(0,MyMusic.Length)] as AudioClip;
        GetComponent<AudioSource>().Play();
    }
}
