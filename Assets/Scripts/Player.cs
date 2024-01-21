using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public bool isPlayerOne = false;
    public bool isPlayerTwo = false;
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
    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private GameObject _leftEngine;
    private int _score;
    private UIManager _uiManager;
    [SerializeField]
    private AudioSource _laserShotAudio;
    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Script attached to: " + gameObject.name);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        

        if (_gameManager.isCoopMode == false)
        {
            transform.position = new Vector3(0, -1, 0);
        }


        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is null");
        }
        if (_uiManager == null)
        {
            Debug.LogError("UI Manager is null");

        }
        if (_gameManager == null)
        {
            Debug.LogError("Game Manager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerOne == true)
        {
            MovementPlayer1();
            if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFireTime && isPlayerOne == true)
            {
                _laserShotAudio.Play();
                Attack();
            }

        }
        if (isPlayerTwo == true)
        {
            MovementPlayer2();

            if (Input.GetKeyDown(KeyCode.RightShift) && Time.time > _nextFireTime && isPlayerTwo == true)
            {
                _laserShotAudio.Play();
                Attack();
            }
        }

    }

    void MovementPlayer1()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Player movement
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        {
            
            if (_isSpeedActive == true)
            {
                transform.Translate(direction * Time.deltaTime * _playerSpeed * 1.5f);


            }
            else
            {
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
    }
        void MovementPlayer2()
        {

            KeyCode up = KeyCode.I;
            KeyCode right = KeyCode.L;
            KeyCode down = KeyCode.K;
            KeyCode left = KeyCode.J;


            if (_isSpeedActive == true)
            {
                if (Input.GetKey(up))
                {
                    transform.Translate(Vector3.up * _playerSpeed * 1.5f * Time.deltaTime);
                }
                if (Input.GetKey(down))
                {
                    transform.Translate(Vector3.down * _playerSpeed * 1.5f * Time.deltaTime);
                }
                if (Input.GetKey(left))
                {
                    transform.Translate(Vector3.left * _playerSpeed * 1.5f * Time.deltaTime);

                }
                if (Input.GetKey(right))
                {
                    transform.Translate(Vector3.right * _playerSpeed * 1.5f * Time.deltaTime);
                }

            }
            else
            {
                if (Input.GetKey(up))
                {
                    transform.Translate(Vector3.up * _playerSpeed * Time.deltaTime);
                }
                if (Input.GetKey(down))
                {
                    transform.Translate(Vector3.down * _playerSpeed * Time.deltaTime);
                }
                if (Input.GetKey(left))
                {
                    transform.Translate(Vector3.left * _playerSpeed * Time.deltaTime);

                }
                if (Input.GetKey(right))
                {
                    transform.Translate(Vector3.right * _playerSpeed * Time.deltaTime);
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

        }

        void Attack()
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
            if (_isShieldActive == true)
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
            if (_playerHealth == 2)
            {
                _rightEngine.SetActive(true);
            }
            if (_playerHealth == 1)
            {
                _leftEngine.SetActive(true);
            }
            if (_playerHealth < 1)
            {
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
       public  void ShieldActive()
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

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Enemy Laser")
            {
                Destroy(collision.gameObject);
                Damage();
            }
        }
    
}
