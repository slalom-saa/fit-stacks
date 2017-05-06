using System.Collections.Generic;
using System.Linq;

namespace Slalom.Stacks.Services.Inventory
{
    /// <summary>
    /// Maintains an inventory of remote endpoints and updates paths when necessary.
    /// </summary>
    public class RemoteEndPointInventory
    {
        private readonly List<RemoteEndPoint> _endpoints = new List<RemoteEndPoint>();

        /// <summary>
        /// Gets the remote endpoints.
        /// </summary>
        /// <value>The remote endpoints.</value>
        public IEnumerable<RemoteEndPoint> EndPoints => _endpoints.AsEnumerable();

        /// <summary>
        /// Adds the end points if they do not exist or updates the full path if they do.
        /// </summary>
        /// <param name="endPoints">The remote endpoints to add.</param>
        public void AddEndPoints(params RemoteEndPoint[] endPoints)
        {
            foreach (var endPoint in endPoints)
            {
                var current = this.EndPoints.FirstOrDefault(e => e.Path == endPoint.Path);
                if (current == null)
                {
                    _endpoints.Add(endPoint);
                }
                else
                {
                    current.FullPath = endPoint.FullPath;
                }
            }
        }
    }
}