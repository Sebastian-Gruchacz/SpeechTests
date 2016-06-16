namespace IoDemo
{
    internal interface IAsynchronousSynthesizer
    {
        void Play(string text);

        void Stop();

        void Pause();

        void Resume();
    }
}