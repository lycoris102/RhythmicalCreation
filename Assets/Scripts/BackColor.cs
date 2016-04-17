using UnityEngine;
using System.Collections;

public class BackColor : MonoBehaviour {

    public GameObject backColorObject;

    public void ChangeBackColorCreator()
    {
        Vector3 position = backColorObject.transform.localPosition;
        _ChangeBackColor(new Vector3(
            0,
            position.y,
            position.z
        ));
    }
    public void ChangeBackColorPlayer()
    {
        Vector3 position = backColorObject.transform.localPosition;
        _ChangeBackColor(new Vector3(
            -18.5f,
            position.y,
            position.z
        ));
    }
    void _ChangeBackColor(Vector3 position)
    {
        iTween.MoveTo(backColorObject, iTween.Hash(
            "position", position,
            "time", 0.2f,
            "easeType", iTween.EaseType.easeInOutSine
        ));
    }
}
