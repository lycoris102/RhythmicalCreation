using UnityEngine;
﻿using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

    private int score;
    public Text scoreText;

    public void AddScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
