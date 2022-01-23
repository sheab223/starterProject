using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PESMTextKill : MonoBehaviour
{


    public AudioSource musicSource;
    public AudioClip introMusic;

    // Start is called before the first frame update
    void Start()
    {
        musicSource.clip = introMusic;
        musicSource.Play();
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
           
    }
}
