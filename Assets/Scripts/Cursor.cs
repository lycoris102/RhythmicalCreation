using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour {

    public Vector3 basePosition;

    private float duration = 0.558f;

    void Update()
    {
        if (Music.IsNearChanged)
        {
            if (Music.Near.Bar % 2 == 0 && Music.Near.Beat == 0 && Music.Near.Unit == 0)
            {
                this.gameObject.transform.localPosition = basePosition;
            }
            else
            {
                Vector3 position = this.gameObject.transform.localPosition;
                this.gameObject.transform.localPosition = new Vector3(
                    position.x + duration,
                    position.y,
                    position.z
                );
            }
        }
    }


}
