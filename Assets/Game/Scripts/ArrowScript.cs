using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private float curDir = 269;
    private int flag = -1;
    private float speed = 0.4f;
    private static ArrowScript _shared;

    private void Awake()
    {
        _shared = this;
    }

    public static float CurDir
    {
        get => CurDir;
    }

    // Update is called once per frame
    void Update()
    {
        if (curDir <= 180)
            flag = 1;
        if (curDir >= 360)
            flag = -1;
        curDir = curDir + (flag*speed);
        transform.eulerAngles = new Vector3 (0,0, curDir);
    }
}
