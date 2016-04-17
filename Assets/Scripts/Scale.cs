using UnityEngine;
using System.Collections;

public class Scale : MonoBehaviour {

    public float rate = 1.0f;
    public float time = 0.1f;

    private Vector3 baseScale;

    void Start () {
        baseScale = this.gameObject.transform.localScale;
    }

    void Update () {
        if (Music.IsPlaying && Music.IsJustChangedBeat())
        {
            iTween.ValueTo(this.gameObject, iTween.Hash(
                "from", rate,
                "to", 1.0f,
                "time", time,
                "onupdate", "SetValue"
            ));
        }
    }

    public void SetValue(float rate)
    {
        this.gameObject.transform.localScale = new Vector3(
            baseScale.x * rate,
            baseScale.y * rate,
            baseScale.z * rate
        );
    }
}
