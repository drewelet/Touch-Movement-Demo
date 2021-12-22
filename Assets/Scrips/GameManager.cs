using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject canvasPrefab;
    public GameObject planePrefab;
    public NavMeshData navMeshData;

    void Start()
    {
        if (planePrefab == null)
            planePrefab = GameObject.Find("Andres");

        Vector3 planeCenterPos = getCenter(planePrefab.transform);

        Instantiate(planePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        planePrefab.AddComponent<UnityEngine.AI.NavMeshSurface>();
        NavMesh.AddNavMeshData(navMeshData);


        Instantiate(canvasPrefab, planeCenterPos, Quaternion.identity);

       // planePrefab.GetComponent<UnityEngine.AI.NavMeshSurface>();
    }

    //Get Center Of Plane
    Vector3 getCenter(Transform obj)
    {
        Vector3 center = new Vector3();
        if (obj.GetComponent<Renderer>() != null)
        {
            center = obj.GetComponent<Renderer>().bounds.center;
        }
        else
        {
            foreach (Transform subObj in obj)
            {
                center += getCenter(subObj);
            }
            center /= obj.childCount;
        }
        return center;
    }
}