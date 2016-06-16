using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using IoDemo.WikiModel;

namespace IoDemo
{
    class Program
    {
        private static SpeechSynthesizer _synthesizer;

        static void Main(string[] args)
        {
            if (!InitializeSpeechEngine())
            {
                return;
            }

            var sourceFileBuilder = new PathBuilder(@"WikiSample.xml");
            //Console.WriteLine(sourceFileBuilder.GetFullPath());
            //var reader = new SimpleFileReading(sourceFileBuilder.GetFullPath());

            Console.OutputEncoding = Encoding.UTF8;
            //Console.WriteLine(reader.ReadAll());

            //int a, b;
            //Console.WriteLine(a = b = 3);

            //PrintXmlSerializationDemo();
            WikiPageReader wrdr = new WikiPageReader();
            WikiPage model;

            using (var sr = new FileStream(sourceFileBuilder.GetFullPath(), FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                model = wrdr.ReadModel(sr);
               
                Console.WriteLine("Model parsed.");
            }

            // TODO: show available commands

            if (model != null)
            {
                //var voiceReader = new WikiDumbVoiceReader(_synthesizer);
                //voiceReader.ReadModel(model);

                var modelCommands = ReadWikiCommandsModel(@"Resources.WikiCommands.xml");
                var modelConverter = new WikiModelConverter(modelCommands);
                var universalModel = modelConverter.ToUniversalModel(model);

                var synthesizer = new MsTextToSpeechWrapper(_synthesizer);
                var commandProvider = new ConsoleCommandWrapper();
                var universalPlayer = new UniversalPlayer(synthesizer, commandProvider);
                var t2 = commandProvider.StartListenningAsync();
                var t1 = universalPlayer.PlayModelAsync(universalModel);

                Task.WaitAll(new Task[] {t1, t2});
            }
            
#if DEBUG
            Console.WriteLine();
            Console.WriteLine("Press ENTER");
            Console.ReadLine();
#endif
        }

        private static CommandsModel ReadWikiCommandsModel(string resourceName)
        {
            var assembly = typeof(Program).Assembly;
            var rc = typeof(Program).Namespace + "." + resourceName;

            var ser = new XmlSerializer(typeof(CommandsModel));
            using (Stream stream = assembly.GetManifestResourceStream(rc))
            {
                return ser.Deserialize(stream) as CommandsModel;
            }
        }

        private static bool InitializeSpeechEngine()
        {
            _synthesizer = new SpeechSynthesizer();
            var knownVoices = _synthesizer.GetInstalledVoices().ToArray();
            InstalledVoice plVoice = knownVoices.SingleOrDefault(v => v.VoiceInfo.Culture.Name == "pl-PL");
            if (plVoice == null)
            {
                var enVoice = TrySettingVoice(knownVoices.Where(v => v.Enabled && 
                    !v.VoiceInfo.Name.ToUpper().Contains("SAMPLE") &&
                        v.VoiceInfo.Culture.Parent != null && v.VoiceInfo.Culture.Parent.Name == "en"));

                if (enVoice != null)
                {
                    _synthesizer.Speak("Required Polish version of 'text to speech' was not found or activated in your system.");
                }
                else
                {
                    PlayInvalidSpeechEngineFailureWav();
                }
                return false;
            }

            return true;
        }

        private static InstalledVoice TrySettingVoice(IEnumerable<InstalledVoice> installedVoices)
        {
            foreach (var installedVoice in installedVoices)
            {
                try
                {
                    _synthesizer.SelectVoice(installedVoice.VoiceInfo.Name);
                    return installedVoice;
                }
                catch (Exception)
                {
                   // deliberatelly continue...
                }
            }

            return null;
        }

        private static void PlayInvalidSpeechEngineFailureWav()
        {
            // TODO: Cwiczenie - play "error message" audio file, synchronoulsy - wait till end

            throw new NotImplementedException();
        }

        private static void PrintXmlSerializationDemo()
        {
            var obj = new XmlSample(5, "Czarek");
            var ser = new XmlSerializer(typeof(XmlSample)); // obj.GetType()

            using (var memStr = new MemoryStream())
            {
                ser.Serialize(memStr, obj);

                memStr.Position = 0;
                var buff = memStr.GetBuffer();
                string buffContent = Encoding.UTF8.GetString(buff, 0, (int)memStr.Length);

                Console.WriteLine(buffContent);
            }
        }
    }
}
