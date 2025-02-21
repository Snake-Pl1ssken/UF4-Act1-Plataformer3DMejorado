using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AutoSavedCheckBox : MonoBehaviour
{
    public string prefKey;
    public bool defaultValue;

    private Toggle toggle;

    public virtual void InternalValueChanged(bool value)
    {

    }

    void Awake()
    {
        toggle = GetComponent<Toggle>();
        toggle.isOn = PlayerPrefs.GetInt(prefKey, defaultValue ? 1 : 0) == 1;
        toggle.onValueChanged.AddListener(onToggleValueChanged);
    }

    void Start()
    {
        InternalValueChanged(toggle.isOn);
    }

    void onToggleValueChanged(bool isOn)
    {

        PlayerPrefs.SetInt(prefKey, isOn ? 1 : 0);
        PlayerPrefs.Save();

        InternalValueChanged(isOn);
    }
}
