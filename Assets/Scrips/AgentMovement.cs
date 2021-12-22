using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    //Variables
    private UnityEngine.AI.NavMeshAgent agent;

    private Camera mainCamera;

    private LineRenderer lineRenderer;

    private GameObject clickMarker;
    private GameObject lineRendererPos;

    //Start Function -- Sets all variables and components
    void OnEnable()
    {

        clickMarker = this.gameObject.transform.GetChild(0).gameObject;

        if (clickMarker == null)
            clickMarker = GameObject.Find("clickMarker");

        if (lineRendererPos == null)
            lineRendererPos = GameObject.Find("lineRendererPos");

        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("No camera found.");
        }

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.startWidth = 0.25f;
        lineRenderer.endWidth = 0.25f;
        lineRenderer.positionCount = 0;
    }

    //Update Function -- Calls all other functions on every frame
    void Update()
    {
        if (agent.hasPath)
        {
            DrawPath();
        }

        TouchMovement();
    }

    //Movement Function -- Touch and drag movement
    void TouchMovement()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hitInfo;

            switch (touch.phase)
            {
                case TouchPhase.Moved:
                    if (Physics.Raycast(ray, out hitInfo))
                    {
                        agent.SetDestination(hitInfo.point);
                    }
                    break;

                case TouchPhase.Stationary:
                    if (Physics.Raycast(ray, out hitInfo))
                    {
                        agent.SetDestination(hitInfo.point);
                    }
                    break;

                default:
                    break;
            }

        }
        else
        {
            CheckPath();
        }
    }

    //DrawPath Function -- Draws the linerenderer with the agent's desired path.
    void DrawPath()
    {
        lineRenderer.positionCount = agent.path.corners.Length;
        lineRenderer.SetPosition(0, lineRendererPos.transform.position);

        if (agent.path.corners.Length < 2)
        {
            return;
        }

        for (int i = 1; i < agent.path.corners.Length; i++)
        {
            Vector3 pointPosition = new Vector3(agent.path.corners[i].x, agent.path.corners[i].y, agent.path.corners[i].z);
            lineRenderer.SetPosition(i, pointPosition);
            clickMarker.SetActive(true);
            clickMarker.transform.position = new Vector3 (pointPosition.x, .1f, pointPosition.z);
        }

    }

    //Check Function -- Checks if agent has reached the desired path yet.
    void CheckPath()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    clickMarker.SetActive(false);
                }
            }
        }
    }
}