using UnityEngine;

public class Oscillator : MonoBehaviour
{
    public double frequency = 440.0; // The frequency in Hertz if the tone that the oscillator will produce.
    private double increment; // The amount of distance the wave will be moving in ech frame.
    private double phase; // Actual location on the wave.
    private double sampling_frequency = 48000.0; // The frequency unity's audio engine runs by default.

    public float gain; // The power or volume of the oscillator.

    void OnAudioFilterRead(float[] data, int channels)
    {
        increment = frequency * 2.0 * Mathf.PI / sampling_frequency;

        for (int i = 0; i < data.Length; i += channels)
        {
            phase += increment;
            data[i] = (float)(gain * Mathf.Sin((float)phase));

            if (channels == 2)
            {
                data[i + 1] = data[i];
            }
        }
    }
}