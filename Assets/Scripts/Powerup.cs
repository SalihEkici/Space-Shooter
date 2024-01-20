using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private AudioClip _clip;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if (gameObject.transform.position.y < -4.0f)
        {
            Destroy(this.gameObject);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        
        if (player != null)
        {
            AudioSource.PlayClipAtPoint(_clip, transform.position);
            switch (gameObject.tag)
            {
                case "Tripleshot Powerup":

                    player.TripleShotActive();
                    break;
                case "Speed Powerup":
                    player.SpeedActive();
                    break;
                case "Shield Powerup":
                    player.ShieldActive();
                    break;
            }
            Destroy (this.gameObject );
        }
    }
}
