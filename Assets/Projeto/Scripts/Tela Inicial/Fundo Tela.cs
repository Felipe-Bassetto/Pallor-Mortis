using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FundoTela : MonoBehaviour
{
    [Header("Variaveis")]
    [SerializeField] private float velocidade;
    [SerializeField] private Vector3 rePos;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localPosition += Vector3.left * velocidade * Time.deltaTime;

        if(gameObject.transform.localPosition.x <= -980f) gameObject.transform.localPosition = rePos;
    }
}
