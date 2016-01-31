using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance;

    public Camera playerCamera;
    public AnimationCurve _blurTransitionCurve;
    public float colorSaturation = 1f;

    private Fisheye _fisheye;
    private ColorCorrectionCurves _colorSaturation;
    private BlurOptimized _blur;

    void Awake()
    {
        Instance = this;

        _fisheye = playerCamera.gameObject.GetComponent<Fisheye>();
        _colorSaturation = playerCamera.gameObject.GetComponent<ColorCorrectionCurves>();
        _blur = playerCamera.gameObject.GetComponent<BlurOptimized>();
    }

    void Start()
    {
        SetFishEyeEffect(false);
    }

    void Update()
    {
        _colorSaturation.saturation = colorSaturation;
    }

    public void SetSaturation(float saturation)
    {
        _colorSaturation.saturation = saturation;
    }

    public void StartBlurVision()
    {
        StartCoroutine(BlurVision());
    }
    IEnumerator BlurVision()
    {
        _blur.enabled = true;
        float startTime = Time.time;
        while (Time.time - startTime < 3)
        {
            _blur.blurSize = _blurTransitionCurve.Evaluate(Time.time - startTime);
            yield return null;
        }
        _blur.enabled = false;
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
