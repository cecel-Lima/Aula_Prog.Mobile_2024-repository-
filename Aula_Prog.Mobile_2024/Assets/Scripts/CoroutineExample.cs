using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Countdown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Countdown()
    {
        int countdownTime = 3;

        while (countdownTime > 0)
        {
            Debug.Log("contagem regressiva: " +  countdownTime);
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        Debug.Log("GO!");
    }
}
