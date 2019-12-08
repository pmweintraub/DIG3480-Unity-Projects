using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform startMarker;
    public Transform endMarker;

    public float speed = 1.0F;

    private float startTime;
    private float journeyLength;
    private bool faceRight = true;
    private Rigidbody2D rd2d;



    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        startTime = Time.time;
        journeyLength = Vector2.Distance(startMarker.position, endMarker.position);

    }

    void Flip()
    {
        faceRight = !faceRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector2.Lerp(startMarker.position, endMarker.position, Mathf.PingPong(fracJourney, 1));

        /**/
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EndPos"))
        {
            faceRight = false;
        }
        if (other.gameObject.CompareTag("StartPos"))
        {
            faceRight = false;
        }
    }

    private void FixedUpdate()
    {
        if(faceRight == false)
        {
            Flip();
        }
    }

}
