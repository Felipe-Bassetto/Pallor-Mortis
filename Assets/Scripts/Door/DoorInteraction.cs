using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    private Camera cameraPrincipal;
    public bool trancada;
    public GameObject doorPivot;

    // Start is called before the first frame update
    void Start()
    {
        cameraPrincipal = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Vector2 mousePos = cameraPrincipal.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePos);
            if (hit != null && hit.gameObject == gameObject)
            {
                if(trancada)
                {
                    Debug.Log("Trancada, fala da personagem, pop up");
                }
                else
                {
                    Debug.Log("Abre");
                }
            }
            
        }
        
        

    }
}
