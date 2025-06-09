using UnityEngine;
using UnityEngine.UI;

public class UISettingsPanelScreenManager : UIPanelScreenManager
{
    protected override void Awake()
    {
        base.Awake();

        volumeMusicSlider.value = 0.5f;
        volumeEffectsSlider.value = 0.5f;
        currentMusicVolume = volumeMusicSlider.value;
        currentEffectsVolume = volumeEffectsSlider.value;

        volumeMusicSlider.onValueChanged.AddListener(OnVolumeMusicChanged);
        volumeEffectsSlider.onValueChanged.AddListener(OnVolumeEffectsChanged);
    }

    [SerializeField] private Slider volumeMusicSlider;
    [SerializeField] private Slider volumeEffectsSlider;
    private float currentMusicVolume;
    private float currentEffectsVolume;


    private void OnVolumeMusicChanged(float newValue)
    {
        currentMusicVolume = newValue;
    }

    private void OnVolumeEffectsChanged(float newValue)
    {
        currentEffectsVolume = newValue;
    }

    public void Return()
    {
        HideUI();
    }
}
