using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    //Variables
    public GameObject canvasPrefab;
    public GameObject planePrefab;
    public GameObject agentPrefab;

    void Start()
    {
        if (planePrefab == null)
            planePrefab = GameObject.Find("Andres_Plane");

        if (agentPrefab == null)
            agentPrefab = GameObject.Find("Agent");

        Vector3 planeCenterPos = getCenter(planePrefab.transform);


        GameObject planePrefabInstance = Instantiate(planePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        planePrefabInstance.AddComponent<UnityEngine.AI.NavMeshSurface>();
        NavMeshSurface navMeshSurfaceInstance = planePrefabInstance.GetComponent<UnityEngine.AI.NavMeshSurface>();
        navMeshSurfaceInstance.BuildNavMesh();

       // Vector3 newAgentPlaneSpawnPosition = new Vector3(transform.position.x, 1, transform.position.z);
      //  transform.position = newAgentPlaneSpawnPosition;
        GameObject agentPrefabInstance = Instantiate(agentPrefab, planeCenterPos, Quaternion.identity);
        agentPrefabInstance.AddComponent<UnityEngine.AI.NavMeshAgent>();
        agentPrefabInstance.AddComponent<AgentMovement>();

        Instantiate(canvasPrefab, planeCenterPos, Quaternion.identity);
    }

    //Get Object Center
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