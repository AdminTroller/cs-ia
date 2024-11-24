using UnityEngine;
using TMPro;
using System;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject titleScreen;
    [SerializeField] GameObject warning;
    
    [SerializeField] SpriteRenderer danger;
    [SerializeField] TextMeshProUGUI dangerText;

    [SerializeField] SpriteRenderer headphones;
    [SerializeField] TextMeshProUGUI headphonesText;
    [SerializeField] Sprite headphones1;
    [SerializeField] Sprite headphones2;

    [SerializeField] TextMeshProUGUI continueText;
    const float fadeSpeed = 1f;
    bool inWarning = true;
    float warningTimer = 0f;
    int warningState = 1;
    bool clicked = false;
    bool warningWait = false;

    [SerializeField] SpriteRenderer staticSR;
    [SerializeField] Sprite[] staticSprites;
    float staticTimer = 0f;

    [SerializeField] GameObject UI;
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject levelGrid;

    void Update() {
        if (Input.GetKeyDown(KeyCode.H)) StartNight(1);

        warning.SetActive(inWarning);
        titleScreen.SetActive(!inWarning);

        if (inWarning) Warning();
        else TitleScreen();
    }

    void Warning() {

        warningTimer += Time.deltaTime;

        if (warningWait == true) {
            if (warningTimer >= 0.5f) inWarning = false;
            return;
        }

        danger.enabled = warningState == 1;
        dangerText.enabled = warningState == 1;
        headphones.enabled = warningState == 2;
        headphonesText.enabled = warningState == 2;
        if (Mathf.FloorToInt(warningTimer*1.5f) % 2 == 0) headphones.sprite = headphones1;
        else headphones.sprite = headphones2;

        if (Input.GetMouseButtonDown(0) && headphones.color.a >= 0.5f) {
            clicked = true;
            warningTimer = 0;
        }

        if (clicked) {
            danger.color -= new Color(0,0,0,fadeSpeed*2 * Time.deltaTime);
            dangerText.color -= new Color(0,0,0,fadeSpeed*2 * Time.deltaTime);
            headphones.color -= new Color(0,0,0,fadeSpeed*2 * Time.deltaTime);
            headphonesText.color -= new Color(0,0,0,fadeSpeed*2 * Time.deltaTime);
            continueText.color -= new Color(0,0,0,fadeSpeed*2 * Time.deltaTime);
            if (warningTimer > 0.5f) {
                warningState++;
                warningTimer = 0;
                clicked = false;
                if (warningState == 3) warningWait = true;
            }
        }
        else {
            if (warningTimer > 0.2f && warningTimer < 3f) {
                continueText.enabled = false;
                continueText.color = new Color(0.6f,0.6f,0.6f);
                if (headphones.color.a >= 1) {
                    danger.color = new Color(1,1,1,1);
                    dangerText.color = new Color(1,1,1,1);
                    headphones.color = new Color(1,1,1,1);
                    headphonesText.color = new Color(1,1,1,1);
                }
                else {
                    danger.color += new Color(0,0,0,fadeSpeed * Time.deltaTime);
                    dangerText.color += new Color(0,0,0,fadeSpeed * Time.deltaTime);
                    headphones.color += new Color(0,0,0,fadeSpeed * Time.deltaTime);
                    headphonesText.color += new Color(0,0,0,fadeSpeed * Time.deltaTime);
                }
            }
            else if (warningTimer >= 3f) {
                continueText.enabled = true;
                if (Mathf.FloorToInt(warningTimer) % 2 == 0) {
                    continueText.color = new Color(0.5f,0.5f,0.5f);
                }
                else continueText.color = new Color(0.6f,0.6f,0.6f);
            }
        }
    }

    void TitleScreen() {
        titleScreen.SetActive(true);

        staticTimer += Time.deltaTime*10;
        if (staticTimer >= 5) staticTimer -= 5;
        staticSR.sprite = staticSprites[Mathf.FloorToInt(staticTimer)];
    }

    void StartNight(int night) {
        UI.SetActive(true);
        player.SetActive(true);
        enemy.SetActive(true);
        levelGrid.SetActive(true);
        gameObject.SetActive(false);
    }
}
