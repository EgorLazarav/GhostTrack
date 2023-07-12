using UnityEngine;

public class MasterSoundSlider : SoundSlider
{
    protected override void OnSliderValueChanged(float newValue)
    {
        AudioListener.volume = newValue;
    }
}
