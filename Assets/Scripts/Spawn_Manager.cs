using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _tripleShotPowerUpPrefab;
    [SerializeField]
    private GameObject _speedPowerUpPrefab;
    [SerializeField]
    private GameObject _shieldPowerUpPrefab;


    private bool _stopSpawning = false;
    private int powerupID;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(-9, 9), 6.0f, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(2);
        }
    }
    IEnumerator SpawnPowerupRoutine()
    {
        while (_stopSpawning == false)
        {
            powerupID = Random.Range(0, 3);
            switch (powerupID)
            {
                case 0:
                    Instantiate(_tripleShotPowerUpPrefab, new Vector3(Random.Range(-9f, 9f), 7, 0), Quaternion.identity);
                    break;
                case 1:
                    Instantiate(_speedPowerUpPrefab, new Vector3(Random.Range(-9f, 9f), 7, 0), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(_shieldPowerUpPrefab, new Vector3(Random.Range(-9f, 9f), 7, 0), Quaternion.identity);
                    break;
            }
            yield return new WaitForSeconds(Random.Range(3,8));
        }
    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
