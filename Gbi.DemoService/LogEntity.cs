using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gbi.DemoService
{
    [Serializable]
    public struct LogEntity
    {
        //Location
        public readonly string MachineName;
        public readonly string AppDomainName;
        public readonly int ThreadID;
        public readonly string ThreadName;
        public readonly int ContextID;

        //Identity 
        public readonly string UserName;

        //Object info
        public readonly string AssemblyName;
        public readonly string TypeName;
        public readonly string MemberAccessed;
        public readonly string Date;
        public readonly string Time;

        //Exception 
        public readonly string ExceptionName;
        public readonly string ExceptionMessage;

        //Event
        public readonly string Event;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEntity"/> struct.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <param name="typeName">Name of the type.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="eventDescription">The event description.</param>
        public LogEntity(string assemblyName, string typeName, string methodName, string eventDescription) :
            this(assemblyName, typeName, methodName, "", "")
        {
            Event = eventDescription;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEntity"/> struct.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <param name="typeName">Name of the type.</param>
        /// <param name="methodName">Name of the method.</param>
        public LogEntity(string assemblyName, string typeName, string methodName) :
            this(assemblyName, methodName, typeName, null, null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEntity"/> struct.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <param name="typeName">Name of the type.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="exceptionName">Name of the exception.</param>
        /// <param name="exceptionMessage">The exception message.</param>
        public LogEntity(string assemblyName, string typeName, string methodName, string exceptionName, string exceptionMessage)
        {
            MachineName = Environment.MachineName;
            AppDomainName = AppDomain.CurrentDomain.FriendlyName;
            ThreadID = Thread.CurrentThread.GetHashCode();
            ThreadName = Thread.CurrentThread.Name;
            ContextID = Thread.CurrentContext.ContextID;
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                UserName = Thread.CurrentPrincipal.Identity.Name;
            }
            else
            {
                UserName = "Unauthenticated";
            }
            AssemblyName = assemblyName;
            TypeName = typeName;
            MemberAccessed = methodName;
            Date = DateTime.Now.ToShortDateString();
            Time = DateTime.Now.ToLongTimeString();
            ExceptionName = exceptionName;
            ExceptionMessage = exceptionMessage;
            Event = "";
        }
    }
}
