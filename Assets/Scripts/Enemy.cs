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
    

    void Start()
    {
        transform.position = new Vector3(Random.Range(-9, 9), 6.0f, 0);
         _player = GameObject.Find("Player").GetComponent<Player>();
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
            Destroy(this.gameObject);
            //add 10 score
            _player.AddScore(10);
            Destroy(other.gameObject);

        }
        if (other.tag == "Player")
        {
            
            if (_player != null) {
                _player.Damage();
            }
            Destroy(this.gameObject);
        }
       
    }
}
