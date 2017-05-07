/* 
 * Copyright (c) Stacks Contributors
 * 
 * This file is subject to the terms and conditions defined in
 * the LICENSE file, which is part of this source code package.
 */

namespace Slalom.Stacks.Services.EndPoints
{
    /// <summary>
    /// Checks the health of the service and returns nothing if everything is good.  If there is an issue with the system health then
    /// an exception is thrown.
    /// </summary>
    [EndPoint("_system/health")]
    public class CheckHealth : EndPoint
    {
        /// <summary>
        /// Receives the call to the endpoint.
        /// </summary>
        public override void Receive()
        {
        }
    }
}