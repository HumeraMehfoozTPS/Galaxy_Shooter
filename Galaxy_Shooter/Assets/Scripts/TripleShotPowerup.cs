using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TripleShotPowerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int _PowerUpID;
  
    // Start is called before the first frame update
    void Start()
    {
        transform.position= new Vector3(Random.Range(-9f,9f),5f,0);
      
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y<-5)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D bang)
    {
        if(bang.tag == "Player")
        {
            Player player = bang.transform.GetComponent<Player>();
            if (player != null)
            {
                switch (_PowerUpID)
                {
                    case 0:
                        player.OntripleShotActive();
                        break;
                    case 1:
                        Debug.Log("You get a Speed powerUp");
                        player.SpeedboostActive();
                        break;
                    case 2:
                        Debug.Log("You get a shield");
                        player.ShieldActive();
                        break;
                    default:
                        Debug.Log("out of bound value");
                        break;
                }
               
            }
           
            Destroy(gameObject) ;
        }
    }
}
