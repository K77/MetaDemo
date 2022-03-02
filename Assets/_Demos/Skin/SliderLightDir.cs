using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderLightDir : MonoBehaviour
{
    public UnityEngine.UI.Slider _Slider;
    public Transform _ligthTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        _Slider.onValueChanged.AddListener((value) =>
        {
            _ligthTransform.eulerAngles =
                new Vector3(_ligthTransform.eulerAngles.x, value*360f, _ligthTransform.eulerAngles.z);
        });
    }

    void onValueChange(float value)
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
