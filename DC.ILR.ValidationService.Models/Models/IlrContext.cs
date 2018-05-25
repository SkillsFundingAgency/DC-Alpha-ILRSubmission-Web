using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.ILR.ValidationService.Models
{
    public class IlrContext
    {
        public string Filename;
        public string ContainerReference;
        public Guid CorrelationId;
        public bool IsShredAndProcess;
    }
}
