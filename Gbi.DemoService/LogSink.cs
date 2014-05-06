using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Gbi.DemoService
{
    /// <summary>
    /// Delegate PreProcess
    /// </summary>
    /// <param name="entity">The entity.</param>
    public delegate void PreProcess(LogEntity entity);

    /// <summary>
    /// Delegate PostProcess
    /// </summary>
    /// <param name="entity">The entity.</param>
    public delegate void PostProcess(LogEntity entity);

    /// <summary>
    /// Class LogSink.
    /// </summary>
    class LogSink : IMessageSink
    {
        /// <summary>
        /// The preference process
        /// </summary>
        private PreProcess PreProcess = null;

        /// <summary>
        /// The post process
        /// </summary>
        private PostProcess PostProcess = null;

        /// <summary>
        /// The message next sink
        /// </summary>
        protected IMessageSink MessageNextSink;

        public LogSink(IMessageSink nextSink)
            : this(nextSink, DefaultProcess, DefaultProcess)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogSink"/> class.
        /// </summary>
        /// <param name="nextSink">The next sink.</param>
        /// <param name="option">The option.</param>
        public LogSink(IMessageSink nextSink, PreProcess preProcess, PostProcess postProcess)
        {
            this.MessageNextSink = nextSink;
            this.PreProcess = preProcess == null ? DefaultProcess : preProcess;
            this.PostProcess = postProcess == null ? DefaultProcess : postProcess;
        }

        /// <summary>
        /// Gets the next message sink in the sink chain.
        /// </summary>
        /// <value>The next sink.</value>
        /// <returns>The next message sink in the sink chain.</returns>
        ///   <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
        ///   </PermissionSet>
        public IMessageSink NextSink
        {
            get
            {
                return this.MessageNextSink;
            }
        }

        /// <summary>
        /// Asynchronously processes the given message.
        /// </summary>
        /// <param name="msg">The message to process.</param>
        /// <param name="replySink">The reply sink for the reply message.</param>
        /// <returns>Returns an <see cref="T:System.Runtime.Remoting.Messaging.IMessageCtrl" /> interface that provides a way to control asynchronous messages after they have been dispatched.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Synchronously processes the given message.
        /// </summary>
        /// <param name="msg">The message to process.</param>
        /// <returns>A reply message in response to the request.</returns>
        public IMessage SyncProcessMessage(IMessage msg)
        {
            #region basic assembly information

            IMethodMessage methodMessage = msg as IMethodMessage;
            string assemblyName = Util.GetAssemblyName(methodMessage);
            string typeName = Util.GetTypeName(methodMessage);
            string methodName = Util.GetMethodName(methodMessage);

            #endregion

            #region Preprocess method

            this.PreProcess(new LogEntity(assemblyName, typeName, methodName));

            #endregion

            // proxy execute target method
            IMethodReturnMessage returnedMessage = MessageNextSink.SyncProcessMessage(msg) as IMethodReturnMessage;

            string exceptionName = Util.GetExceptionName(returnedMessage);
            string exceptionMessage = Util.GetExceptionMessage(returnedMessage);

            #region Postprocess method

            this.PostProcess(new LogEntity(assemblyName, typeName, methodName, exceptionName, exceptionMessage));

            #endregion

            return returnedMessage;
        }

        /// <summary>
        /// Defaults the process.
        /// </summary>
        /// <param name="logEntity">The log entity.</param>
        private static void DefaultProcess(LogEntity logEntity)
        { }
    }
}
