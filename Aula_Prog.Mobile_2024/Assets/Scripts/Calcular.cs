using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calcular : MonoBehaviour
{
    float nota1, nota2, media;

    void Start()
    {
        nota1 = 7.25f;
        nota2 = 2.5f;

        media = CalcularMedia(nota1, nota2);

        print("a media do aluno foi: " +  media);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float CalcularMedia(float nota1, float nota2)
    {
        return (nota1 + nota2) / 2;
    }
}
