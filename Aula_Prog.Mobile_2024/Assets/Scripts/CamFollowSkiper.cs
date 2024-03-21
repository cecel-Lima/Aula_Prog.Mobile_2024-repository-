using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowSkiper : MonoBehaviour
{
    public Transform skipper;
    Vector3 thisCamPos;
    float thisPosY;
    // Start is called before the first frame update
    void Start()
    {
        thisPosY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        thisCamPos = skipper.position;
        thisCamPos.y = thisPosY;
        thisCamPos.z = -10f;
        transform.position = thisCamPos;
    }
}
