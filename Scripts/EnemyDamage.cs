using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] Collider collisionMesh;
    [SerializeField] int hitPoints = 10;
    [SerializeField] ParticleSystem hitParticlePrefab;
    [SerializeField] ParticleSystem deadParticlePrefab;
    [SerializeField] AudioClip enemyHitSFX;
    [SerializeField] AudioClip enemyDeathSFX;

    AudioSource myAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    private void OnParticleCollision(GameObject other)
    {
        print("I'm hit");
        ProcessHit();
        if (hitPoints <= 0)
        {
            var part= Instantiate(deadParticlePrefab, transform.position, Quaternion.identity);
            KillEnemy();
        }
    }

    void ProcessHit()
    {
        hitPoints = hitPoints - 1;
        hitParticlePrefab.Play();
        myAudioSource.PlayOneShot(enemyHitSFX);
        //print("current hitpoints are " + hitPoints);
    }

    void KillEnemy()
    {
        var vfx = Instantiate(deadParticlePrefab, transform.position, Quaternion.identity);
        vfx.Play();
        myAudioSource.PlayOneShot(enemyDeathSFX);
        Destroy(vfx.gameObject, 1);
        Destroy(gameObject, 1);
    }
}
