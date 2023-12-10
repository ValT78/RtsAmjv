using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private RaycastHit hita;
    private RaycastHit hitb;
    private List<NavMeshAgent> unitsAgent = new List<NavMeshAgent>();
    private List<TroopBot> selectedTroops = new List<TroopBot>();
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
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(mouseRay, out hita)) hitb = hita;
        if (Input.GetMouseButtonUp(0) && Physics.Raycast(mouseRay, out hitb))
        {
            if (isSelected) foreach (NavMeshAgent unitAgent in unitsAgent) unitAgent.SetDestination(hitb.point);
            else
            {
                if (Vector3.Distance(hita.point, hitb.point) < 0.5f) isSelected = SelectAgent(hita.point); // Single target mode
                else isSelected = SelectAgent(hita.point, hitb.point); // Multitarget mod
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isSelected = false;
            
            hita = new RaycastHit();
            hitb = new RaycastHit();

            unitsAgent.Clear();
        }

    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {


            if (unitsAgent.Count != 0)
            {
                foreach (NavMeshAgent unitAgent in unitsAgent)
                {
                    Gizmos.DrawSphere(unitAgent.transform.position + Vector3.up * 2, 0.1f);
                    if (unitAgent.hasPath)
                    {
                        Gizmos.DrawLine(unitAgent.transform.position, hitb.point);    
                        Gizmos.DrawSphere(hitb.point, 0.5f);
                    }
                }
            }
            else
            {
                Gizmos.DrawSphere(hita.point, 0.5f);
                Gizmos.DrawSphere(hitb.point, 0.5f);
                DrawRect(CreateRect(hita.point, hitb.point));
            }
        }
    }

    private Rect CreateRect(Vector3 posa, Vector3 posb)
    {
        Vector2 pos2a = new Vector2(posa.x, posa.z);
        Vector2 pos2b = new Vector2(posb.x, posb.z);

        Vector2 corner = new Vector2(Mathf.Min(pos2a.x, pos2b.x), Mathf.Min(pos2a.y, pos2b.y));
        Vector2 size = new Vector2(Mathf.Abs(pos2a.x - pos2b.x), Mathf.Abs(pos2a.y - pos2b.y));

        return new Rect(corner, size);
    }

    void DrawRect(Rect rect)
    {
        Gizmos.DrawWireCube(new Vector3(rect.center.x, 0.01f, rect.center.y), new Vector3(rect.size.x, 0.01f, rect.size.y));
    }
    private bool SelectAgent(Vector3 hitpoint)
    {
        // mono mode 
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
        unitsAgent.Add(selectedTroop.GetComponent<NavMeshAgent>());
        return true;
    }

    private bool SelectAgent(Vector3 hitpointa, Vector3 hitpointb)
    {
        // multimode
        List<TroopBot> selectedTroops = new List<TroopBot>();
        Rect zone = CreateRect(hitpointa, hitpointb);
        foreach (TroopBot troop in GameManager.troopUnits)
        {
            Vector2 pos = new Vector2(troop.transform.position.x, troop.transform.position.z);
            if (zone.Contains(pos)) selectedTroops.Add(troop);
        }
        if (selectedTroops.Count == 0) return false;
        foreach (TroopBot selectedTroop in selectedTroops) unitsAgent.Add(selectedTroop.GetComponent<NavMeshAgent>());
        return true;
    }
}
