using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public or private identify
    //data type(int, floats, booleans, strings)
    //every variable has a NAME
    //option value asigned
    [SerializeField]
    private GameObject _Explosion;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShootPrefab;
    [SerializeField]
    private GameObject _shieldGameObject;

    [SerializeField]
    private GameObject[] _engines;


    [SerializeField]
    private float _fireRate = 0.25f;
    private float _canFire = 0.0f;
    //Firerate is 0.25f
    // canfire -- has the amount float time between fired passed?
    //Time.Time
         
    [SerializeField]
    private float _speed = 15.0f;

    public int lifes = 3;
    
    //triple shots
    public bool canTripleShot = false;
    //speed boost
    public bool canSpeedBoost = false;

    public bool isShieldEnabled = false;

    private int hitCount = 0; 

    private Ui_Manager _ui_Manager;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    private AudioSource _audiosource;
    // Start is called before the first frame update
    private void Start()
    {
        //posicion actual = nueva posicion
        transform.position = new Vector3(0, 0, 0);
        _ui_Manager = GameObject.Find("Canvas").GetComponent<Ui_Manager>();
        if (_ui_Manager != null)
        {
            _ui_Manager.UpdateLives(lifes);
        }
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if(_spawnManager != null){
            _spawnManager.StartSpawnRoutines();
        }
        _audiosource = GetComponent<AudioSource>();
        hitCount = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        Movement();

        // if space key pressed
        //spawn laser at player position
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            Shoot();               
         }

    }
    private void Shoot()
    {
        //spawn my laser
        
        if (Time.time > _canFire)
        {
            _audiosource.Play();
            if (canTripleShot == true)
            {
                //triple Shoot
                Instantiate(_tripleShootPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.9f, 0), Quaternion.identity);
                
            }
            _canFire = Time.time + _fireRate;
        }

    }

    private void Movement()
    {
        //Horizontal
        float horizontalInput = Input.GetAxis("Horizontal");
        //vertical
        float verticalInput = Input.GetAxis("Vertical");

        //is speed boost enabled
        // move 1.5x the normal speed 
        //else normal speed
        if(canSpeedBoost == true)
        {
            transform.Translate(Vector3.right * _speed * 2f * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * 2f *  verticalInput * Time.deltaTime);
        }
        else { 
        transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
        }

        //if player on the "Y" is greater than 0
        //set player position 0

        if (transform.position.y > 4)
        {
            transform.position = new Vector3(transform.position.x, 4, 0);
        }
        else if (transform.position.y < -4.0f)
        {
            transform.position = new Vector3(transform.position.x, -4.0f, 0);
        }

        if (transform.position.x > 9.0f)
        {
            transform.position = new Vector3(-9.0f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.0f)
        {
            transform.position = new Vector3(9.0f, transform.position.y, 0);
        }

    }
    
    public void Damage()
    {
        

        if (isShieldEnabled == true)
        {
            isShieldEnabled = false;
            _shieldGameObject.SetActive(false);
            return;
        }
        hitCount++;
        if (hitCount == 1)
        {
            _engines[0].SetActive(true);
        }
        else if (hitCount == 2)
        {
            _engines[1].SetActive(true);
        }

        lifes--;
        _ui_Manager.UpdateLives(lifes);

        if(lifes < 1)
        {
            _gameManager.gameOver = true;
            _ui_Manager.ShowTitleScreen();
            Instantiate(_Explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        
        
        }
    }
    //Triple Shots
    public void TripleShowPowerupOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    

     public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }
    //Speed Boost
    
    
    public void SpeedBoostPowerupOn()
    {
        canSpeedBoost = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }
    
    public IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canSpeedBoost = false;
    }
    //Shields
    public void ShieldPowerupOn()
    {
        isShieldEnabled = true;
        _shieldGameObject.SetActive(true);
    }


}


