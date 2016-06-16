using System;
using System.Linq;
using System.Speech.Synthesis;
using IoDemo.WikiModel;

namespace IoDemo
{
    internal class WikiDumbVoiceReader
    {
        private readonly SpeechSynthesizer _synthesizer;

        public WikiDumbVoiceReader(SpeechSynthesizer synthesizer)
        {
            _synthesizer = synthesizer;
        }

        public void ReadModel(WikiPage model)
        {
            _synthesizer.Speak("Czytam artykuł z WikiPedii: " + model.Title);

            var intro = model.Sections.Values.SingleOrDefault(s => s.Id == 0);
            if (intro != null)
            {
                ReadIntro(intro);
                // TODO: jesli brak reakcji usera - dajemy spis (jak jest) a jak nie - kolejne sekcje - do końca lub STOP
            }

            // TODO: else - zaczynamy w takim razie od spisu - jeśli jest

            // TODO: else - jak nie ma ani wstępu ani spisu - jedziemy po kolei sekcje - według Id
        }

        private void ReadIntro(WikiSection intro)
        {
            if (intro == null) throw new ArgumentNullException(nameof(intro));

            foreach (var paragraph in intro.Paragraphs)
            {
                ReadParagraph(paragraph);
            }
        }

        private void ReadParagraph(Paragraph paragraph)
        {
            _synthesizer.Speak(paragraph.Content);
        }
    }
}