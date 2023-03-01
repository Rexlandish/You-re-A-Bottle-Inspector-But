using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BottleManager : MonoBehaviour
{

    public GameObject bottlePrefab;

    public Transform startPos;
    public Transform endPos;

    public UnityEvent checks;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartBottleProcess());
        //checks  Rules.instance.CheckIfRed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartBottleProcess()
    {
        while (true)
        {
            GameObject spawnedBottlePrefab = Instantiate(bottlePrefab);
            Bottle spawnedBottle = spawnedBottlePrefab.GetComponent<Bottle>();

            // Randomise bottle data

            BottleData tempBottleData = ScriptableObject.CreateInstance<BottleData>();
            //tempBottleData.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
            tempBottleData.color = new List<Color> { Color.red, Color.blue } [UnityEngine.Random.Range(0, 1)];
            tempBottleData.height = UnityEngine.Random.Range(1f, 3f);

            spawnedBottle.bottleData = tempBottleData;
            spawnedBottle.startPos = startPos;
            spawnedBottle.endPos = endPos;


            spawnedBottlePrefab.transform.position = startPos.position;
            spawnedBottle.SetMovementEnabled(true);
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.3f, 0.6f));
        }
    }
}
