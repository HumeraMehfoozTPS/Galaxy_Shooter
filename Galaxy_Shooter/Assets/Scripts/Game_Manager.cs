using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{

    [SerializeField]
    private bool _isGameOver;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r") && _isGameOver == true)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void levelRestart()
    {
        _isGameOver = true;

    }
}
