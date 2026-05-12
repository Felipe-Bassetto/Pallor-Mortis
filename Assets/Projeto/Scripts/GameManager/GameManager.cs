using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("GameObjects")]
    public PlayerPOV POV;
    public GameObject memorie;

    [Header("Scripts")]
    [SerializeField] private SoundManager sm;

    // Start is called before the first frame update
    void Start()
    {
        POV.CamLock(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseMemorie()
    {
        memorie.SetActive(false);
        sm.PlaySound(13);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
