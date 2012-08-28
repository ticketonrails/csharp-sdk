using System;
using System.Runtime.Serialization;

namespace Tor.Api
{
    [DataContract]
    public class Ticket
    {
        [DataMember(Name = "email")]
        public string Email { get; set; }
        [DataMember(Name = "from_name")]
        public string FromName { get; set; }

        [DataMember(Name = "subject")]
        public string Subject { get; set; }

        [DataMember(Name = "body")]
        public string Body { get; set; }

        [DataMember(Name = "html")]
        public bool Html { get; set; }

        public DateTime Date { get; set; }
        private DateTime Epoch
        {
            get
            {
                return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            }
        }
        long _epochDate = 0;
        [DataMember(Name = "date")]
        public long EpochDate
        {
            get
            {
                _epochDate = Convert.ToInt64((Date - Epoch).TotalSeconds);
                return _epochDate;
            }
            set
            {
                _epochDate = value;
            }
        }

        [DataMember(Name = "labels")]
        public string[] Labels { get; set; }
        public string Attachment;
    }
}