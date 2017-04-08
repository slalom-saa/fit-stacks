using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging.Registry;

namespace Slalom.Stacks.Documentation.Model
{
    public class DocumentElement
    {
        private ServiceRegistry _registry = new ServiceRegistry();

        private DocumentElement(params Assembly[] assemblies)
        {
            _registry.RegisterLocal(assemblies);
        }

        private void AddEndPoints()
        {
            foreach (var service in _registry.Hosts.SelectMany(e => e.Services))
            {
                foreach (var endPoint in service.EndPoints)
                {
                    this.EndPoints.Add(new EndPointElement
                    {
                        Name = service.Name,
                        Path = endPoint.Path,
                        Timeout = endPoint.Timeout.ToString()
                    });
                }
            }
        }

        public static DocumentElement Create(params Assembly[] assemblies)
        {
            var document = new DocumentElement(assemblies);

            document.AddEndPoints();

            return document;
        }

        public List<EndPointElement> EndPoints { get; set; } = new List<EndPointElement>();
    }
}
