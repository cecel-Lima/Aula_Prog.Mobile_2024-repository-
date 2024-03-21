using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdSpeedByDrag : MonoBehaviour
{
    Touch finger;
    Vector3 beganPos;
    Vector3 fingerPos;
    [SerializeField]
    int speedMarks = 0;
    [SerializeField]
    float speed;
    bool incremented = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            finger = Input.GetTouch(0);
            if (finger.phase == TouchPhase.Began )
            {
                beganPos = Camera.main.ScreenToWorldPoint(finger.position);
            }
            fingerPos = Camera.main.ScreenToWorldPoint(finger.position);

            DirectionInput();
        }
        else
        {
            beganPos = fingerPos;
            DirectionInput();
        }
        DirectionAndSpeed();
    }

    void DirectionAndSpeed()
    {
        Vector3 pos = transform.position;
        pos.x += speed * speedMarks * Time.deltaTime;
    }

    void DirectionInput()
    {
        if (beganPos.x < fingerPos.x && !incremented)
        {
            incremented = true;
            speedMarks++;
            if (speedMarks > 3) { speedMarks = 3; }
        }

        if(beganPos.x > fingerPos.x && !incremented)
        {
            incremented = true;
            speedMarks--;
            if (speedMarks < -3) { speedMarks = -3; }
        }

        if (incremented && (finger.phase == TouchPhase.Ended || finger.phase == TouchPhase.Canceled))
        {
            incremented = false;
        }
    }
}
