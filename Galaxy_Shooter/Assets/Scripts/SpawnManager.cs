using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private bool _enemySpawning = false;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _allPowerUps;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void StartSpawning()
    {
        StartCoroutine(EnemySpawning());
        StartCoroutine(PowerUpSpawning());
    }

    IEnumerator EnemySpawning()
    {
        yield return new WaitForSeconds(3.0f);
        while (_enemySpawning == false) 
        {
            Vector3 onPos = new Vector3(Random.Range(-9f, 9f), 7f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab,onPos, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator PowerUpSpawning()
    {
        yield return new WaitForSeconds(3.0f);
        while (_enemySpawning == false)
        {
            Vector3 onPos = new Vector3(Random.Range(-9f, 9f), 7f, 0);
            Instantiate(_allPowerUps[Random.Range(0,3)], onPos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3f, 7f));
        }
    }
    public void onPlayerDeath()
    {
        _enemySpawning=true;    
    }

}
