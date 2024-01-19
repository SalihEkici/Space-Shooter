using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _playerSpeed = 3.5f;
    [SerializeField]
    private float _fireRate = 0.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private int _playerHealth = 3;
    private float _nextFireTime = 0f;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    private Spawn_Manager _spawnManager;
    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField]
    private bool _isSpeedActive = false;
    [SerializeField]
    private bool _isShieldActive = false;
    [SerializeField]
    private GameObject _shieldVisualizer;
    private int _score;
    private UIManager _uiManager;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, -1, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFireTime)
        {
            CharacterAttack();
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Player movement
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        if(_isSpeedActive == true)
        {
            transform.Translate(direction * Time.deltaTime * _playerSpeed*2);
            
        }
        else {
            transform.Translate(direction * Time.deltaTime * _playerSpeed);
        }
        // Player bounds
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);
        if (transform.position.x < -9.2f)
        {
            transform.position = new Vector3(9.2f, transform.position.y, 0);
        }
        if (transform.position.x > 9.2f)
        {
            transform.position = new Vector3(-9.2f, transform.position.y, 0);
        }
    }

    void CharacterAttack()
    {
        if (_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, new Vector3(transform.position.x, transform.position.y + 1.03f, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + 1.03f, 0), Quaternion.identity);
        }
        _nextFireTime = Time.time + _fireRate;
    }
    
    public void Damage()
    {
        if(_isShieldActive == true)
        {
            _isShieldActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }
        else
        {
            _playerHealth -= 1;
            UpdateLives(_playerHealth);

        }

        if (_playerHealth < 1) {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
            
        }
        Debug.Log(_playerHealth);
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(StartTripleShotCoroutine());
    }

    IEnumerator StartTripleShotCoroutine()
    {
        yield return new WaitForSeconds(5);
        _isTripleShotActive = false;
    }

    public void SpeedActive()
    {
        _isSpeedActive = true;
        StartCoroutine(StartSpeedPowerupCoroutine());
        
    }
    IEnumerator StartSpeedPowerupCoroutine()
    {
        yield return new WaitForSeconds(5);
        _isSpeedActive = false;
    }
    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

    public void UpdateLives(int currentLives)
    {
        _uiManager.UpdateLives(currentLives);
    }

}
