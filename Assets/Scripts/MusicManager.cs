using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip[] music;
    void Start()
    {
        int random = Random.Range(0, music.Length);
        GetComponent<AudioSource>().PlayOneShot(music[random], 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
