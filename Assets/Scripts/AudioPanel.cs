using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioPanel : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Button _muteButton;

    private bool _isMute = false;
    private int _mixerParametersAmount = Enum.GetNames(typeof(MixerParameters)).Length;

    private const float ValueMultiplier = 20f;
    private const float MinAuidoMixerValue = -80f;

    public event Action Muted;
    public event Action Unmuted;

    public enum MixerParameters
    {
        MasterVolume,
        MusicVolume,
        SoundsVolume,
    }

    private void OnEnable()
    {
        _muteButton.onClick.AddListener(ChangeMuteMode);
    }

    private void OnDisable()
    {
        _muteButton.onClick.RemoveListener(ChangeMuteMode);
    }

    public void SetVolume(float value, MixerParameters parameter)
    {
        if (value == 0)
            value = MinAuidoMixerValue;
        else
            value = Mathf.Log10(value) * ValueMultiplier;

        _audioMixer.SetFloat(parameter.ToString(), value);
    }

    private void ChangeMuteMode()
    {
        _isMute = !_isMute;

        if (_isMute)
            Mute();
        else
            Unmute();
    }

    private void Unmute() => Unmuted?.Invoke();

    private void Mute()
    {
        Muted?.Invoke();

        for (int i = 0; i < _mixerParametersAmount; i++)
            SetVolume(0f, (MixerParameters)i);
    }
}