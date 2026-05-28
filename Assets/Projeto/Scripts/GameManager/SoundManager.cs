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
    private AudioSource audioSourceAlterar;

    [SerializeField] private float speedGradual;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (audioSourceAlterar != null)
        {
            if (aumentaVolume) audioSourceAlterar.volume += speedGradual * Time.deltaTime;

            if (audioSourceAlterar.volume >= 1)
            {
                audioSourceAlterar.volume = 1;
                aumentaVolume = false;
            }

            if (diminuirVolume) audioSourceAlterar.volume -= speedGradual * Time.deltaTime;

            if (audioSourceAlterar.volume <= 0)
            {
                audioSourceAlterar.volume = 0;
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
        audioSourceAlterar = sources[source];
    }

    public void DiminuirVolumeGradual(int source)
    {
        diminuirVolume = true;
        audioSourceAlterar = sources[source];
    }
}
