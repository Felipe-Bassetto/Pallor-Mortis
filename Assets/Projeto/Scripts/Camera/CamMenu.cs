using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMenu : MonoBehaviour
{
    [Header("Configurações de Camera(POV)")]
    public bool camMove;
    public float velocidade;

    Camera cam;
    private Vector3 camLocation = new Vector3(0, 1, 0);

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (camMove) cam.transform.position = Vector3.MoveTowards(cam.transform.position, camLocation, velocidade * Time.unscaledDeltaTime);
        if (cam.transform.position == camLocation) CameraMovement(false);
    }
    public void CameraMovement(bool move)
    {
        camMove = move;
    }
}
