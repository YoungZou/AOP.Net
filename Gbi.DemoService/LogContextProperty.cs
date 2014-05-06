using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Gbi.DemoService
{
    class LogContextProperty : IContextProperty, IContributeServerContextSink
    {
        /// <summary>
        /// The preference process
        /// </summary>
        private PreProcess preProcess = null;

        /// <summary>
        /// The post process
        /// </summary>
        private PostProcess postProcess = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogContextProperty"/> class.
        /// </summary>
        public LogContextProperty()
            : this(null, null)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogContextProperty"/> class.
        /// </summary>
        /// <param name="preProcess">The preference process.</param>
        /// <param name="postProcess">The post process.</param>
        public LogContextProperty(PreProcess preProcess, PostProcess postProcess)
        {
            this.preProcess = preProcess;
            this.postProcess = postProcess;
        }

        /// <summary>
        /// Gets the name of the property under which it will be added to the context.
        /// </summary>
        /// <value>The name.</value>
        /// <returns>The name of the property.</returns>
        ///   <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
        ///   </PermissionSet>
        public string Name
        {
            get
            {
                return "Log";
            }
        }

        /// <summary>
        /// Takes the first sink in the chain of sinks composed so far, and then chains its message sink in front of the chain already formed.
        /// </summary>
        /// <param name="nextSink">The chain of sinks composed so far.</param>
        /// <returns>The composite sink chain.</returns>
        public IMessageSink GetServerContextSink(IMessageSink nextSink)
        {
            return new LogSink(nextSink, this.preProcess, this.postProcess);
        }

        /// <summary>
        /// Determines whether [is new context ok] [the specified context].
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns><c>true</c> if [is new context ok] [the specified context]; otherwise, <c>false</c>.</returns>
        public bool IsNewContextOK(Context context)
        {
            LogContextProperty logContextProperty = null;
            logContextProperty = context.GetProperty("Log") as LogContextProperty;

            if (logContextProperty == null)
            {
                Debug.Assert(false);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Freezes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Freeze(Context context)
        { }
    }
}
