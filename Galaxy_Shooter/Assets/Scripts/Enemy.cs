using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    private UI_Manager scorer;
    private Animator my_animator;
    [SerializeField]
    private AudioClip my_audioClip;
    private AudioSource my_audioSource;
    void Start()
    {
        transform.position = new Vector3(Random.Range(-9f, 9f), 7.0f, 0);
        scorer =  GameObject.Find("Canvas").GetComponent<UI_Manager>();
        my_animator = GetComponent<Animator>();
        if(my_animator == null)
        {
            Debug.LogError("No Animator found");
        }
        my_audioSource = GetComponent<AudioSource>();   
        if(my_audioSource == null)
        {
            Debug.LogError("No Audio for Collision");
        }
        else
        {
            my_audioSource.clip = my_audioClip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y < -6f)
        {
            transform.position = new Vector3(Random.Range(-9f,9f), 8f, 0);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit"+other.transform.name);
        if(other.tag == "Player" )
        {
           Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }
            my_audioSource.Play();
            my_animator.SetTrigger("On_EnemyDeath");
            Destroy(gameObject,1.2f);
            _speed = 0;


        }
        else if(other.tag == "Laser")
        {
            
            if(scorer != null)
            {
                scorer.ScoreAdd();
            }
            my_audioSource.Play();
            my_animator.SetTrigger("On_EnemyDeath");
            Destroy(other.gameObject);
            Destroy(gameObject,1.2f);
            _speed = 0;
        }
        
    }
}
