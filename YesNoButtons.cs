using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class YesNoButtons : MonoBehaviour {

    public static int YesOrNo;
    public static bool startGame;
    private GameObject[] FlagsArrayHolders;
    private GameObject[] FlagsArray;
    public GameObject TimerBar;
    public static bool gameOver;
    public static bool activateEnd;
    public static bool gameStarted;

    [SerializeField]
    private GameObject StartPanel, TutorialScreenFor, TutorialScreenAgainst,Ropes, NoButt, ScoreUI, WinScreen, LostScreen;

    public Text endScore;

    public AudioClip buttonSound;

    void PlaySound(int clip) {

        GetComponent<AudioSource>().clip = buttonSound;
        GetComponent<AudioSource>().Play();
    }

   

void Update() {

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

        endScore.text = "" + FlagSpawner.Score;

        if (gameOver) {

            gameStarted = false;
            gameOver = false;

            TimerBar.SetActive(false);
            ScoreUI.SetActive(false);

            foreach(GameObject go in FlagsArrayHolders) {
                go.SetActive(false);
            }
            foreach (GameObject go in FlagsArray) {
                go.SetActive(false);
            }
            Ropes.SetActive(false);

            LostScreen.SetActive(true);


        }

        if (activateEnd) {

            gameStarted = false;
            activateEnd = false;

            foreach (GameObject go in FlagsArrayHolders) {
                go.SetActive(false);
            }
            foreach (GameObject go in FlagsArray) {
                go.SetActive(false);
            }
            Ropes.SetActive(false);

            WinScreen.SetActive(true);

            ScoreUI.SetActive(false);
            TimerBar.SetActive(false);

           StartCoroutine(Camera.main.GetComponent<DataBase>().Post());


        }


    }

    void Start() {

        TutorialScreenAgainst.SetActive(false);
        TutorialScreenFor.SetActive(false);
        WinScreen.SetActive(false);

        activateEnd = false;
        gameStarted = false;
        startGame = false;

        StartPanel.SetActive(true);
        Ropes.SetActive(false);


        FlagsArrayHolders = GameObject.FindGameObjectsWithTag("Holder");
        FlagsArray = GameObject.FindGameObjectsWithTag("Flag");
        foreach (GameObject go in FlagsArrayHolders) {
            go.GetComponent<Image>().enabled = false;
        }
        foreach (GameObject go in FlagsArray) {
            go.GetComponent<Image>().enabled = false;
            go.GetComponent<BoxCollider>().enabled = true;
        }

        ScoreUI.SetActive(false);
        TimerBar.SetActive(false);

    }

    public void Restart() {

        PlaySound(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    void DeactivateButtons() {

        PlaySound(0);

        StartPanel.SetActive(false);
        NoButt.SetActive(false);

        foreach (GameObject go in FlagsArrayHolders) {
            go.GetComponent<Image>().enabled = true;
        }
        foreach (GameObject go in FlagsArray) {
            go.GetComponent<Image>().enabled = true;
            go.GetComponent<BoxCollider>().enabled = true;
        }

        Ropes.SetActive(true);

        gameStarted = true;

        TimerBar.SetActive(true);
        ScoreUI.SetActive(true);

    }

    public void onContinueForClick() {

        PlaySound(0);

        YesOrNo = 1;
        DeactivateButtons();
        startGame = true;
        TutorialScreenFor.SetActive(false);
    }

    public void OnYesClick() {

        PlaySound(0);

        StartPanel.SetActive(false);
        TutorialScreenFor.SetActive(true);
       
    }

    public void onContinueAgainstClick() {

        PlaySound(0);

        YesOrNo = 0;
        DeactivateButtons();
        startGame = true;
        TutorialScreenAgainst.SetActive(false);
    }


    public void OnNoClick() {
        PlaySound(0);

        StartPanel.SetActive(false);
        TutorialScreenAgainst.SetActive(true);

    }
}
