using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject canvasPrefab;
    public GameObject planePrefab;
    public GameObject agentPrefab;

    public NavMeshData navMeshData;

    void Start()
    {
        
        if (planePrefab == null)
            planePrefab = GameObject.Find("Andres");

        if (planePrefab == null)
            agentPrefab = GameObject.Find("Agent");

        Vector3 planeCenterPos = getCenter(planePrefab.transform);


        GameObject planePrefabInstance = Instantiate(planePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        planePrefabInstance.AddComponent<UnityEngine.AI.NavMeshSurface>();
        NavMesh.AddNavMeshData(navMeshData);

        Vector3 newAgentPlaneSpawnPosition = new Vector3(transform.position.x, 1, transform.position.z);
        transform.position = newAgentPlaneSpawnPosition;
        GameObject agentPrefabInstance = Instantiate(agentPrefab, newAgentPlaneSpawnPosition, Quaternion.identity);
        agentPrefabInstance.AddComponent<UnityEngine.AI.NavMeshAgent>();
        agentPrefabInstance.AddComponent<AgentMovement>();

        Instantiate(canvasPrefab, planeCenterPos, Quaternion.identity);

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