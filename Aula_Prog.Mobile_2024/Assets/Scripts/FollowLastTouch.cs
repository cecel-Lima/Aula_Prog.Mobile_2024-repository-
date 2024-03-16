using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLastTouch : MonoBehaviour
{
    Touch tou;
    Vector3 lastTou;
    Vector3 direction;
    [SerializeField]
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            tou = Input.GetTouch(0);
            lastTou = Camera.main.ScreenToWorldPoint(tou.position);
            lastTou.z = 0f;
        }
        direction = lastTou - transform.position;
        transform.Translate(direction * Time.deltaTime * speed);
    }
}
