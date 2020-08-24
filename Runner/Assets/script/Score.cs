using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private float score = 0.0f;
    private int Difficultlevel = 1;
    private int maxDifficultlevel = 10;
    private int scoreTonextlevel = 10;
    private bool isdeath = false;

    public Deathmenu deathmenu;
    public Text scoretext;
    void Update()
    {
        if (isdeath)
            return;
        if(score > scoreTonextlevel)
        {
            LevelUp();
        }
        score += Time.deltaTime * Difficultlevel;
        scoretext.text = ((int)score).ToString();
    }

    private void LevelUp()
    {
        if (Difficultlevel == maxDifficultlevel)
            return;

        scoreTonextlevel *= 2;
        Difficultlevel++;
        GetComponent<PlayerMotor>().setspeed(Difficultlevel);
    }
   
    public void Ondeath()
    {
        isdeath = true;
        if(PlayerPrefs.GetFloat("HighScore") < score)
            PlayerPrefs.SetFloat("HighScore", score);

        deathmenu.Togglemenu(score);
    }
}
