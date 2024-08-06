using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _canfire= -1f;
    [SerializeField]
    private float _nextfire = 0.3f;
    [SerializeField]
    private int _lives = 3;
    private Boolean _isTripleshotEnabled=false;
    private Boolean _isSpeedBoostActive = false;
    private Boolean _isShieldActive=false;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _ShieldActive;
    private SpawnManager _spawnManager;
    private UI_Manager livesUpdate;
    [SerializeField]
    private GameObject _leftEngine;
    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private AudioClip _audioClip;//for laser fire
    [SerializeField]
    private AudioClip _audioClip2P; //for powerUps
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0,0,0);
        livesUpdate = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _audioSource = GetComponent<AudioSource>(); 
        if( _audioSource == null )
        {
            Debug.LogError("There is no sound in Player Audio source");
        }
        else
        {
            _audioSource.clip = _audioClip;
        }
        if(_spawnManager == null )
        {
            Debug.LogError("SpawnManager is not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        calculatemovement();

        if ((Input.GetKeyDown("space") || CrossPlatformInputManager.GetButtonDown("Fire")) && Time.time > _canfire)
        {
            Fire();
        }
        
    }
        void calculatemovement()
    {
        float horizontalUpdate = CrossPlatformInputManager.GetAxis("Horizontal");//Input.GetAxis("Horizontal");
        float verticalUpdate = CrossPlatformInputManager.GetAxis("Vertical");//Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalUpdate, verticalUpdate, 0);
        if (_isSpeedBoostActive == true)
        {
            transform.Translate(direction*_speed*_speed*Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
        if (transform.position.x >= 11f)
        {
            transform.position = new Vector3(-10f, transform.position.y, 0);
        }
        else if (transform.position.x < -10f)
        {
            transform.position = new Vector3(10f, transform.position.y, 0);
        }
        if (transform.position.y > 0f)
        {
            transform.position = new Vector3(transform.position.x, -5f, 0);
        }
        else if (transform.position.y < -5f)
        {
            transform.position = new Vector3(transform.position.x, 0f, 0);
        }
    }

    void Fire()
    {
        _canfire = Time.time + _nextfire;
        if (_isTripleshotEnabled == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
        }

        _audioSource.Play();
    }

    public void Damage()
    {
        if (_isShieldActive == true)
        {
            _ShieldActive.SetActive(false);
            _isShieldActive = false;
        }
        else {
            _lives -= 1;
            livesUpdate.LivesScore(_lives);
            if( _lives > 1 )
            {
                _leftEngine.SetActive(true);
            }
            else if( _lives > 0 )
            {
                _rightEngine.SetActive(true);
            }
            else if (_lives < 1)
            {
                _spawnManager.onPlayerDeath();
                Destroy(this.gameObject);
                livesUpdate.GameOver();
              
            }
        }
    }

    public void OntripleShotActive()
    {
        PowerUpSound();
        _isTripleshotEnabled = true;
        if (_isTripleshotEnabled == true) 
        {
            StartCoroutine(TripleShotSpawning());
        }
    }

    IEnumerator TripleShotSpawning()
    {
        yield return new WaitForSeconds(10f);
        _ShieldActive.SetActive(false);
        _isTripleshotEnabled=false;
        _isSpeedBoostActive=false;
        _isShieldActive=false;
    }

    public void SpeedboostActive()
    {
        PowerUpSound();
        _isSpeedBoostActive = true;
        if(_isSpeedBoostActive == true)
        {
            StartCoroutine(TripleShotSpawning());
        }    
    }

    public void ShieldActive()
    {
        PowerUpSound();
        _isShieldActive=true;
       
        if (_isShieldActive==true)
        {
            _ShieldActive.SetActive(true);
            StartCoroutine(TripleShotSpawning());
        }
    }

    void PowerUpSound()
    {
       AudioSource.PlayClipAtPoint(_audioClip2P,transform.position);

    }
}
