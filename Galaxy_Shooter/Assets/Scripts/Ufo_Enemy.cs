using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ufo_Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private GameObject _UfoExplosion;
    private SpawnManager spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if(spawnManager == null )
        {
            Debug.LogError("you have no Spawn_Manager");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward*_speed*Time.deltaTime);

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Laser")
        {
            Instantiate(_UfoExplosion,transform.position,Quaternion.identity);
            Destroy(collision.gameObject);
            spawnManager.StartSpawning();
            Destroy(this.gameObject);
          
        }
    }
}
