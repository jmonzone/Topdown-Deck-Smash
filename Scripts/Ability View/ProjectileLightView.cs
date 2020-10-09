using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ProjectileLightView : MonoBehaviour
{
    [SerializeField] private Light2D light2d;

    private void Update()
    {
        light2d.pointLightOuterRadius = transform.localScale.x * 2;
    }
}
