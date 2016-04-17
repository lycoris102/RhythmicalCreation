using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Title : MonoBehaviour {

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            iTween.ValueTo(this.gameObject, iTween.Hash(
                "from", 0.0f,
                "to", 1.0f,
                "time", 3.0f,
                "onupdate", "SetAlpha",
                "oncomplete", "GoToMain"
            ));

            iTween.ValueTo(this.gameObject, iTween.Hash(
                "from", 1.0f,
                "to", 0.0f,
                "time", 2.5f,
                "onupdate", "SetVolume",
                "oncomplete", "GoToMain"
            ));
        }
    }

    public void SetAlpha(float alpha)
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, alpha);
    }

    public void SetVolume(float volume)
    {
        Music.SetVolume(volume);
    }

    public void GoToMain()
    {
        SceneManager.LoadScene("main");
    }
}
