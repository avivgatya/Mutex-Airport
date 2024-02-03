using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject refToPlaneInObject;
    private static GameObject plane1;
    private static GameObject plane1Instance;
    private static Transform canvas;
    public Transform refToCanvasInObject;

    public Sprite[] refToflags = new Sprite[20];
    private static Sprite[] flags = new Sprite[20];

    private void Start()
    {
        plane1 = refToPlaneInObject;
        canvas = refToCanvasInObject;
        flags = refToflags;
    }
    public static void SpawnInPosition(GameObject flightDetailesParent,string runWayNumber,int num)
    {
        plane1Instance = Instantiate(plane1, canvas);
        RectTransform rect = plane1Instance.GetComponent<RectTransform>();
        Plane planeScript = plane1Instance.GetComponent<Plane>();
        planeScript.UpdateParent(flightDetailesParent);
        planeScript.UpdateRunWayAndNum(runWayNumber,num);

        switch (runWayNumber)
        {
            case "080":
                rect.localPosition = new Vector3(676, 261, 0);
                rect.rotation = Quaternion.Euler(0, 0, 10);
                break;
            case "260":
                rect.localPosition = new Vector3(-715, 16, 0);
                rect.rotation = Quaternion.Euler(0, 0, -170);
                break;
            case "090":
                rect.localPosition = new Vector3(500, -111, 0);
                rect.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case "270":
                rect.localPosition = new Vector3(-500, -111, 0);
                rect.rotation = Quaternion.Euler(0, 0, -180);
                break;

        }
    }
    public static Sprite GetRandomFlag() => flags[Random.Range(0, flags.Length)];
   

}
