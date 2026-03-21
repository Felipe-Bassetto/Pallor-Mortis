using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{
    [Header("Rigidbody")]
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public float speed;

    public bool playerLocked;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMove (InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>(); 
        movementX = movementVector.x; 
        movementY = movementVector.y; 
    }

    void FixedUpdate()
    {
        if(!playerLocked)
        {
            Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
            rb.velocity = movement * speed; 
        }
    }
    
    public void PlayMovement(bool move)
    {
        playerLocked = move;
    }
}
