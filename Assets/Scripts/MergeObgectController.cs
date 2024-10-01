using UnityEngine;

public class MergeObgectController : SoundsManeger
{

    [SerializeField] private int _level;
    [SerializeField] private int _points;
    [SerializeField] private bool _isSpawned;
    [SerializeField] private GameObject _nextMergetObgect;
    [SerializeField] private ParticleSystem _previousMergetObgect;
    [SerializeField] private float _explosionForce = 150f; // Сила отталкивания



    private void OnEnable()
    {
        _previousMergetObgect.Play();

    }

    private void Start()
    {
        audioSources = FindObjectOfType<PlayerInputController>().GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Тук");
        
        if (collision.GetComponent<MergeObgectController>())
        {
            PlaySound(1, 0.2f);

            MergeObgectController merge = collision.GetComponent<MergeObgectController>();

            if (!merge._isSpawned && !collision.isTrigger && merge._level == _level)
            {
                Destroy(collision.gameObject);
                _isSpawned = true;

                FindObjectOfType<PlayerInputController>().AddPoints(_points * _level);

                GameObject newObgect = Instantiate(_nextMergetObgect, Vector2.Lerp(transform.position, collision.transform.position, .5f), Quaternion.identity);
                Instantiate(_previousMergetObgect, Vector2.Lerp(transform.position, collision.transform.position, .5f), Quaternion.identity);
                PlaySound(0);

                ApplyExplosionForce(newObgect);
                _previousMergetObgect.Play(true);
                Destroy(gameObject);
            }
        }
    }
    

    private void ApplyExplosionForce(GameObject obj)
    {
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 explosionDirection = (rb.position - (Vector2)transform.position).normalized;
            rb.AddForce(explosionDirection * _explosionForce);
        }
    }
       
  
}
