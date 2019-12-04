using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    
    private float _speed = 8.0f;
    [SerializeField]
    private GameObject explosion;

    private Ui_Manager _ui_Manager;
    private GameManager _gameManager;
    [SerializeField]
    private AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
        _ui_Manager = GameObject.Find("Canvas").GetComponent<Ui_Manager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        //move down
        transform.Translate(Vector3.down * _speed * Time.deltaTime); 
        if(transform.position.y < -7f)
        {
            float randomX = Random.Range(-8f,8f);
            transform.position = new Vector3(randomX, 7f, 0f);
        }
        //when off the screen on the bottom 
        //respawn back on top with a new x position betwen the bounds of the screen

        if (_gameManager.gameOver == true) {      
            Destroy(this.gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {
            
            //access the player
            Player player = other.GetComponent<Player>();
            
            if (player != null)
            {
                
                player.Damage();

               
            }
            Instantiate(explosion, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);
            
        }
        else if (other.tag == "Laser")
        {
            
            if (other.transform.parent != null)
            {
              Destroy(other.transform.parent.gameObject);   
            }
            Instantiate(explosion, transform.position, Quaternion.identity);
            _ui_Manager.UpdateScore();
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);
            Destroy(other.gameObject);
            



        }


    }
}
