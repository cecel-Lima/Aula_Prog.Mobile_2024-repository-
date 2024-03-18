using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnTouch : MonoBehaviour
{
    public GameObject[] obj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
            touchPos.z = 0f;

            obj[i].transform.position = touchPos;

            //GameObject spw = Instantiate(obj, touchPos, Quaternion.identity);
            //Destroy(spw, 2f);
        }
    }
}
