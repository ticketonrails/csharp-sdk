using System.Runtime.Serialization;

namespace Tor.Api
{
    [DataContract]
    public class Response
    {
        [DataMember(Name = "id")]
        public string TicketId { get; set; }

        [DataMember(Name = "ticket")]
        public string TicketNumber { get; set; }
    }
}