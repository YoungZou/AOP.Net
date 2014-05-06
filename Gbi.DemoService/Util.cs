using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Gbi.DemoService
{
    /// <summary>
    /// Class Util.
    /// </summary>
    static class Util
    {
        /// <summary>
        /// Gets the name of the assembly.
        /// </summary>
        /// <param name="methodMessage">The method message.</param>
        /// <returns>System.String.</returns>
        public static string GetAssemblyName(IMethodMessage methodMessage)
        {
            string fullTypeName = null;

            if (methodMessage != null)
            {
                fullTypeName = methodMessage.TypeName;
                string[] arr = fullTypeName.Split(new Char[] { ',', ',' });
                return arr[1];
            }

            return fullTypeName;
        }

        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        /// <param name="methodMessage">The method message.</param>
        /// <returns>System.String.</returns>
        public static string GetTypeName(IMethodMessage methodMessage)
        {
            string fullTypeName = null;

            if (methodMessage != null)
            {
                fullTypeName = methodMessage.TypeName;
                string[] arr = fullTypeName.Split(new Char[] { ',', ',' });
                return arr[0];
            }

            return fullTypeName;
        }

        /// <summary>
        /// Gets the name of the exception.
        /// </summary>
        /// <param name="returnedMessage">The returned message.</param>
        /// <returns>System.String.</returns>
        public static string GetExceptionName(IMethodReturnMessage returnedMessage)
        {
            if (returnedMessage != null && returnedMessage.Exception != null)
            {
                return returnedMessage.Exception.GetType().ToString();
            }
            return null;
        }
        /// <summary>
        /// Gets the exception message.
        /// </summary>
        /// <param name="returnedMessage">The returned message.</param>
        /// <returns>System.String.</returns>
        public static string GetExceptionMessage(IMethodReturnMessage returnedMessage)
        {
            if (returnedMessage != null && returnedMessage.Exception != null)
            {
                return returnedMessage.Exception.Message;
            }
            return null;
        }

        /// <summary>
        /// Gets the name of the method.
        /// </summary>
        /// <param name="methodMessage">The method message.</param>
        /// <returns>System.String.</returns>
        public static string GetMethodName(IMethodMessage methodMessage)
        {
            if (methodMessage != null)
            {
                string methodName = methodMessage.MethodName;
                switch (methodName)
                {
                    case ".ctor":
                        {
                            return "Constructor";
                        }
                    case "FieldGetter":
                    case "FieldSetter":
                        {
                            IMethodCallMessage methodCallMessage = (IMethodCallMessage)methodMessage;
                            return (string)methodCallMessage.InArgs[1];
                        }
                }
                if (methodName.EndsWith("Item"))
                {
                    return methodName;
                }

                if (methodName.StartsWith("get_") || methodName.StartsWith("set_"))
                {
                    return methodName.Substring(4);
                }
                if (methodName.StartsWith("add_"))
                {
                    return methodName.Substring(4) + "+=";
                }
                if (methodName.StartsWith("remove_"))
                {
                    return methodName.Substring(7) + "-=";
                }
                return methodName;
            }

            return null;
        }
    }
}
