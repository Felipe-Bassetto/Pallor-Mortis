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
        gameObject.transform.position += Vector3.left * velocidade * Time.deltaTime;

        if(gameObject.transform.position.x <= -950f) gameObject.transform.position = rePos;
    }
}
