public class EffectsSoundSlider : SoundSlider
{
    protected override void OnSliderValueChanged(float newValue)
    {
        AudioManager.Instance.SetEffectsVolume(newValue);
    }
}