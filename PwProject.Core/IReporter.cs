using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PwProject.Core
{
    public interface IReporter
    {
        void PrintAnnotation(Annotation annotation);
    }
}
