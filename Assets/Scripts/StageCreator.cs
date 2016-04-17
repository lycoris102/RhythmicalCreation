using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// XXX 超マッチョなのでクラス分けたい...

public class StageCreator : MonoBehaviour {

    public GameObject cameraObject;
    public GameObject countTextPositionObject;
    public GameObject resultObject;
    public GameObject buildingPrefab;
    public GameObject tipPrefab;
    public Vector3 basePosition;
    public Sprite buildingSilhouette;
    public List<Sprite> tipsSpriteList;
    public BackColor backColor;
    public TurnUI turnUI;
    public Score score;
    public bool isStarted = false;

    private int stageCount = 0;
    private int unitCount = 0;
    private float duration = 0.558f;
    private List<GameObject> buildingObjectList = new List<GameObject>();
    private List<GameObject> goodBuildingObjectList = new List<GameObject>();
    private List<List<int>> stageSettingList = new List<List<int>>();

    void Start()
    {
        resultObject.SetActive(false);
        LoadCsv();
        InitBuilding();
    }

    void Update()
    {
        if (Music.IsPlaying && Music.IsNearChanged && IsCreateTurn())
        {
            isStarted = true;
            if (Music.Just.Bar != 0 && Music.Just.Bar % 2 == 0 && Music.Just.Beat == 0 && Music.Just.Unit == 0)
            {
                Next();
            }

            if (stageSettingList[stageCount].Count > 0)
            {
                Create();

                if (Music.Just.Bar % 2 == 1 && Music.Just.Beat == 3 && Music.Just.Unit == 1)
                {
                    ChangeTurn();
                }
            }
        }
        if (!Music.IsPlaying && isStarted == true)
        {
            resultObject.SetActive(true);
        }
    }

    void Next()
    {
        AddScoreByGoodBuildingObjectList();
        HiddenBuilding();
        this.gameObject.transform.localPosition = basePosition;
        unitCount = 0;
        stageCount++;
    }

    void Create()
    {
        int size = stageSettingList[stageCount][unitCount];
        if (size > 0)
        {
            GameObject buildingObject = buildingObjectList[unitCount];
            buildingObject.GetComponent<SpriteRenderer>().sprite = buildingSilhouette;
            buildingObject.SetActive(true);
            Building building = buildingObject.GetComponent<Building>();

            building.size = size;
            building.isPushed = false;

            // サイズ変更
            Vector3 scale = buildingObject.transform.localScale;
            buildingObject.transform.localScale = new Vector3(
                scale.x,
                scale.y *= (size + 1),
                scale.z
            );

            // 位置調整
            Vector3 position = buildingObject.transform.localPosition;
            buildingObject.transform.localPosition = new Vector3(
                position.x,
                basePosition.y - size,
                position.z
            );

            // 生える建物
            iTween.MoveTo(buildingObject, iTween.Hash(
                "position", position,
                "time", 0.05,
                "easeType", iTween.EaseType.easeInOutSine
            ));

            // カメラ振動
            iTween.ShakePosition(cameraObject, iTween.Hash(
                "y", 0.1f,
                "time", 0.1f
            ));
        }
        unitCount++;
    }

    void ChangeTurn()
    {
        backColor.ChangeBackColorPlayer();
        turnUI.ChangePlayerTurn();
    }

    // 現在の拍に該当するオブジェクトが存在すれば返す
    public GameObject JustBuildingObject()
    {
        int justUnitCount = (((Music.Just.Bar % 2) * 16) + (Music.Just.Beat * 4) + Music.Just.Unit);
        Debug.Log("just:"+ justUnitCount);
        return buildingObjectList[justUnitCount];
    }

    public GameObject NearBuildingObject()
    {
        int nearUnitCount = (((Music.Near.Bar % 2) * 16) + (Music.Near.Beat * 4) + Music.Near.Unit);
        Debug.Log("near:"+ nearUnitCount);
        return buildingObjectList[nearUnitCount];
    }

    public GameObject NextNearBuildingObject()
    {
        int nearUnitCount = (((Music.Near.Bar % 2) * 16) + (Music.Near.Beat * 4) + Music.Near.Unit) + 1;
        Debug.Log("near:"+ nearUnitCount);
        return buildingObjectList[nearUnitCount];
    }

    public GameObject JudgeBuildingObject()
    {
        GameObject buildingObject = NearBuildingObject();
        if (buildingObject.activeSelf == false)
        {
            buildingObject = JustBuildingObject();
        }
        return buildingObject.activeSelf == true ? buildingObject : null;
    }

    public void SetGoodBuildingObjectList(GameObject buildingObject)
    {
        goodBuildingObjectList.Add(buildingObject);
    }

    public void AddScoreByGoodBuildingObjectList()
    {
        foreach (GameObject buildingObject in goodBuildingObjectList)
        {
            int size = buildingObject.GetComponent<Building>().size;
            GameObject tipObject = (GameObject)Instantiate(tipPrefab, buildingObject.transform.localPosition, Quaternion.identity);

            Sprite tipSprite = tipsSpriteList[size - 1];
            tipObject.GetComponent<SpriteRenderer>().sprite = tipSprite;

            iTween.MoveTo(tipObject, iTween.Hash(
                "position", countTextPositionObject.transform.localPosition,
                "time", 1.0f,
                "easeType", iTween.EaseType.easeInOutSine,
                "oncomplete", "AddScore",
                "oncompleteparams", tipObject,
                "oncompletetarget", this.gameObject
            ));
        }
        goodBuildingObjectList = new List<GameObject>();
    }

    public void AddScore(GameObject tipObject)
    {
        score.AddScore();
        Destroy(tipObject);
    }

    public bool IsCreateTurn()
    {
        return (Music.Just.Bar % 4 == 0 || Music.Just.Bar % 4 == 1) ? true : false;
    }

    void InitBuilding()
    {
        for (int i = 0; i < 32; i++)
        {
            Vector3 targetPosition = new Vector3(
                basePosition.x + duration * i,
                basePosition.y,
                basePosition.z
            );
            GameObject buildingObject = (GameObject)Instantiate(buildingPrefab, targetPosition, Quaternion.identity);
            buildingObject.SetActive(false);
            buildingObjectList.Add(buildingObject);
        }
    }

    void HiddenBuilding()
    {
        foreach (GameObject buildingObject in buildingObjectList)
        {
            buildingObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            buildingObject.GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            buildingObject.SetActive(false);
        }
    }

    void LoadCsv()
    {
        TextAsset csvFile = Resources.Load("stage", typeof(TextAsset)) as TextAsset;
        string[] lines = csvFile.text.Replace("\r\n", "\n").Split("\n"[0]);

        foreach (var line in lines)
        {
            if (line == "")
            {
                continue;
            }
            List<int> list = line.Split(',').Select(n => int.Parse(n)).ToList();
            stageSettingList.Add(list);
        }
    }

}
