using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private Text _score;
    [SerializeField]
    private int _scoreIndex = 0;
    [SerializeField]
    private Sprite[] _livesImages;
    [SerializeField]
    private Image _livesRefImage;
    [SerializeField]
    private GameObject _GameOverObject;
    [SerializeField]
    private GameObject _RestartText;
    private Game_Manager GM;


    // Start is called before the first frame update
    void Start()
    {
        _score.text = "Score:" + _scoreIndex;
        GM = GameObject.Find("GameManager").GetComponent<Game_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void ScoreAdd()
    {
        _scoreIndex += 10;
        _score.text = "Score:" + _scoreIndex;
    }

    public void LivesScore(int _lives)
    {
        _livesRefImage.sprite = _livesImages[_lives];        
    }

    public void GameOver()
    {
        _GameOverObject.SetActive(true);
        StartCoroutine(Gameoverflicker());
        _RestartText.gameObject.SetActive(true);
        GM.levelRestart();

    }

    IEnumerator Gameoverflicker()
    {
        while (true)
        { 
            yield return new WaitForSeconds(0.5f); 
            _GameOverObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            _GameOverObject.SetActive(true);
        }
           
    }
}
