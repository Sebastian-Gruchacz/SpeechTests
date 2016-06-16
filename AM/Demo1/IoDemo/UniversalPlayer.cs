using System.Threading.Tasks;
using IoDemo.Model;

namespace IoDemo
{
    internal class UniversalPlayer
    {
        private readonly IAsynchronousSynthesizer _synthesizer;
        private readonly ICommandProvider _commandProvider;

        public UniversalPlayer(IAsynchronousSynthesizer synthesizer, ICommandProvider commandProvider)
        {
            _synthesizer = synthesizer;
            _commandProvider = commandProvider;
        }

        public Task PlayModelAsync(UniversalModel universalModel)
        {
            throw new System.NotImplementedException();
        }
    }
}