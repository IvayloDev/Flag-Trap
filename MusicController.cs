using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MusicController : MonoBehaviour {


    private int MusicInt;

    public GameObject musicButt;
    public Sprite MusicOnSprite;
    public Sprite MusicOffSprite;
    public AudioClip sound;

    void PlaySound(int clip) {

        GetComponent<AudioSource>().clip = sound;
        GetComponent<AudioSource>().Play();
    }

    void Start() {
        MusicInt = 0;
        MusicInt = PlayerPrefs.GetInt("MusicInt", 0);
    }

    void OnDestroy() {
        PlayerPrefs.SetInt("MusicInt", MusicInt);
    }

    public void OnSoundClick() {
        PlaySound(0);
        if (MusicInt == 0) {
            MusicInt = 1;
        } else {
            MusicInt = 0;
        }
    }

        void Update() {
            if (MusicInt == 1) {
                musicButt.GetComponent<Image>().sprite = MusicOffSprite;
                AudioListener.volume = 0;
            } else if (MusicInt == 0) {
                musicButt.GetComponent<Image>().sprite = MusicOnSprite;
                AudioListener.volume = 1;
            }

        }

    }
