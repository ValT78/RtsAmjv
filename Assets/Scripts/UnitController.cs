using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Camera cam;
    private RaycastHit hita;
    private RaycastHit hitb;
    private List<TroopBot> selectedTroops = new List<TroopBot>();
    [SerializeField] private bool isSelected;
    [SerializeField] private float maxSelectDist;
    private bool capacitiActive;
    private EnemyBot target;
    private Vector3 barycenter;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isChosingTroop) return;
        // Clear selection with escape
        if (Input.GetKeyDown(KeyCode.Escape)) ClearSelection();

        Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
        // selection des unités
        if (!isSelected)
        {
            if (Input.GetMouseButtonDown(0) && Physics.Raycast(mouseRay, out hita)) hitb = hita;
            if (Input.GetMouseButtonUp(0) && Physics.Raycast(mouseRay, out hitb))
            {
                if (Vector3.Distance(hita.point, hitb.point) < 0.5f) isSelected = SelectTroop(hita.point); // Single target mode
                else isSelected = SelectTroop(hita.point, hitb.point); // Multitarget mod
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && Physics.Raycast(mouseRay, out hita))
            {
                /* 4 possibilité : 
                 *  - avancer vers une postion avec GoToPosition(Vector3 position) click sur le sol
                 *  - avancer vers un enemy avec GoToBot(AttackController target) clic sur un bot
                 *  - capacité spécial [bientot] E puis clic 
                 *  - Avancer vers une position MAIS si un ennemi entre dans son champs de vision,
                 *  ça devient sa cible temporaire AwarePosition(Vector3 mainTarget) shift + clic
                 *  
                 *  Pour faciliter et optimiser la résoltion de ces évenement on va les faire dans l'odre 3 4 2 1
                 */

                if (capacitiActive)
                {
                    foreach (TroopBot troop in selectedTroops) troop.attackController.Special();
                }
                else if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    foreach (TroopBot troop in selectedTroops) troop.AwarePosition(hita.point);
                }
                else if (SelectEnemy(hita.point))
                {
                    foreach (TroopBot troop in selectedTroops) troop.GoToBot(target);
                }
                else foreach (TroopBot troop in selectedTroops)
                {
                    barycenter = calculateBarycenter();
                    Vector3 offset = troop.transform.position - barycenter;
                    troop.GoToPosition(hita.point + offset);
                }

            }

        }

        if (Input.GetKeyDown(KeyCode.E)) capacitiActive = !capacitiActive;

    }

    private void ClearSelection()
    {
        isSelected = false;
        hita = new RaycastHit();
        hitb = new RaycastHit();
        selectedTroops.Clear();
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            if (isSelected)
            {
                foreach (TroopBot troop in selectedTroops)
                {
                    if (troop == null) return;
                    Gizmos.DrawSphere(troop.transform.position + Vector3.up * 2, 0.1f);
                    if (troop.GetComponent<NavMeshAgent>().hasPath)
                    {
                        Gizmos.DrawLine(troop.transform.position, hita.point);    
                        Gizmos.DrawSphere(hita.point, 0.5f);
                    }
                }
            }
            else
            {
                Gizmos.DrawSphere(hita.point, 0.5f);
                Gizmos.DrawSphere(hitb.point, 0.5f);
                DrawRect(CreateRect(hita.point, hitb.point));
            }

            if(target != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(target.transform.position + Vector3.up * 2, 0.2f);
                Gizmos.color = Color.grey;
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
    private bool SelectTroop(Vector3 hitpoint)
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
        selectedTroops.Add(selectedTroop);
        return true;
    }

    private bool SelectTroop(Vector3 hitpointa, Vector3 hitpointb)
    {
        // multimode
        Rect zone = CreateRect(hitpointa, hitpointb);
        foreach (TroopBot troop in GameManager.troopUnits)
        {
            Vector2 pos = new Vector2(troop.transform.position.x, troop.transform.position.z);
            if (zone.Contains(pos))
            {
                selectedTroops.Add(troop);
            }
        }
        return selectedTroops.Count != 0;
    }

    private bool SelectEnemy(Vector3 hitpoint)
    {
        // mono mode 
        float mindist = maxSelectDist;
        float dist;
        target = null;
        foreach (EnemyBot troop in GameManager.enemyUnits)
        {
            dist = Vector3.Distance(hitpoint, troop.transform.position);
            if (dist < mindist)
            {
                mindist = dist;
                target = troop;
            }
        }
        return target != null;
    }

    private Vector3 calculateBarycenter()
    {
        barycenter = Vector3.zero;
        foreach (TroopBot troop in selectedTroops)
        {
            barycenter += troop.transform.position;
        }
        return barycenter / selectedTroops.Count();
    }
}
