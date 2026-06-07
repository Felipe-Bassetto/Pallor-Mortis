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
    public Collider colliderEmpe;
    public Collider colliderAgachada;

    [Header("Scripts")]
    [SerializeField] private PlayerPOV pov;

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
            Vector3 movement = (transform.right * movementX) + (transform.forward * movementY);
            if(Input.GetKey(KeyCode.LeftShift))
            {
                rb.velocity = new Vector3(movement.x * speedRunning, rb.velocity.y, movement.z * speedRunning);
                anim.SetBool("Agaixada", false);
                pov.CamAgaixada(false);
                AtiveCollider();
            }
            else if(Input.GetKey(KeyCode.LeftControl))
            {
                rb.velocity = new Vector3(movement.x * speedCrouched, rb.velocity.y, movement.z * speed);
                anim.SetBool("Agaixada", true);
                pov.CamAgaixada(true);
                AtiveColliderAgachada();
            }
            else
            {
                rb.velocity = new Vector3(movement.x * speed, rb.velocity.y, movement.z * speed);
                anim.SetBool("Agaixada", false);
                pov.CamAgaixada(false);
                AtiveCollider();
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
        colliderEmpe.enabled = true;
    }
    public void AtiveColliderAgachada()
    {
        colliderAgachada.enabled = true;
        colliderEmpe.enabled = false;
    }
}
