public class MusicSoundSlider : SoundSlider
{
    protected override void OnSliderValueChanged(float newValue)
    {
        AudioManager.Instance.SetMusicVolume(newValue);
    }
}
