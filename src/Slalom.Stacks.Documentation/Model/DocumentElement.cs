using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Slalom.Stacks.Messaging.Registry;
using Slalom.Stacks.Reflection;
using Slalom.Stacks.TestKit;

namespace Slalom.Stacks.Documentation.Model
{
    public class DocumentElement
    {
        private readonly Assembly[] _assemblies;
        private readonly ServiceRegistry _registry = new ServiceRegistry();

        private DocumentElement(params Assembly[] assemblies)
        {
            _assemblies = assemblies;
            _registry.RegisterLocal(assemblies);
        }

        private void AddEndPoints()
        {
            var types = _assemblies.SelectMany(e => e.SafelyGetTypes()).ToList();
            foreach (var service in _registry.Hosts.SelectMany(e => e.Services))
            {
                var tests = types.Where(e => e.GetAllAttributes<TestSubjectAttribute>().FirstOrDefault()?.Type == service.ServiceType).ToList();

                foreach (var endPoint in service.EndPoints)
                {
                    this.EndPoints.Add(new EndPointElement(service.Name, endPoint, tests));
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
