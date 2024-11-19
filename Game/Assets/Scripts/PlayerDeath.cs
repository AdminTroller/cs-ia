using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] BoxCollider2D playerCol;
    [SerializeField] BoxCollider2D enemyCol;
    [SerializeField] GameObject deathUI;
    [SerializeField] GameObject jumpscare;
    [SerializeField] AudioSource jumpscareSound;

    bool dead = false;
    float deathTimer = 0;

    void Update() {
        if (playerCol.IsTouching(enemyCol)) dead = true;
        if (dead) Death();
    }

    void Death() {
        if (deathTimer == 0) {
            mixer.SetFloat("SFXVolume", -80);
            Pathfinding.state = 0;
            deathUI.SetActive(true);
            jumpscareSound.Play();
        }
        deathTimer += Time.deltaTime;
    }
}
