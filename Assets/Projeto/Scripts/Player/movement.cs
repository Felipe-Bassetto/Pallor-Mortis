using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("Rigidbody")]
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public float speed;
    public float speedRunning;

    public bool playerLocked = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMove (InputValue movementValue)
    {
        if (!playerLocked)
        {
            Vector2 movementVector = movementValue.Get<Vector2>();
            movementX = movementVector.x;
            movementY = movementVector.y;
        }
    }

    void FixedUpdate()
    {
        if(!playerLocked)
        {
            Vector3 movement = transform.right * movementX + transform.forward * movementY;
            if(Input.GetKey(KeyCode.LeftShift))
            {
                rb.velocity = new Vector3(movement.x * speedRunning, rb.velocity.y, movement.z * speedRunning);
            }
            else
            {
                rb.velocity = new Vector3(movement.x * speed, rb.velocity.y, movement.z * speed);
            }
        }
    }
    
    public void PlayMovement(bool move)
    {
        playerLocked = move;
    }
}
