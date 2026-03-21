using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("GameObjects")]
    public PlayerPOV POV;

    // Start is called before the first frame update
    void Start()
    {
        POV.CamLock(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
