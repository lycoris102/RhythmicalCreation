using UnityEngine;
﻿using UnityEngine.SceneManagement;
using System.Collections;

public class Result : MonoBehaviour {

	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
	}
}
