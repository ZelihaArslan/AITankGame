using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ISense
{//tanım gereği soyuttur
    void Initialize();
    void UpdateSense();
}

public abstract class Sense : MonoBehaviour, ISense
{
    public abstract void Initialize();
    public abstract void UpdateSense();

    protected Aspect aspect;
    public float checkFreq = 5f;
    private float timeFromLastCheck;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if ((timeFromLastCheck += Time.deltaTime) > 1f / checkFreq)
        {
            UpdateSense();
            timeFromLastCheck = 0;
        }
    }
}
