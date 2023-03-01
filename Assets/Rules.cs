using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Rules : MonoBehaviour
{

    List<string> rules;

    public static Rules instance;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {

    }



    public bool CheckIfRed(BottleData bottleData_)
    {
        if (bottleData_.color == Color.red)
        {
            return true;
        }
        return false;
    }
}
