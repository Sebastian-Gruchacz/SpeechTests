using System.Speech.Synthesis;

namespace IoDemo
{
    internal class MsTextToSpeechWrapper : IAsynchronousSynthesizer
    {
        private readonly SpeechSynthesizer _synthesizer;

        public MsTextToSpeechWrapper(SpeechSynthesizer synthesizer)
        {
            _synthesizer = synthesizer;
        }

        public void Play(string text)
        {
            _synthesizer.SpeakAsync(text);
        }

        public void Stop()
        {
            _synthesizer.SpeakAsyncCancelAll();
        }

        public void Pause()
        {
            _synthesizer.Pause();
        }

        public void Resume()
        {
            _synthesizer.Resume();
        }
    }
}