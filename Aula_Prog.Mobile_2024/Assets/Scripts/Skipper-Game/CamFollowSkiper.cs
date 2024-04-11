using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowSkiper : MonoBehaviour
{
    public Transform skipper;
    Vector3 thisCamPos;
    float thisPosZ;
    // Start is called before the first frame update
    void Start()
    {
        thisPosZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        thisCamPos = skipper.position;
        thisCamPos.z = thisPosZ;
        thisCamPos.y += 2f;
        transform.position = thisCamPos;
    }
}
