using System;

namespace Tor.Api
{
	public class Ticket
	{
		public string Email { get; set; }
		public string FromName { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
		public bool Html { get; set; }
		public DateTime Date { get; set; }
		public string[] Labels { get; set; }
		public string Attachment;
	}
}