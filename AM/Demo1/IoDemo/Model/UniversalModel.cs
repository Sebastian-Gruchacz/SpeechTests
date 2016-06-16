namespace IoDemo.Model
{
    // One-way linked graph

    internal class UniversalModel
    {
        public ModelNode EntryNode { get; set; }
    }

    internal class ModelNode
    {
        public string TextToSpeach { get; set; }

        public ModelNode DefaultNextNode { get; set; }

        public CommandedModelNode[] CommandedModelNodes { get; set; }
    }

    internal class CommandedModelNode
    {
        public Command Command { get; set; }

        public ModelNode Node { get; set; }
    }

    internal class Command
    {
        // TODO:...
    }
}