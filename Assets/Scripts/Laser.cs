using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;
    private bool _isEnemyLaser = false;
    void Update()
    {
        if (_isEnemyLaser == true)
        {
            MoveDown();
        }
        else
        {
            MoveUp();
        }
    }

    void MoveUp()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        if (transform.position.y > 8 || transform.position.y < -5.50)
        {
            Destroy(gameObject);
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }

    void MoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -8 || transform.position.y < -5.50)
        {
            Destroy(gameObject);
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }

}
