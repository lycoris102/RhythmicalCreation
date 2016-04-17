using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Transition : MonoBehaviour {

	void Start () {
        iTween.ValueTo(this.gameObject, iTween.Hash(
            "from", 1.0f,
            "to", 0.0f,
            "time", 3.0f,
            "onupdate", "SetValue",
            "oncomplete", "PlayMusic"
        ));
    }

    public void SetValue(float alpha)
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, alpha);
    }

    public void PlayMusic()
    {
        Music.Play("Music");
    }
}
