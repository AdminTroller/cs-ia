using UnityEngine;
using TMPro;
using System;
using UnityEngine.Audio;
using System.IO;

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

    // warning screen variables
    [SerializeField] TextMeshProUGUI skipText;
    const float fadeSpeed = 1.3f;
    bool inWarning = true;
    float warningTimer = 0f;
    int warningState = 1;
    bool clicked = false;
    bool warningWait = false;

    [SerializeField] SpriteRenderer staticSR;
    [SerializeField] Sprite[] staticSprites;
    float staticTimer = 0f;

    // main menu buttons
    bool hovering = false;
    [SerializeField] TextMeshProUGUI newGameText;
    [SerializeField] TextMeshProUGUI continueText;
    [SerializeField] BoxCollider2D newGameCollider;
    [SerializeField] BoxCollider2D continueCollider;
    [SerializeField] AudioSource hover;
    [SerializeField] AudioSource musicBox;
    [SerializeField] AudioSource staticSound;

    // main menu volume slider
    bool volumeHeld = false;
    [SerializeField] GameObject volume;
    [SerializeField] Sprite[] volumeSprites;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSource generatorSound;

    // save file directory path
    string filePath = "./";
    string fileName = "save.txt";

    // main menu clock display
    DateTime currentTime;
    [SerializeField] GameObject hourHand;
    [SerializeField] GameObject minuteHand;
    [SerializeField] GameObject secondHand;
    [SerializeField] AudioSource tickSound;
    int previousSecond = -1;

    [SerializeField] EnemyManager enemyManager;
    [SerializeField] GameObject UI;
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject levelGrid;

    void Awake() {
        LoadVolume();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.H)) StartNight(1); // debug start night

        warning.SetActive(inWarning);
        titleScreen.SetActive(!inWarning);

        if (inWarning) Warning();
        else TitleScreen();
    }

    void Warning() { // health and safety warnings before game starts

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
            skipText.color -= new Color(0,0,0,fadeSpeed*2 * Time.deltaTime);
            if (warningTimer > 0.4f) {
                warningState++;
                warningTimer = 0;
                clicked = false;
                if (warningState == 3) warningWait = true;
            }
        }
        else {
            if (warningTimer > 0.2f && warningTimer < 3f) {
                skipText.enabled = false;
                skipText.color = new Color(0.6f,0.6f,0.6f);
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
                skipText.enabled = true;
                if (Mathf.FloorToInt(warningTimer) % 2 == 0) {
                    skipText.color = new Color(0.5f,0.5f,0.5f);
                }
                else skipText.color = new Color(0.6f,0.6f,0.6f);
            }
        }
    }

    void TitleScreen() { // creates interactable buttons and music in title screen
        titleScreen.SetActive(true);

        if (!musicBox.isPlaying) musicBox.Play();
        if (!staticSound.isPlaying) staticSound.Play();

        staticTimer += Time.deltaTime*10;
        if (staticTimer >= 5) staticTimer -= 5;
        staticSR.sprite = staticSprites[Mathf.FloorToInt(staticTimer)];

        // gets user's mouse position
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (newGameCollider.OverlapPoint(mousePos)) { // hovering
            if (!hovering) hover.Play();
            hovering = true;
            newGameText.text = "New Game <<";
            if (Input.GetMouseButtonDown(0)) { // click
                StartNight(1);
            }
        }
        else newGameText.text = "New Game";

        if (continueCollider.OverlapPoint(mousePos)) { // hovering
            if (!hovering) hover.Play();
            hovering = true;
            continueText.text = "Continue <<";
            if (Input.GetMouseButtonDown(0)) { // click
                StartNight(1);
            }
        }
        else continueText.text = "Continue";

        if (!newGameCollider.OverlapPoint(mousePos) && !continueCollider.OverlapPoint(mousePos)) hovering = false;

        // volume slider interactable
        if (volume.GetComponent<BoxCollider2D>().OverlapPoint(mousePos)) {
            if (Input.GetMouseButtonDown(0)) { // click
                volumeHeld = true;
            }
        }
        if (volumeHeld) {
            volume.GetComponent<SpriteRenderer>().sprite = volumeSprites[1];
            volume.transform.localPosition = new Vector2(Mathf.Clamp(mousePos.x + 8, (float)-4.8, (float)4.8), 0);
            audioMixer.SetFloat("MasterVolume", 15*volume.transform.localPosition.x/4.4f);
            if (volume.transform.localPosition.x <= -4.75) audioMixer.SetFloat("MasterVolume", -80);
            if (Input.GetMouseButtonUp(0)) {
                volumeHeld = false;
                SaveVolume();
            }
        }
        else {
            volume.GetComponent<SpriteRenderer>().sprite = volumeSprites[0];
        }

        // simulating real-time clock in title screen
        currentTime = DateTime.Now;
        hourHand.transform.eulerAngles = new Vector3(0,0,(currentTime.Hour * -30) + (currentTime.Minute / -2));
        minuteHand.transform.eulerAngles = new Vector3(0,0,currentTime.Minute * -6);
        secondHand.transform.eulerAngles = new Vector3(0,0,currentTime.Second * -6);
        if (previousSecond == -1) previousSecond = currentTime.Second; 
        if (currentTime.Second != previousSecond) {
            previousSecond = currentTime.Second;
            tickSound.Play();
        }
    }

    void SaveVolume() { // save volume slider setting
        string path = Path.Combine(filePath, fileName);
        File.Delete(path);
        using (StreamWriter save = new StreamWriter(path, true)) {
            save.WriteLine(volume.transform.localPosition.x);
        }
    }

    void LoadVolume() { // load volume slider setting
        string path = Path.Combine(filePath, fileName);
        if (File.Exists(path)) {
            using (StreamReader save = new StreamReader(path, true)) {
                float volumePos = float.Parse(save.ReadLine());
                volume.transform.localPosition = new Vector2(volumePos, 0);
                audioMixer.SetFloat("MasterVolume", 15*volume.transform.localPosition.x/4.4f);
                if (volume.transform.localPosition.x <= -4.75) audioMixer.SetFloat("MasterVolume", -80);
            }
        }
    }

    // starts a night/level
    void StartNight(int night) {
        UI.SetActive(true);
        player.SetActive(true);
        enemyManager.StartNight(night);
        levelGrid.SetActive(true);
        generatorSound.Play();
        gameObject.SetActive(false);
    }
}
