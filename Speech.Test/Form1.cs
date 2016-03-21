using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Speech.Test
{
    public partial class Form1 : Form
    {
        private SpeechRecognitionEngine _recognizer;
        private SpeechSynthesizer _synthesizer;
        

        public Form1()
        {
            InitializeComponent();
        }

        private bool InitializeSpeechEngine()
        {
            _synthesizer = new SpeechSynthesizer();
            var knownVoices = _synthesizer.GetInstalledVoices().ToArray();
            var plVoice = knownVoices.SingleOrDefault(v => v.VoiceInfo.Culture.Name == "pl-PL");
            if (plVoice == null)
            {
                if (_synthesizer.Voice.Culture.Parent != null &&
                    _synthesizer.Voice.Culture.Parent.Name == "en")
                {
                    _synthesizer.Speak("Polish version of text-to-speech not found or activated.");
                }
                else
                {
                    PlayInvalidSpeechEngineFailureWav();
                }
                return false;
            }
            
            return true;
        }

        private bool InitializeSpeechRecognitionEngine()
        {
            var engines = System.Speech.Recognition.SpeechRecognitionEngine.InstalledRecognizers();
            if (engines.Count == 0)
            {
                // SpeechSDK:  https://www.microsoft.com/en-us/download/details.aspx?id=10121
                _synthesizer.Speak("Nie wykryto żadnych systemów rozpoznawania mowy.");
                return false;
            }

            var engine = engines.SingleOrDefault(r => r.Culture.Name == "pl-PL");
            if (engine == null)
            {
                // LanguagePacks: https://msdn.microsoft.com/en-us/library/hh378476(v=office.14).aspx
                _synthesizer.Speak("Nie wykryto systemu rozpoznawania mowy dla języka polskiego.");
                return false;
            }

            _recognizer = new SpeechRecognitionEngine(engine);
            _recognizer.SpeechRecognized += RecognizerOnSpeechRecognized;
            _recognizer.SetInputToDefaultAudioDevice();

            _recognizer.LoadGrammar(CreateExitGrammar());
            _recognizer.LoadGrammar(CreateWelcomeGrammar());

            return true;
        }

        private void RecognizerOnSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Grammar.Name == "exit")
            {
                if (e.Result.Text == "zamknij się")
                {
                    _synthesizer.Speak("Dobra, już! Zamykam się.");
                }
                else
                {
                    _synthesizer.Speak("Zamykam program!");
                }
                Application.Exit();
            }
            else if (e.Result.Grammar.Name == "welcome")
            {
                HelloWorld();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HelloWorld();
        }

        private void HelloWorld()
        {
            _synthesizer.Speak("A teraz komputerek przemawia do ciebie!");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!InitializeSpeechEngine())
            {
                PlayAppCloseWav();
                this.Close();
                Application.Exit();
                return;
            }
            if (!InitializeSpeechRecognitionEngine())
            {
                _synthesizer.Speak("Zamykam program.");
                this.Close();
                Application.Exit();
                return;
            }
            _recognizer?.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void PlayAppCloseWav()
        {
            // TODO: implement
        }

        private void PlayInvalidSpeechEngineFailureWav()
        {
            // TODO: implement
        }

        private Grammar CreateColorGrammar()
        {

            // Create a set of color choices.
            Choices colorChoice = new Choices(new string[] { "red", "green", "blue" });
            GrammarBuilder colorElement = new GrammarBuilder(colorChoice);

            // Create grammar builders for the two versions of the phrase.
            GrammarBuilder makePhrase = new GrammarBuilder("Make background");
            makePhrase.Append(colorElement);
            GrammarBuilder setPhrase = new GrammarBuilder("Set background to");
            setPhrase.Append(colorElement);

            // Create a Choices for the two alternative phrases, convert the Choices
            // to a GrammarBuilder, and construct the grammar from the result.
            Choices bothChoices = new Choices(new GrammarBuilder[] { makePhrase, setPhrase });
            Grammar grammar = new Grammar((GrammarBuilder)bothChoices);
            grammar.Name = "backgroundColor";
            return grammar;
        }


        private Grammar CreateExitGrammar()
        {
            Choices choices = new Choices( new [] {
                new GrammarBuilder("wyjdź") ,
                new GrammarBuilder("zamknij się"),
                new GrammarBuilder("zakończ")
            });
            Grammar grammar = new Grammar(choices)
            {
                Name = "exit"
            };
            return grammar;
        }

        private Grammar CreateWelcomeGrammar()
        {
            Choices choices = new Choices(new[] {
                new GrammarBuilder("witaj") ,
                new GrammarBuilder("heloł"),
                new GrammarBuilder("siemka")
            });
            Grammar grammar = new Grammar(choices)
            {
                Name = "welcome"
            };
            return grammar;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _recognizer?.RecognizeAsyncStop();
            _recognizer?.Dispose();

            _synthesizer?.Dispose();
        }
    }
}
