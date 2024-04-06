using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AdSpeedByDrag : MonoBehaviour
{
    //Main Caracter Script

    Touch finger;
    Vector2 beganPos;
    Vector2 fingerPos;
    public int speedMarks = 0;
    int launchMark;
    [SerializeField]
    float speed;
    bool incremented = false;
    Rigidbody2D rb;
    bool touchGrass;
    [SerializeField]
    float pVStrenght;
    float stayTime = 0f;
    [SerializeField]
    private float nessStayTime;
    public TextMeshProUGUI textSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            finger = Input.GetTouch(0);

            if (finger.phase == TouchPhase.Began )
            {
                beganPos = finger.position;
            }
            fingerPos = finger.position;

            DirectionInput();

            if (finger.phase == TouchPhase.Stationary)
            {
                stayTime += Time.deltaTime;
                if (stayTime > nessStayTime)
                {
                    PoleVault();
                }

            }
            else
            {
                stayTime = 0f;
            }
        }
        else
        {
            beganPos = fingerPos;
            DirectionInput();
        }
        DirectionAndSpeed();
        textSpeed.text = "Speed: " + speedMarks.ToString();
    }

    void DirectionAndSpeed()
    {
        Vector3 pos = transform.position;
        if (touchGrass) 
        {
            pos.x += speed * speedMarks * Time.deltaTime;
            transform.position = pos;
        }
        else
        {
            pos.x += speed * launchMark * Time.deltaTime;
            transform.position = pos;
        }
    }

    void DirectionInput()
    {
        //swipe finger right
        if (beganPos.x < fingerPos.x && !incremented && touchGrass)
        {
            incremented = true;
            speedMarks++;
            if (speedMarks > 3) { speedMarks = 3; }
            if (speedMarks >= -3 && speedMarks < 0) {  speedMarks = 0; }
        }

        //sripe finger left
        if(beganPos.x > fingerPos.x && !incremented && touchGrass)
        {
            incremented = true;
            speedMarks--;
            if (speedMarks < -3) { speedMarks = -3; }
            if (speedMarks <= 3 && speedMarks > 0) { speedMarks = 0; }
        }

        if (incremented && (finger.phase == TouchPhase.Ended || finger.phase == TouchPhase.Canceled))
        {
            incremented = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Surface"))
        {
            touchGrass = true;
            rb.velocity = Vector2.zero;
        }
    }

    void PoleVault()
    {
        if (touchGrass)
        {
            touchGrass = false;
            launchMark = speedMarks;
            rb.AddForce(new Vector2( 0f, 1f) * pVStrenght);
        }
    }
}
