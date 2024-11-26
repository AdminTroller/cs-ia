using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] GameObject deathUI;
    [SerializeField] GameObject jumpscare;
    [SerializeField] AudioSource jumpscareSound;
    [SerializeField] AudioSource staticSound;
    [SerializeField] SpriteRenderer staticBg;
    [SerializeField] Sprite[] staticSprites;

    public static bool dead = false;
    public static int enemyType;
    float deathTimer = 0;
    const float deathTimerEnd = 1f;
    int staticSprite = 0;

    float zoomSpeed = 5f;

    void Update() {
        if (dead) Death();
    }

    void Death() {
        if (deathTimer == 0) {
            mixer.SetFloat("SFXVolume", -80);
            deathUI.SetActive(true);
            jumpscareSound.Play();

            jumpscare.transform.localPosition = new Vector2(0,-8);
            jumpscare.transform.eulerAngles = Vector3.zero;
            jumpscare.transform.localScale = new Vector3(0.05f,0.05f,1);
        }

        if (deathTimer > deathTimerEnd) {
            jumpscareSound.Stop();
            staticBg.enabled = true;
            
            staticSprite = Mathf.FloorToInt(deathTimer*15) % 5;
            staticBg.sprite = staticSprites[staticSprite];

            if (!staticSound.isPlaying) staticSound.Play();
        }
        else {
            if (jumpscare.transform.localPosition.y < -1.25f) {
                jumpscare.transform.localPosition += new Vector3(0,30f * Time.deltaTime);
            }
            if (jumpscare.transform.localScale.x < 1) {
                jumpscare.transform.localScale += new Vector3(zoomSpeed*Time.deltaTime,zoomSpeed*Time.deltaTime);
                zoomSpeed -= 12f * Time.deltaTime;
            }
            jumpscare.transform.eulerAngles = new Vector3(0,0,Random.Range(-5f,5f));
        }
        

        deathTimer += Time.deltaTime;
    }
}
