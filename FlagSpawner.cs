using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlagSpawner : MonoBehaviour {

    public Color[] colors;
    private GameObject[] FlagsInScene;
    private RaycastHit hit;
    public static int Score;

    [SerializeField]
    private Text txt;
    private bool faster;
    private float timer;

    [SerializeField]
    private Scrollbar bar;

    private bool allOnOrOff;
    public AudioClip flagSound;


    void Awake() {

        FlagsInScene = GameObject.FindGameObjectsWithTag("Flag");

    }

    void PlaySound(int clip) {

        GetComponent<AudioSource>().clip = flagSound;
        GetComponent<AudioSource>().Play();
    }

    void Start() {

        allOnOrOff = false;

        Score = 0;
        timer = 60;
        faster = false;


        for (int i = 0; i <= FlagsInScene.Length - 1; i++) {
            FlagsInScene[i].GetComponent<Image>().color = colors[i];
            FlagsInScene[i].GetComponent<Animator>().SetBool("FadeIn", true);
        }

        InvokeRepeating("Flags", 0, 0.3f);
        
        

    }

    void CheckFlags() {

        if (YesNoButtons.YesOrNo == 1) {

            foreach (GameObject go in FlagsInScene) {

                if(go.transform.GetComponent<Animator>().GetBool("FadeIn") == true) {
                    return;
                 }
            }
                 allOnOrOff = true;

        }

        if (YesNoButtons.YesOrNo == 0) {

            foreach (GameObject go in FlagsInScene) {

                if (go.transform.GetComponent<Animator>().GetBool("FadeOut") == true) {
                    return;
                }
            }

            allOnOrOff = true;

        }
    }



    void Flags() {

        int randomInt = Random.Range(0, FlagsInScene.Length);

        if (timer <= 0) {
            return;
        } else {

            if (YesNoButtons.YesOrNo == 1) {

                FlagsInScene[randomInt].GetComponent<Animator>().SetBool("FadeOut", true);
                FlagsInScene[randomInt].GetComponent<Animator>().SetBool("FadeIn", false);

            } else if (YesNoButtons.YesOrNo == 0) {

                FlagsInScene[randomInt].GetComponent<Animator>().SetBool("FadeOut", false);
                FlagsInScene[randomInt].GetComponent<Animator>().SetBool("FadeIn", true);

            }
        }

    }

    void Update() {

        bar.size = timer / 60;

        if (YesNoButtons.gameStarted) {
            timer -= Time.deltaTime;
            InvokeRepeating("CheckFlags", 15, 1.5f);
        }

        if (allOnOrOff) {
            YesNoButtons.gameOver = true;
            allOnOrOff = false;
            CancelInvoke();
        }


        if (timer <= 0f) {

            timer = 60;
            YesNoButtons.activateEnd = true;

            CancelInvoke();

        }

     

        if (!YesNoButtons.startGame) {
            return;
        }
      
        txt.text = "" + Score;

        if (timer <= 30 && !faster) {
            faster = true;
            CancelInvoke("Flags");
            InvokeRepeating("Flags", 0, 0.18f);

        }

        if (Input.GetMouseButtonDown(0)) {

            Ray toMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(toMouse, out hit, 100.0f)) {

                //if the box is the top

                if (hit.transform.tag == "Flag") {

                    if (YesNoButtons.YesOrNo == 1) {

                        if (hit.transform.GetComponent<Animator>().GetBool("FadeOut")) {
                            Score++;
                            hit.transform.GetComponentsInChildren<Animator>()[1].SetTrigger("anim");
                            PlaySound(0);
                        }
                        hit.transform.GetComponent<Animator>().SetBool("FadeIn", true);
                        hit.transform.GetComponent<Animator>().SetBool("FadeOut", false);

                    } else if (YesNoButtons.YesOrNo == 0) {
                        //NO CLICKED

                        if (hit.transform.GetComponent<Animator>().GetBool("FadeIn")) {
                            Score++;
                            hit.transform.GetComponentsInChildren<Animator>()[1].SetTrigger("anim");
                            PlaySound(0);
                        }
                        //set opacity to 10
                        hit.transform.GetComponent<Animator>().SetBool("FadeIn", false);
                        hit.transform.GetComponent<Animator>().SetBool("FadeOut", true);
                        //set color to white lerp
                    }
                }
            }
        }
    }
}
