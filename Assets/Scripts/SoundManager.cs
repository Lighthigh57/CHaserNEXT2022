using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip[] sounds;
    // Start is called before the first frame update
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = sounds[Random.Range(0, sounds.Length)];
        _audioSource.Play();
    }

}
