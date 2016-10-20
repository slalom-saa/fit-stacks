namespace Authentication.ExecutionContext
{
    public class ExampleExecutionContext : Slalom.LeanStack.Messaging.ExecutionContext
    {
        public ExampleExecutionContext(string userName)
            : base(userName)
        {
        }
    }
}