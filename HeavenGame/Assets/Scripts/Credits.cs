using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct Credit
{
    public string position;
    public string name;
}

public class Credits : MonoBehaviour {

    public Font font;
    public List<Credit> credits;
    public Text creditText;

	// Use this for initialization
	void Start () {
        StartCoroutine(RollCredits());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator RollCredits()
    {
        yield return new WaitForSeconds(1.0f);
        foreach(Credit credit in credits)
        {
            creditText.text = credit.position + "\n\n" + credit.name;
            yield return new WaitForSeconds(2.0f);
        }

        creditText.text = "Music\n\nJulia fuit\n\nBy Gustavo Santaolalla";
        yield return new WaitForSeconds(2.0f);
        creditText.text = "Music\n\nDark Ambience\n\nBy Patrick Lieberkind";
        yield return new WaitForSeconds(2.0f);
        creditText.text = "A\n\nPixel Dash Studios\n\nProduction";
        yield return new WaitForSeconds(2.0f);
        creditText.font = font;
        creditText.fontSize = 120;
        creditText.text = "HEAVEN";
        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene("Menu");
    }
}
