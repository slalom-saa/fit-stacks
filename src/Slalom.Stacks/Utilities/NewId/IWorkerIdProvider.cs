namespace Slalom.Stacks.Utilities.NewId
{
    public interface IWorkerIdProvider
    {
        byte[] GetWorkerId(int index);
    }
}