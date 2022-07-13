using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class ActivateBOT : MonoBehaviour
{
    public Camera Cam;
    public NavMeshAgent Agent;

    [Space(10)]

    public GameObject EndPointBeam;
    public bool EnableDebug;

    [SerializeField]
    private Vector3 EndPoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit Hit;

            if (Physics.Raycast(ray, out Hit))
            {
                Agent.SetDestination(Hit.point);

                if (EnableDebug)
                {
                    StartCoroutine(ShowBeacon());
                }
            }
        }
    }

    IEnumerator ShowBeacon()
    {
        yield return new WaitForSeconds(0.5f);

        EndPoint = Agent.pathEndPosition;

        Instantiate(EndPointBeam, new Vector3(EndPoint.x, 99.5f + EndPoint.y, EndPoint.z), Quaternion.identity);
    }
}
