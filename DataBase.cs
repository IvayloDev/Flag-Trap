using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Text;
using System.Collections.Generic;
using LitJson;

public class DataBase : MonoBehaviour {

    [SerializeField]
    private Text forText, AgainstText;

    private string serverURL = "...";
    private string token = "...";
    private WWWForm form;

    [Serializable]
    public class Json {
        public int type;
        public int score;
    }

    [Serializable]
    public class MyData {

        public int countType0 { get; set; }
        public int scoreType0 { get; set; }
        public int countType1 { get; set; }
        public int scoreType1 { get; set; }
        public int highscoretype0 { get; set; }
        public int highscoretype1 { get; set; }

    }


    [Serializable]
    public class myObject {

        public object[] errors { get; set; }
        public MyData data { get; set; }
        public int httpStatusCode { get; set; }

    }



    void Start() {


            Dictionary<string, string> dict = new Dictionary<string, string>();

            dict.Add("Content-Type", "application/json");
            dict.Add("Token", token);

            WWW www = new WWW(serverURL, null, dict);

            StartCoroutine(WaitForRequest(www));

        }


        IEnumerator WaitForRequest(WWW www) {


            yield return www;

        //myObj = www.text;
        myObject myObj = new myObject();

        myObj = (myObject)JsonMapper.ToObject<myObject>(www.text);

        if (myObj != null) {

            forText.text = ("" + myObj.data.scoreType1);
            AgainstText.text = ("" + myObj.data.scoreType0);
        } else {
            forText.text = "няма връзка";
            AgainstText.text = "няма връзка";

        }

    }




    Json jsonPOST = new Json();

    public IEnumerator Post () {

        Dictionary<string, string> dict = new Dictionary<string, string>();

        dict.Add("Content-Type", "application/json");

        dict.Add("token", token);

        jsonPOST.type = YesNoButtons.YesOrNo;
        jsonPOST.score = FlagSpawner.Score;

        string jSonString = JsonUtility.ToJson(jsonPOST);

        byte[] rawData = Encoding.ASCII.GetBytes(jSonString.ToCharArray());

        WWW www = new WWW(serverURL, rawData,dict);

        yield return www;

    }
	
}
