using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float secondsBetweenSpawns = 2f;
    [SerializeField] EnemyMovement enemyPrefab;
    [SerializeField] Transform enemyParentTransform;
    [SerializeField] Text spawnedEnemies;
    [SerializeField] AudioClip spawnedEnemySFX;
    int score = 0;
    
    private void Awake()
    {
        spawnedEnemies.text = score.ToString();
    }

    void Start()
    {
        StartCoroutine(RepeatedlySpawnEnemies());

    }

    IEnumerator RepeatedlySpawnEnemies()
    {
        while (true)
        {
            var newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            GetComponent<AudioSource>().PlayOneShot(spawnedEnemySFX);
            score += 1;
            spawnedEnemies.text = score.ToString();
            newEnemy.transform.parent = enemyParentTransform;
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }

}
