using UnityEngine;

public class FootStepManager : MonoBehaviour
{
    [Header("Componentes")]
    public Rigidbody rb; 
    public AudioSource audioSource;

    [Header("Andando")]
    public float walkStepDistance = 2.5f; 
    public AudioClip[] clipsWalk;

    [Header("Correndo")]
    public float runStepDistance = 1.3f;
    public AudioClip[] clipsRun;

    [Header("Física")]
    public float velocityThreshold = 0.3f;
    private float timer;
    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    void Update()
    {
        bool isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift); // Verificação

        float currentStepDistance = isRunning ? runStepDistance : walkStepDistance;
        AudioClip[] currentClips = isRunning ? clipsRun : clipsWalk;

        if (rb.velocity.magnitude < velocityThreshold)
        {
            timer = 0;
            if (audioSource.isPlaying) audioSource.Stop(); 
            lastPosition = transform.position;
            return;
        }

        float distanceMoved = Vector3.Distance(
            new Vector3(transform.position.x, 0, transform.position.z), 
            new Vector3(lastPosition.x, 0, lastPosition.z)
        );

        if (IsGrounded())
        {
            timer += distanceMoved;

            if (timer >= currentStepDistance)
            {
                PlayFootstep(currentClips);
                timer = 0;
            }
        }

        lastPosition = transform.position;
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.5f);
    }

    void PlayFootstep(AudioClip[] selectedClips)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f))
        {
            if (hit.collider.CompareTag("Ground") && selectedClips.Length > 0)
            {
                audioSource.clip = selectedClips[Random.Range(0, selectedClips.Length)];
                audioSource.Play();
            }
        }
    }
}