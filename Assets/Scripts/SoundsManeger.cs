using UnityEngine;

public class SoundsManeger : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioSource audioSources;
    public SoundArray[] randSounds;

    // Start is called before the first frame update
    void Start()
    {
        audioSources = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Метод воспроизведения звука
    public void PlaySound(int i, float volume = 1f, bool random = false, bool destroyed = false, float p1 = 0.85f, float p2 = 1.2f)
    {

        AudioClip clip = random ? randSounds[i].arrAudioClips[Random.Range(0, randSounds[i].arrAudioClips.Length)] : audioClips[i];
        audioSources.pitch = Random.Range(p1, p2);

        if (destroyed)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position, volume);
        }
        else
        {
            audioSources.PlayOneShot(clip, volume);
        }
    }

    [System.Serializable]
    public class SoundArray
    {
        public AudioClip[] arrAudioClips;
    }

}
