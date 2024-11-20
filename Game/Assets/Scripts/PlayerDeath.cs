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

    float zoomSpeed = 5f;

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

        if (jumpscare.transform.localScale.x < 1) {
            jumpscare.transform.localScale += new Vector3(zoomSpeed*Time.deltaTime,zoomSpeed*Time.deltaTime);
            jumpscare.transform.eulerAngles = new Vector3(0,0,Random.Range(-10f,10f));
        }
        deathTimer += Time.deltaTime;
    }
}
