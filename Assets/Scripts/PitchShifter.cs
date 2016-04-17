using UnityEngine;
﻿using UnityEngine.UI;
using System.Collections;

public class PitchShifter : MonoBehaviour {

    public Text pitchText;
    public StageCreator stageCreator;
    public AudioSource audioSource;
    private int pitch = 0;

    void Update()
    {
        if (!Music.IsPlaying && stageCreator.isStarted == false)
        {
            if (Input.GetKeyDown("up") && audioSource.pitch <= 1.3f)
            {
                audioSource.pitch += 0.1f;
                pitch++;
                SetText();
            }
            if (Input.GetKeyDown("down") && audioSource.pitch >= 0.7f)
            {
                audioSource.pitch -= 0.1f;
                pitch--;
                SetText();
            }
        }
    }

    void SetText () {
        if (pitch == 0) {
            pitchText.text = "";
        }
        if (pitch > 0) {
            pitchText.text = "+" + pitch.ToString();
        }
        else {
            pitchText.text = pitch.ToString();
        }
    }
}
