using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health = 10;
    [SerializeField] int healthDecrease = 1;
    [SerializeField] Slider sliderHealth;
    [SerializeField] AudioClip auchSound;

    private void OnTriggerEnter(Collider other)
    {
        health = health - healthDecrease;
        sliderHealth.value = health;
        GetComponent<AudioSource>().PlayOneShot(auchSound);
    }

}
