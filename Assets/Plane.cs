using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Plane : MonoBehaviour
{
    public RectTransform rect;
    float size =0.02f;
    float currentSize=1;
    float velocity = 80f; //30
    GameObject flightDetailesParent;
    public Image flag;
    bool destoryed = false;
    float rotation = 0;
    private string runWay;
    public Text numberTxt;
    public RectTransform numberRec;
    static IDictionary<string, LandingPoint> runWaysInfo = new Dictionary<string, LandingPoint>() 
    { 
        { "080", new LandingPoint(50,true) },
        { "260", new LandingPoint(-195, false) },
        { "090", new LandingPoint(147,true) },
        { "270", new LandingPoint(-110, false) }
    };
    private void Update()
    {
        Debug.Log("Moves");
        rect.Translate(Vector3.right*-velocity*Time.deltaTime);
        if (!destoryed)
        {
            if (rect.localScale.x > 0.5)
            {
                currentSize = currentSize - size * Time.deltaTime;
                rect.localScale = new Vector3(currentSize, currentSize, 1);
            }
            if (runWaysInfo[runWay].parkingClockwise)
            {
                if (rect.localPosition.x < runWaysInfo[runWay].stoppingPoint)
                    velocity -= 15 * Time.deltaTime;
            }
            else
            {
                if (rect.localPosition.x > runWaysInfo[runWay].stoppingPoint)
                    velocity -= 15 * Time.deltaTime;
            }
            if (velocity < 5)
            {
                destoryed = true;
                velocity = 10;
            }
        }
        else
        {
            if (runWaysInfo[runWay].parkingClockwise)
                rect.rotation = Quaternion.Euler(0, 0, rect.rotation.z - rotation);
            else
                rect.rotation = Quaternion.Euler(0, 0, -170 + rotation);
            rotation += 15 * Time.deltaTime;
            if (rotation > 85)
            {
                Destroy(gameObject);
                Debug.Log("Des!!!!!!");
                ManagerSC.Landed(runWay);
                Destroy(flightDetailesParent);
            }
        }
    }
    public void UpdateParent(GameObject parent)
    {
        flightDetailesParent= parent;
        flag.sprite=parent.GetComponent<FlightDetails>().GetFlag();
    }
    public void UpdateRunWayAndNum(string runWay,int num)
    {
        this.runWay= runWay;
        numberTxt.text = "A"+num.ToString();
        if(!runWaysInfo[runWay].parkingClockwise)
            numberRec.rotation = Quaternion.Euler(0, 0, 180);
         
    }
}
public class LandingPoint
{
    public int stoppingPoint;
    public bool parkingClockwise;
    public LandingPoint(int stoppingPoint,bool parkingClockwise)
    {
        this.stoppingPoint= stoppingPoint;
        this.parkingClockwise= parkingClockwise;
    }
}
