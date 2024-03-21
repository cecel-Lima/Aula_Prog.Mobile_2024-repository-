using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdSpeedByDrag : MonoBehaviour
{
    //Main Caracter Script

    Touch finger;
    Vector3 beganPos;
    Vector3 fingerPos;
    public int speedMarks = 0;
    [SerializeField]
    float speed;
    bool incremented = false;
    Rigidbody2D rb;
    [SerializeField]
    bool touchGrass;
    [SerializeField]
    float pVStrenght;
    float stayTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
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

            if (finger.phase == TouchPhase.Stationary)
            {
                stayTime += Time.deltaTime;
                if (stayTime > 0.1f)
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
    }

    void DirectionAndSpeed()
    {
        Vector3 pos = transform.position;
        pos.x += speed * speedMarks * Time.deltaTime;
        if (touchGrass) { transform.position = pos; }
    }

    void DirectionInput()
    {
        if (beganPos.x < fingerPos.x && !incremented)
        {
            incremented = true;
            speedMarks++;
            if (speedMarks > 3) { speedMarks = 3; }
            if (speedMarks >= -3 && speedMarks < 0) {  speedMarks = 0; }
        }

        if(beganPos.x > fingerPos.x && !incremented)
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
            if (speedMarks == 0) { rb.velocity = Vector2.zero; }
        }
    }

    void PoleVault()
    {
        if (touchGrass)
        {
            touchGrass = false;
            rb.AddForce(new Vector3(speedMarks, 1f, 0f) * pVStrenght);
        }
    }
}
