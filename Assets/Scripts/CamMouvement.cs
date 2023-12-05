using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMouvement : MonoBehaviour
{
    [SerializeField] float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h_mouvement = Mathf.Round(Input.GetAxis("Horizontal") * 100) / 100;
        float v_mouvement = Mathf.Round(Input.GetAxis("Vertical") * 100) / 100;
        transform.Translate(new Vector3(h_mouvement, 0, v_mouvement)*speed*Time.deltaTime, Space.World);
        
    }
}
