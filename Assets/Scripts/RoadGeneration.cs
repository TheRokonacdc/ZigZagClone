using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGeneration : MonoBehaviour
{

    public GameObject roadPrefab;

    public Vector3 lastRoadPos;
    public float offset = 0.7071068f;

    private int roadCount = 0;


    public void StartBuildingRoad()
    {
        InvokeRepeating("CreateNewRoadBlock", 0f, .5f);
    }

    public void CreateNewRoadBlock()
    {
        Debug.Log("Create new road block.");

        Vector3 spawnPos = Vector3.zero;

        float chance = Random.Range(0, 99);
        if (chance < 50)
        {
            spawnPos = new Vector3(lastRoadPos.x + offset, lastRoadPos.y, lastRoadPos.z + offset);
        }
        else { spawnPos = new Vector3(lastRoadPos.x - offset, lastRoadPos.y, lastRoadPos.z + offset); }
        
        GameObject newRoad = Instantiate(roadPrefab,spawnPos,Quaternion.Euler(0,45,0));
        lastRoadPos = newRoad.transform.position;
        roadCount++;
        if (roadCount % 5 == 0 ) { newRoad.transform.GetChild(0).gameObject.SetActive(true); }
    }

}
