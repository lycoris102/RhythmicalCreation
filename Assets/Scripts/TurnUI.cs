using UnityEngine;
﻿using UnityEngine.UI;
using System.Collections;

public class TurnUI : MonoBehaviour {

    public GameObject SpaceKeyObject;
    public GameObject RepleatAfterMeTextObject;

    void Start() {
        ChangeCreatorTurn();
    }

    public void ChangePlayerTurn()
    {
        SpaceKeyObject.SetActive(true);
        RepleatAfterMeTextObject.SetActive(false);
    }

    public void ChangeCreatorTurn()
    {
        SpaceKeyObject.SetActive(false);
        RepleatAfterMeTextObject.SetActive(true);
    }
}
