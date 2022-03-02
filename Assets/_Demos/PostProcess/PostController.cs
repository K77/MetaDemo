using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostController : MonoBehaviour
{
    public Volume _volume;

    private Vignette _vignette;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0,0,200,200),"post"))
        {
            _volume.weight = 1 - _volume.weight;
        }
    }
}
