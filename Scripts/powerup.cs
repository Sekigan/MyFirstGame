using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int powerupID; // 0 = triple shot ; 1= speed boost; 2= shields
    [SerializeField]
    private AudioClip audioClip;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with: " + other.name);
        if (other.tag == "Player")
        {
            //access the player
            Player player = other.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position, 1f);
            if (player != null)
            {
                //enable triple shot
                if(powerupID == 0) { 

                player.TripleShowPowerupOn();
                }
                else if(powerupID == 1)
                {
                    player.SpeedBoostPowerupOn();
                    //enable speed boost
                }
                else if (powerupID == 2) {
                    player.ShieldPowerupOn();
                }

            }



            // destroy our selves
            
            Destroy(this.gameObject);
        }
        

    }
}
