using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using static UnityEngine.Rendering.DebugUI;

public class AutoSavedSlider_ForContrast : AutoSavedSlider
{
    [SerializeField] Volume globalVolume;
    private ColorAdjustments colorAdjustments;
    private float minValue = -40;
    private float maxValue = 40;


    public override void InternalValueChanged(float value)
    {
        float interpolatedValue = Mathf.Lerp(minValue, maxValue, value);

        globalVolume.profile.TryGet(out colorAdjustments);
        colorAdjustments.contrast.value = interpolatedValue;
    }
}



