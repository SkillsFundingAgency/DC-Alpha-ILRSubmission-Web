using System;

namespace DCT.ILR.ValidationService.Models.Models
{
    public class IlrContext
    {
        public string Filename { get; set; }

        public string ContainerReference { get; set; }

        public Guid CorrelationId { get; set; }

        public bool IsShredAndProcess { get; set; }
    }
}
