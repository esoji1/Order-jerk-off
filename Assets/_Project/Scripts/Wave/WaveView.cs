using TMPro;

namespace _Project.Wave
{
    public class WaveView
    {
        private TextMeshProUGUI _waveText;

        public WaveView(TextMeshProUGUI waveText) => _waveText = waveText;

        public void Show(int currentWave) => _waveText.text = currentWave.ToString();
    }
}
