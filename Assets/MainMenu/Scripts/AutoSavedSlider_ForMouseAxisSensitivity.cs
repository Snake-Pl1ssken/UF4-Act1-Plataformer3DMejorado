using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class AutoSavedSlider_ForMouseAxisSensitivity : AutoSavedSlider
{
    [SerializeField][Range(0.1f, 100f)] float sensivilityX;
    [SerializeField][Range(0.1f, 100f)] float sensivilityY;
    [SerializeField] CinemachineInputAxisController inputAxisController;
    [SerializeField] Slider MouseAxisX;
    [SerializeField] Slider MouseAxisY;

    private void Start()
    {
        sensivilityX = PlayerPrefs.GetFloat("SensivilityX", 10f);
        sensivilityY = PlayerPrefs.GetFloat("SensivilityY", 10f);

        MouseAxisX.value = sensivilityX;
        MouseAxisX.onValueChanged.AddListener(UpdateSensitivityX);
        MouseAxisY.value = sensivilityY;
        MouseAxisY.onValueChanged.AddListener(UpdateSensitivityY);

        foreach (var controller in inputAxisController.Controllers)
        {
            if (controller.Name == "Look Orbit X")
            {
                controller.Input.Gain = sensivilityX;
            }
            else if (controller.Name == "Look Orbit Y")
            {
                controller.Input.Gain = sensivilityY;
            }
        }
    }

    public override void InternalValueChanged(float value)
    {
        foreach (var controller in inputAxisController.Controllers)
        {
            if (controller.Name == "Look Orbit X")
            {
                controller.Input.Gain = sensivilityX;
            }
            else if (controller.Name == "Look Orbit Y")
            {
                controller.Input.Gain = sensivilityY;
            }
        }
    }

    public void UpdateSensitivityX(float value)
    {
        sensivilityX = value;
        foreach (var controller in inputAxisController.Controllers)
        {
            if (controller.Name == "Look Orbit X")
            {
                controller.Input.Gain = value;
            }
        }
        PlayerPrefs.SetFloat("SensivilityX", value);
        PlayerPrefs.Save();  
    }

    public void UpdateSensitivityY(float value)
    {
        sensivilityY = value;
        foreach (var controller in inputAxisController.Controllers)
        {
            if (controller.Name == "Look Orbit Y")
            {
                controller.Input.Gain = value;
            }
        }
        PlayerPrefs.SetFloat("SensivilityY", value);
        PlayerPrefs.Save();  
    }

}
