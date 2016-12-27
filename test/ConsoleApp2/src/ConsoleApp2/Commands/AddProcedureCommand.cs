using Slalom.Stacks.Messaging;

namespace Slalom.Stacks
{
    public class AddProcedureCommand : Command
    {
        public string Name { get; }

        public AddProcedureCommand(string name)
        {
            this.Name = name;
        }
    }
}