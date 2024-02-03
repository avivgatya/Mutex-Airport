using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class ManagerSC : MonoBehaviour
{
    int numFlights = 99;
    public GameObject flightPrefab;
    public Transform flightsBoard;
    private GameObject newFlight;
    private static string[] stringsFromTest;
    private void Start()
    {
        string[] write = { "true", "true" };
        File.WriteAllLines(Application.dataPath + '/' + "DataTextFile.txt", write);
    }
    public void NewFlight()
    {
        numFlights++;
        Debug.Log("New Flight! num: " + numFlights);
        
        newFlight=Instantiate(flightPrefab, flightsBoard);
        newFlight.GetComponent<FlightDetails>().UpdateText(numFlights);
    }
    public static bool AskForPermissionToLand(string runWayNumber)
    {
        string[] write = new string[2];
        string[] read = File.ReadAllLines(Application.dataPath + '/' + "DataTextFile.txt");
        if (runWayNumber == "080" || runWayNumber == "260")
        {
            if(read[0]=="true")
            {
                write[0]="false";
                write[1]=read[1];
                File.WriteAllLines(Application.dataPath + '/' + "DataTextFile.txt", write);
                return true;
            }
        }
        if (runWayNumber == "090" || runWayNumber == "270")
        {
            if (read[1] == "true")
            {
                write[1] = "false";
                write[0] = read[0];
                File.WriteAllLines(Application.dataPath + '/' + "DataTextFile.txt", write);
                return true;
            }
        }
        return false; 
    }
    public static void Landed(string runWayNumber)
    {
        string [] write =new string[2];
        string [] read = File.ReadAllLines(Application.dataPath + '/' + "DataTextFile.txt");
        if (runWayNumber == "080" || runWayNumber == "260")
        {
            write[0] = "true";
            write[1] = read[1];
            File.WriteAllLines(Application.dataPath + '/' + "DataTextFile.txt",write);
        }
        if (runWayNumber == "090" || runWayNumber == "270")
        {
            write[1] = "true";
            write[0] = read[0];
            File.WriteAllLines(Application.dataPath + '/' + "DataTextFile.txt", write);
        }
    }
}
