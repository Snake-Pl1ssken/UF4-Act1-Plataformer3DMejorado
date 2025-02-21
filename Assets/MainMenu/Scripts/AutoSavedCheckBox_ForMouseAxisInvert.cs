using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class AutoSavedCheckBox_ForMouseAxisInvert : AutoSavedCheckBox
{
    [SerializeField] CinemachineInputAxisController inputAxisController;
    [SerializeField] Toggle flipX;
    [SerializeField] Toggle flipY;

    private void Start()
    {
        // Recuperar valores de PlayerPrefs
        bool isFlippedX = PlayerPrefs.GetInt("FlipXAxis", 0) == 1;
        bool isFlippedY = PlayerPrefs.GetInt("FlipYAxis", 0) == 1;

        // Asignar valores a los toggles
        flipX.isOn = isFlippedX;
        flipY.isOn = isFlippedY;

        // Aplicar inversión al iniciar
        FlipXAxis(isFlippedX);
        FlipYAxis(isFlippedY);

        // Agregar listeners para cambiar los valores cuando el usuario interactúe con los toggles
        flipX.onValueChanged.AddListener(FlipXAxis);
        flipY.onValueChanged.AddListener(FlipYAxis);
    }

    public void FlipXAxis(bool isFlipped)
    {
        foreach (var controller in inputAxisController.Controllers)
        {
            if (controller.Name == "Look Orbit X")
            {
                float originalGain = Mathf.Abs(controller.Input.Gain); // Asegurar que siempre partimos de un valor positivo
                controller.Input.Gain = isFlipped ? -originalGain : originalGain;
            }
        }

        // Guardar configuración en PlayerPrefs
        PlayerPrefs.SetInt("FlipXAxis", isFlipped ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void FlipYAxis(bool isFlipped)
    {
        foreach (var controller in inputAxisController.Controllers)
        {
            if (controller.Name == "Look Orbit Y")
            {
                float originalGain = Mathf.Abs(controller.Input.Gain); // Asegurar que siempre partimos de un valor positivo
                controller.Input.Gain = isFlipped ? -originalGain : originalGain;
            }
        }

        // Guardar configuración en PlayerPrefs
        PlayerPrefs.SetInt("FlipYAxis", isFlipped ? 1 : 0);
        PlayerPrefs.Save();
    }
}
