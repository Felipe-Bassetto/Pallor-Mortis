using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class SoundManager : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField] private AudioSource[] sources;

    public AudioClip[] audios;
    public AudioClip[] musicas;

    [Header("Variaveis")]
    private bool diminuirVolume;
    private bool aumentaVolume;
    private AudioSource audioSourceAumentar;
    private AudioSource audioSourceDiminuir;

    [SerializeField] private float speedGradual;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(audioSourceAumentar != null)
        {
            if (aumentaVolume) audioSourceAumentar.volume += speedGradual * Time.deltaTime;

            if (audioSourceAumentar.volume >= 1f)
            {
                audioSourceAumentar.volume = 1;
                aumentaVolume = false;
            }
        }
        if(audioSourceDiminuir != null)
        {
            if (diminuirVolume) audioSourceDiminuir.volume -= speedGradual * Time.deltaTime;

            if (audioSourceDiminuir.volume <= 0f)
            {
                audioSourceDiminuir.volume = 0;
                diminuirVolume = false;
            }
        }
    }

    public void PlaySound(int index)
    {
        sources[0].PlayOneShot(audios[index]);
    }

    public void PlayLoop(int index)
    {
        sources[1].clip = audios[index];
        sources[1].Play();
    }

    public void PlayOST(int index)
    {
        sources[3].clip = musicas[index];
        sources[3].Play();
    }

    public void PararLoop()
    {
        sources[1].Stop();
    }

    public void AumentarVolumeGradual(int source)
    {
        aumentaVolume = true;
        audioSourceAumentar = sources[source];
    }

    public void DiminuirVolumeGradual(int source)
    {
        diminuirVolume = true;
        audioSourceDiminuir = sources[source];
    }
}
