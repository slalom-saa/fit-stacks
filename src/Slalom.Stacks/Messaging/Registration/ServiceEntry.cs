using System;

namespace Slalom.Stacks.Messaging.Registration
{
    public class Service
    {
        public string Path { get; set; }

        public string Type { get; set; }

        public string Input { get; set; }

        public string Output { get; set; }

        public Service(Type service, string rootPath)
        {
            this.Path = service.GetPath();
            this.RootPath = rootPath;
            this.Type = service.AssemblyQualifiedName;
            this.Input = service.GetRequestType().AssemblyQualifiedName;
            this.Output = service.GetResponseType()?.AssemblyQualifiedName;
        }

        public string RootPath { get; set; }
    }
}