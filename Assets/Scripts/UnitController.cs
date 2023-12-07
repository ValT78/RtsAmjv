using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Camera cam;
    private RaycastHit hit;
    private NavMeshAgent unitAgent;
    [SerializeField] private bool isSelected;
    [SerializeField] private float maxSelectDist;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(mouseRay, out hit))
        {
            if (isSelected)
            {
                unitAgent.SetDestination(hit.point);
            }
            else
            {
                isSelected = SelectAgent(hit.point);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isSelected = false;
            unitAgent = null;
        }

    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            if (unitAgent != null )
            {
                Gizmos.DrawSphere(unitAgent.transform.position + Vector3.up * 2, 0.1f);
                if(unitAgent.hasPath) Gizmos.DrawSphere(hit.point, 0.5f);
            }
        }
    }

    private bool SelectAgent(Vector3 hitpoint)
    {
        float mindist = maxSelectDist;
        float dist;
        TroopBot selectedTroop = null;
        foreach(TroopBot troop in GameManager.troopUnits)
        {
            dist = Vector3.Distance(hitpoint, troop.transform.position);
            if (dist < mindist)
            {
                mindist = dist;
                selectedTroop = troop;
            }
        }
        if (selectedTroop == null) return false;
        unitAgent = selectedTroop.GetComponent<NavMeshAgent>();
        return true;
    }
}
