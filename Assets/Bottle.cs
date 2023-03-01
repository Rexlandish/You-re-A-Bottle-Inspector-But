using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{

    public BottleData bottleData;

    public Transform startPos;
    public Transform endPos;

    bool isMoving = false;
    bool isLost = false; // Is yet to be accepted / rejected

    float speed = 4f;
    float lerpPosition = 0f;


    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {

        sr = GetComponent<SpriteRenderer>();

        // Take data from bottleData
        sr.color = bottleData.color;
        transform.localScale = new Vector3(transform.lossyScale.x, bottleData.height, transform.lossyScale.z);
        SetMovementEnabled(true);
        transform.position = startPos.position + new Vector3(0f, bottleData.height, 0f);
        
    }

    // Update is called once per frame
    void Update()
    {

        if (lerpPosition >= 1f && !isLost)
        {
            Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(-4f, 0f), ForceMode2D.Impulse);
            rb.AddTorque(-1f, ForceMode2D.Impulse);
            Destroy(gameObject, 5f);
            isLost = true;

        }

        if (!isLost)
        {
            // Lerp from beginning to end
            transform.position = Vector3.Lerp(
                startPos.position + new Vector3(0f, bottleData.height/2, 0f),
                endPos.position + new Vector3(0f, bottleData.height/2, 0f), 
                lerpPosition
            );

            // Normalize Speed
            float distance = Vector3.Distance(startPos.position, endPos.position);

            // Movement
            if (isMoving) lerpPosition +=  speed * Time.deltaTime / distance;
        }

    }

    void SetIsLost(bool isLost_)
    {
        isLost = isLost_;
        if (isLost_) Destroy(GetComponent<BoxCollider2D>());
    }

    public void AcceptBottle()
    {
        SetIsLost(true);
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        sr.sortingOrder = 2;
        rb.gravityScale = 5f;
        rb.AddForce(new Vector2(10f, 30f));
    }
    public void RejectBottle()
    {
        SetIsLost(true);
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();        
        rb.gravityScale = 5f;
        rb.AddForce(new Vector3(-5f, 5f), ForceMode2D.Impulse);
        rb.AddTorque(-10f, ForceMode2D.Impulse);
    }

    public void SetMovementEnabled(bool isEnabled_)
    {
        isMoving = isEnabled_;
    }

    public void SetStartPos(Transform startPos_)
    {
        startPos = startPos_;
    }

    public void SetEndPos(Transform endPos_) 
    {
        endPos = endPos_;
    }
}
