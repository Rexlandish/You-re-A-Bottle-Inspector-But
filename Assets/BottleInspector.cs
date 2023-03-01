using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleInspector : MonoBehaviour
{

    public Color acceptColor;
    public Color rejectColor;
    public Color targetColor;
    public List<string> currentRules;

    public BottleJudgement bottleJudgement = BottleJudgement.NONE;

    SpriteRenderer sr;
    BoxCollider2D bc;

    public enum BottleJudgement
    {
        NONE,
        ACCEPT,
        REJECT
    }

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0)) // If accept
        {
            RequestAccept(); // Green line

            Collider2D[] collidingBottles = GetCollidingBottles();
            bottleJudgement = BottleJudgement.ACCEPT;

            // Get bottle data to compare
            Bottle currentBottle = null;

            if (collidingBottles.Length != 0) currentBottle = collidingBottles[0].gameObject.GetComponentInParent<Bottle>();

            currentBottle.AcceptBottle();
            ExecuteChecks(currentBottle.bottleData);
        }
        else if (Input.GetMouseButtonDown(1)) // If Reject
        {
            RequestReject(); // Red line

            Collider2D[] collidingBottles = GetCollidingBottles();
            bottleJudgement = BottleJudgement.REJECT;

            // Get bottle data to compare
            Bottle currentBottle = null;

            if (collidingBottles.Length != 0) currentBottle = collidingBottles[0].gameObject.GetComponentInParent<Bottle>(); 
            
            currentBottle.RejectBottle();
            ExecuteChecks(currentBottle.bottleData);
        }

        

        // Lerp alpha always
        sr.color = new Color(
            Mathf.Lerp(sr.color.r, targetColor.r, 10f * Time.deltaTime),
            Mathf.Lerp(sr.color.g, targetColor.g, 10f * Time.deltaTime),
            Mathf.Lerp(sr.color.b, targetColor.b, 10f * Time.deltaTime),
            Mathf.Lerp(sr.color.a, targetColor.a, 10f * Time.deltaTime)
        );
    }

    Collider2D[] GetCollidingBottles()
    {
        Collider2D[] currentlyCollidingBottles = Physics2D.OverlapBoxAll(transform.position, transform.localScale, LayerMask.NameToLayer("Bottle"));
        return currentlyCollidingBottles;
    }
    
    void RequestAccept()
    {
        sr.color = acceptColor;
    }

    void RequestReject()
    {
        sr.color = rejectColor;
    }

    bool ExecuteChecks(BottleData bottleData_)
    {
        if (currentRules[0] == "red")
        {
            return Rules.instance.CheckIfRed(bottleData_);
        }

        return true;
    }


}
