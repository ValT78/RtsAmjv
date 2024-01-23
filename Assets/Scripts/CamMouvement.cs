using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMouvement : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] Vector3 corner1;
    [SerializeField] Vector3 corner2;
    private Rect playzone;

    // Start is called before the first frame update
    void Start()
    {
        playzone = CreateRect(corner1, corner2);
    }

    // Update is called once per frame
    void Update()
    {
        float h_mouvement = Mathf.Round(Input.GetAxis("Horizontal") * 100) / 100;
        float v_mouvement = Mathf.Round(Input.GetAxis("Vertical") * 100) / 100;
        Vector3 translate = new Vector3(-h_mouvement, 0, -v_mouvement) * speed * Time.deltaTime;
        if(playzone.Contains(new Vector2(transform.position.x + translate.x,
            transform.position.z + translate.z))) transform.Translate(translate, Space.World);
    }


    private Rect CreateRect(Vector3 posa, Vector3 posb)
    {
        Vector2 pos2a = new Vector2(posa.x, posa.z);
        Vector2 pos2b = new Vector2(posb.x, posb.z);

        Vector2 corner = new Vector2(Mathf.Min(pos2a.x, pos2b.x), Mathf.Min(pos2a.y, pos2b.y));
        Vector2 size = new Vector2(Mathf.Abs(pos2a.x - pos2b.x), Mathf.Abs(pos2a.y - pos2b.y));

        return new Rect(corner, size);
    }

    private void OnDrawGizmosSelected()
    {
        DrawRect(CreateRect(corner1, corner2));
    }

    void DrawRect(Rect rect)
    {
        Gizmos.DrawWireCube(new Vector3(rect.center.x, 0.01f, rect.center.y), new Vector3(rect.size.x, 0.01f, rect.size.y));
    }
}
