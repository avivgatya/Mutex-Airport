using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlightDetails : MonoBehaviour
{
    public Text textComp;
    private int numOfFlight;
    bool allowed = false;
    public Image image;
    private Sprite myFlag;
    private string[] runWays = new string[4] {"080","260","090","270"};
    public Text runway;
    public Text approved;
    string myRunway;
    private void Start()
    {
        myFlag = Spawner.GetRandomFlag();
        image.sprite = myFlag;
        myRunway=runWays[Random.Range(0,runWays.Length)];
        runway.text="Runway: "+myRunway;
    }
    private void Update()
    {
        if (allowed)
        {
            // if plane take too much time to deside land, in case of starvation func operates.
            //InCaseOfLanding
            //Invoke("InCaseOfStarvation",10.0f);
            SpawnPlane();
            allowed = false;
            approved.gameObject.SetActive(true);
        }
        else
        {
            allowed = ManagerSC.AskForPermissionToLand(myRunway);
        }
    }
    public void UpdateText(int num)
    {
        numOfFlight = num;
        string str= "Flight num: A" + num.ToString();
        Debug.Log(str);
        textComp.text = str;
    }
    public void SpawnPlane()
    {
        Spawner.SpawnInPosition(gameObject,myRunway, numOfFlight);
    }
    public Sprite GetFlag() => myFlag;
    
    public void InCaseOfStarvation()
    {
        //Stop landing
        allowed=false;
        ManagerSC.Landed(myRunway);
        

    }
}
