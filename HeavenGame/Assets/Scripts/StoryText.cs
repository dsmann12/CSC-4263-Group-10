using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryText : MonoBehaviour {
    Text introText;
    public List<string> strList;
    public float letterRate;
    public float sentenceRate;
    public uint lineSize;
    public bool loadingScene = false;
    public string nextScene;
    
    // Use this for initialization
    void Start () {
        introText = GetComponent<Text>();
        StartCoroutine(DisplayText());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator DisplayText()
    {
        // for every line in strList
        for (int i = 0; i < strList.Capacity; ++i)
        {
            if (i % lineSize == 0)
            {
                introText.text = "";
            }
            
            char[] str = strList[i].ToCharArray();
            for (int c = 0; c < str.Length; ++c)
            {
                introText.text += str[c];
                yield return new WaitForSeconds(letterRate);
            }
            introText.text += "\n\n";
            yield return new WaitForSeconds(sentenceRate);
        }
        yield return new WaitForSeconds(3.0f);
        if (loadingScene)
        {
            LevelManager lm = GetComponent<LevelManager>();
            lm.LoadLevel(nextScene);
        } else
        {
            GameObject.Find("Story").SetActive(false);
        }
    }
}

