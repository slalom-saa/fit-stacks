namespace Slalom.Stacks.Utilities.NewId
{
    internal interface IWorkerIdProvider
    {
        byte[] GetWorkerId(int index);
    }
}