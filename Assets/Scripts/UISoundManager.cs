using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundManager : MonoBehaviour
{
    private AudioSource source;
    // Start is called before the first frame update
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void SEShot(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
