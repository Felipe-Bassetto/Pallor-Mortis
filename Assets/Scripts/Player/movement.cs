using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float walk = 5f;

    private bool playerLocked = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerLocked)
        {
            WASD();
        }
    }

    void WASD()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * walk * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * walk * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * walk * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * walk * Time.deltaTime);
        }
    }
    
    public void PlayMovement(bool move)
    {
        playerLocked = move;
    }
}
