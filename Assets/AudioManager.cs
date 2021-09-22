using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip[] audiolist;
    List<AudioSource> source = new List<AudioSource>();


    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i<audiolist.Length; i++)
        {
            source.Add(new AudioSource());
            source[i] = gameObject.AddComponent<AudioSource>();
            source[i].clip = audiolist[i];
        }
    }

    public void playsound (int s)
    {
        source[s].Play();
    }


   
}
