using Gbi.DemoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbi.Test
{
    public class LogAopAttribute : BaseAopAttribute
    {
        public override void PreProcess(LogEntity entity)
        {
            string test = "this is preprocess";
        }

        public override void PostProcess(LogEntity entity)
        {
            string test = "this is postprocess";
        }
    }
}
