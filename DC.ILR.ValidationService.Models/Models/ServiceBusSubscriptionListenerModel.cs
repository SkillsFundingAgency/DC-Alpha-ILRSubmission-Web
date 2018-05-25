﻿using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DC.ILR.ValidationService.Models
{
    public class ServiceBusSubscriptionListenerModel
    {
        public BrokeredMessage Message { get; set; }
        public CancellationToken token { get; set; }
    }
}
