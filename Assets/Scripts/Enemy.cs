using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 1.0f;
    public GameObject enemy;
    private Player _player;
    private Animator m_Animator;
    private AudioSource m_AudioSource;
    void Start()
    {
        transform.position = new Vector3(Random.Range(-9, 9), 6.0f, 0);
         _player = GameObject.Find("Player").GetComponent<Player>();
        if ( _player == null )
        {
            Debug.LogError("PLAYER IS NULL");
        }
        m_Animator = gameObject.GetComponent<Animator>();
        if (m_Animator == null)
        {
            Debug.LogError("Animator IS NULL");
        }
        m_AudioSource = gameObject.GetComponent<AudioSource>();
        if (m_AudioSource == null)
        {
            Debug.LogError("Audio Source for Enemy is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= -4f)
        {
            transform.position = new Vector3(Random.Range(-9, 9), 6.0f, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Laser") {
            Destroy(other.gameObject);
            m_Animator.SetTrigger("OnEnemyDeath");
            GetComponent<BoxCollider2D>().enabled = false;
            _speed = 0f;
            m_AudioSource.Play();
            Destroy(this.gameObject, 2.8f);

            //add 10 score
            _player.AddScore(10);
            
        }
        if (other.tag == "Player")
        {
            
            if (_player != null) {
                _player.Damage();
            }
            m_Animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            m_AudioSource.Play();
            Destroy(this.gameObject, 2.8f);
        }
        
    }

}
