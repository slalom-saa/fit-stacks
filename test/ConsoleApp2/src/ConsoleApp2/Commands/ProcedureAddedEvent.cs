namespace Slalom.Stacks
{
    public class ProcedureAddedEvent
    {
        public string Name { get; }

        public ProcedureAddedEvent(string name)
        {
            this.Name = name;
        }
    }
}