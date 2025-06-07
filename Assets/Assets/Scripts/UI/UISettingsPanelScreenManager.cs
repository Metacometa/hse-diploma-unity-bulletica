using UnityEngine;
using UnityEngine.UI;

public class UISettingsPanelScreenManager : UIPanelScreenManager
{
    protected override void Awake()
    {
        base.Awake();

        volumeSlider.value = 0.5f;
        currentVolume = volumeSlider.value;

        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    [SerializeField] private Slider volumeSlider;
    private float currentVolume;


    private void OnVolumeChanged(float newValue)
    {
        currentVolume = newValue;
    }

    public void Return()
    {
        HideUI();
    }
}
