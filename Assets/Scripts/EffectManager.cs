using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class EffectManager : MonoBehaviour
{
    public Camera playerCamera;

    public AnimationCurve _blurTransitionCurve;

    private Fisheye _fisheye;
    private ColorCorrectionCurves _colorCorrection;
    private Blur _blur;



    void Awake()
    {
        _fisheye = playerCamera.gameObject.GetComponent<Fisheye>();
        _colorCorrection = playerCamera.gameObject.GetComponent<ColorCorrectionCurves>();
        _blur = playerCamera.gameObject.GetComponent<Blur>();
    }

    void Start()
    {
        SetFishEyeEffect(false);
        SetColorCorrection(0.5f);
    }

    public void SetColorCorrection(float saturation)
    {
        _colorCorrection.saturation = saturation;
    }

    public void StartBlurVision()
    {
        StartCoroutine(BlurVision());
    }
    IEnumerator BlurVision()
    {
        float startTime = Time.time;
        while(Time.time - startTime < 3)
        {
            _blur.iterations = Mathf.RoundToInt( _blurTransitionCurve.Evaluate(Time.time - startTime));
            yield return null;
        }
        yield return null;
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
