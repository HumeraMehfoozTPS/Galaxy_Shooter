using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_behaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
