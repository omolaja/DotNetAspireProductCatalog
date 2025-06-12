using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventMessaging.Events
{
   public class EventIntegration
    {
        public Guid EventId => Guid.NewGuid();

        public DateTime EventDate => DateTime.UtcNow;

        public string EventType  => GetType().AssemblyQualifiedName ?? string.Empty;
    }
}
