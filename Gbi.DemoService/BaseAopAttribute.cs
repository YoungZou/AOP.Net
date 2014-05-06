using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Gbi.DemoService
{
    public class BaseAopAttribute : ContextAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAopAttribute"/> class.
        /// </summary>
        public BaseAopAttribute()
            : base("LogAttribute")
        { }

        /// <summary>
        /// Adds the current context property to the given message.
        /// </summary>
        /// <param name="ctorMsg">The <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" /> to which to add the context property.</param>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
        ///   </PermissionSet>
        public override void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
        {
            IContextProperty logProperty = new LogContextProperty(this.PreProcess, this.PostProcess);
            ctorMsg.ContextProperties.Add(logProperty);
        }

        /// <summary>
        /// Returns a Boolean value indicating whether the context parameter meets the context attribute's requirements.
        /// </summary>
        /// <param name="ctx">The context in which to check.</param>
        /// <param name="ctorMsg">The <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" /> to which to add the context property.</param>
        /// <returns>true if the passed in context is okay; otherwise, false.</returns>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
        ///   </PermissionSet>
        public override bool IsContextOK(Context ctx, IConstructionCallMessage ctorMsg)
        {
            return false;
        }

        /// <summary>
        /// Preferences the process.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void PreProcess(LogEntity entity) { }

        /// <summary>
        /// Posts the process.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void PostProcess(LogEntity entity) { }
    }
}
