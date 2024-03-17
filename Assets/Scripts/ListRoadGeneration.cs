using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListRoadGeneration : MonoBehaviour
{

    public GameObject roadPrefab;

    public Vector3 lastRoadPos;
    public float offset = 0.7071068f;

    private int roadCount = 0;

    public Rigidbody rb;

    public float distanceToLastRoad;
    public float distanceFromOldRoad;

    private GameObject oldRoad;
    private GameObject newRoad;

    private List<GameObject> oldRoadsList = new List<GameObject>();

    //public void StartBuildingRoad()
    //{
    //    InvokeRepeating("EndlessRoad", 0f, .2f);
    //    InvokeRepeating("DestroyRoadBlock", 0f, .2f);
    //}

    public void Awake()
    {
        //StartBuildingRoad();
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space)) { CreateNewRoadBlock(); }
        //if (Input.GetKeyDown(KeyCode.G)) { DestroyRoadBlock(); }

        distanceToLastRoad = lastRoadPos.magnitude - rb.position.magnitude;
        Debug.Log("Distance to Last Road: " +  distanceToLastRoad);
        if (distanceToLastRoad < 10)
        {
            Debug.Log("sqrMag of last road - sqrMag of player is: " 
                + (lastRoadPos.magnitude - rb.position.magnitude).ToString());
            EndlessRoad();
        }

        distanceFromOldRoad = rb.position.magnitude - oldRoadsList[0].transform.position.magnitude;
        Debug.Log("Distance from Old Road: " + distanceFromOldRoad);
        if (distanceFromOldRoad > 3)
        {
            Debug.Log("Destroy road block.");
            Debug.Log("sqrMag of player is - sqrMag of gameObject is: " 
                + (rb.position.magnitude - transform.position.magnitude).ToString());
            DestroyRoadBlock();
        }
    }

    public void EndlessRoad()
    {
        Debug.Log("Inside EndlessRoad");
        CreateNewRoadBlock();
    }

    public void DestroyRoadBlock()
    {
        Debug.Log("Inside DestroyRoadBlock " + oldRoadsList[0].ToString());
        Destroy(oldRoadsList[0]);
        oldRoadsList.RemoveAt(0);
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

        newRoad = Instantiate(roadPrefab,spawnPos,Quaternion.Euler(0,45,0));
        lastRoadPos = newRoad.transform.position;
        roadCount++;
        if (roadCount % 5 == 0 ) { newRoad.transform.GetChild(0).gameObject.SetActive(true); }
        oldRoadsList.Add(newRoad);
    }

}
