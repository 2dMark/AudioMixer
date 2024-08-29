using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class VolumeControlSlider : MonoBehaviour
{
    [SerializeField] private AudioPanel _audioPanel;
    [SerializeField] private AudioPanel.MixerParameters _mixerGroupParameter;

    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(ChangeVolume);

        _audioPanel.Muted += DisableInteractivity;
        _audioPanel.Unmuted += EnableInteractivity;
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(ChangeVolume);

        _audioPanel.Muted -= DisableInteractivity;
        _audioPanel.Unmuted -= EnableInteractivity;
    }

    private void EnableInteractivity()
    {
        _slider.interactable = true;

        ChangeVolume(_slider.value);
    }

    private void DisableInteractivity() => _slider.interactable = false;

    private void ChangeVolume(float value)
        => _audioPanel.SetVolume(value, _mixerGroupParameter);
}