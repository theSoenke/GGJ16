﻿using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class DarkenImage : MonoBehaviour
{

    private Material _material;

    [Range(-1, 1)]
    public float _darken;
    [Range(0, 2)]
    public float _contrast;

    void Awake()
    {
        _material = new Material(Shader.Find("Hidden/Darken"));
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(_darken == 0)
        {
            Graphics.Blit(source, destination);
        }
        _material.SetFloat("_Brightness", _darken);
        _material.SetFloat("_Contrast", _contrast);
        Graphics.Blit(source,  destination, _material);
    }

    }
