using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    public StageCreator stageCreator;
    public GameObject cameraObject;
    public BackColor backColor;
    public TurnUI turnUI;
    public List<Sprite> buildingSpriteList;

    void Update()
    {
        if (IsPlayerTurn())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject buildingObject = stageCreator.JudgeBuildingObject();
                if (buildingObject != null)
                {
                    Building building = buildingObject.GetComponent<Building>();
                    if (building.isPushed == false)
                    {
                        StartCoroutine("ChangeBuilding", buildingObject);
                    }

                    // カメラ振動
                    iTween.ShakePosition(cameraObject, iTween.Hash(
                        "y", 0.1f,
                        "time", 0.1f
                    ));
                }
            }
            if (Music.Just.Bar % 2 == 1 && Music.Just.Beat == 3 && Music.Just.Unit == 3)
            {
                ChangeTurn();
            }
        }
    }

    IEnumerator ChangeBuilding (GameObject buildingObject) {
        Building building = buildingObject.GetComponent<Building>();

        // サイズ調整
        buildingObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        Vector3 position = buildingObject.transform.localPosition;
        buildingObject.transform.localPosition = new Vector3(
            position.x,
            position.y - 1.0f,
            position.z
        );

        iTween.MoveTo(buildingObject, iTween.Hash(
            "position", position,
            "time", 0.1f,
            "easeType", iTween.EaseType.easeInOutSine
        ));

        // 画像変更
        Sprite buildingSprite = buildingSpriteList[building.size - 1];
        buildingObject.GetComponent<SpriteRenderer>().sprite = buildingSprite;

        // パーティクル
        building.PlayParticle();

        stageCreator.SetGoodBuildingObjectList(buildingObject);

        building.isPushed = true;

        yield break;
    }

    void ChangeTurn()
    {
        backColor.ChangeBackColorCreator();
        turnUI.ChangeCreatorTurn();
    }

    bool IsPlayerTurn()
    {
        return !stageCreator.IsCreateTurn();
    }
}
