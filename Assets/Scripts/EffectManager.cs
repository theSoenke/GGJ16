﻿using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class EffectManager : MonoBehaviour
{
    public Camera playerCamera;

    private Fisheye _fisheye;
    private ColorCorrectionCurves _colorCorrection;


    void Awake()
    {
        _fisheye = playerCamera.gameObject.GetComponent<Fisheye>();
        _colorCorrection = playerCamera.gameObject.GetComponent<ColorCorrectionCurves>();
    }

    void Start()
    {
        SetFishEyeEffect(false);
        SetColorCorrection(1f);
    }

    public void SetColorCorrection(float saturation)
    {
        _colorCorrection.saturation = saturation;
    }

    public void SetFishEyeEffect(bool enable)
    {
        if (enable)
        {
            _fisheye.strengthX = 0.7f;
            _fisheye.strengthY = 0.7f;
        }
        else
        {
            _fisheye.enabled = false;
        }
    }
}
