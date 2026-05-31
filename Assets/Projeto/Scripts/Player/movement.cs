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
    public float speedCrouched;

    public bool playerLocked;

    [Header("Animator")]
    Animator anim;

    [Header("Colliders")]
    public Collider collider;
    public Collider colliderAgachada;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void OnMove (InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
        anim.SetBool("Andando", movementVector != Vector2.zero);
    }

    void FixedUpdate()
    {
        if(!playerLocked)
        {
            Vector3 movement = (transform.right * movementX * -1) + (transform.forward * movementY * -1);
            if(Input.GetKey(KeyCode.LeftShift))
            {
                rb.velocity = new Vector3(movement.x * speedRunning, rb.velocity.y, movement.z * speedRunning);
                anim.SetBool("Agaixada", false);
            }
            else if(Input.GetKey(KeyCode.LeftControl))
            {
                rb.velocity = new Vector3(movement.x * speedCrouched, rb.velocity.y, movement.z * speed);
                anim.SetBool("Agaixada", true);
            }
            else
            {
                rb.velocity = new Vector3(movement.x * speed, rb.velocity.y, movement.z * speed);
                anim.SetBool("Agaixada", false);
            }
        }
    }
    
    public void PlayMovement(bool move)
    {
        playerLocked = move;
    }

    public void AtiveCollider()
    {
        colliderAgachada.enabled = false;
        collider.enabled = true;
    }
    public void AtiveColliderAgachada()
    {
        colliderAgachada.enabled = true;
        collider.enabled = false;
    }
}
