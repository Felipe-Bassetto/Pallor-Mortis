using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float walk = 5f;

    [Header("Rigidbody")]
    private Rigidbody rb;

    private bool playerLocked = false;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
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
            rb.AddForce(Vector3.forward * 10f);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * walk * Time.deltaTime);
            rb.AddForce(Vector3.back * 10f);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * walk * Time.deltaTime);
            rb.AddForce(Vector3.left * 10f);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * walk * Time.deltaTime);
            rb.AddForce(Vector3.right * 10f);
        }
    }
    
    public void PlayMovement(bool move)
    {
        playerLocked = move;
    }
}
