namespace Slalom.Stacks.Documentation.Model
{
    public class DependencyElement
    {
        public DependencyElement(string v)
        {
            this.Path = v;
        }

        public string Path { get; set; }
    }
}