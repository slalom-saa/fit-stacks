/* 
 * Copyright (c) Stacks Contributors
 * 
 * This file is subject to the terms and conditions defined in
 * the LICENSE file, which is part of this source code package.
 */

using System.Linq;
using Slalom.Stacks.Services.Inventory;
using Slalom.Stacks.Validation;

namespace Slalom.Stacks.Services.EndPoints
{
    /// <summary>
    /// Gets all connected remote endpoints.
    /// </summary>
    [EndPoint("_system/endpoints/remote")]
    public class GetRemoteEndPoints : EndPoint
    {
        private readonly RemoteServiceInventory _inventory;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRemoteEndPoints" /> class.
        /// </summary>
        /// <param name="inventory">The current inventory.</param>
        public GetRemoteEndPoints(RemoteServiceInventory inventory)
        {
            Argument.NotNull(inventory, nameof(inventory));

            _inventory = inventory;
        }

        /// <inheritdoc />
        public override void Receive()
        {
            this.Respond(_inventory.EndPoints.Select(e => e.FullPath));
        }
    }
}