#region Copyright 2010-2014 by Roger Knapp, Licensed under the Apache License, Version 2.0
/* Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
#endregion

namespace CSharpTest.Net.RpcLibrary
{
    using System.ComponentModel;
    using System.Diagnostics;

    public sealed class RpcException : Win32Exception
    {
        public RpcException()
        {
        }

        public RpcException(RpcError errorCode)
            : base(unchecked((int)errorCode))
        {
        }

        public RpcException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Returns the RPC Error as an enumeration.
        /// </summary>
        public RpcError RpcError
        {
            get { return (RpcError)this.NativeErrorCode; }
        }

        [DebuggerNonUserCode]
        internal static void Assert(int rawError)
        {
            Assert((RpcError)rawError);
        }

        /// <summary>
        /// Asserts that the argument is set to RpcError.RPC_S_OK or throws a new exception.
        /// </summary>
        [DebuggerNonUserCode]
        public static void Assert(RpcError errorCode)
        {
            if (errorCode != RpcError.RPC_S_OK)
            {
                var ex = new RpcException(errorCode);
                Log.Error("RpcError.{0} - {1}", errorCode, ex.Message);
                throw ex;
            }
        }
    }
}
