using System;

namespace Tor.Sample
{
	class MainClass
	{
		const string API_KEY = "YOUR API KEY";
		const string PROJECT_DOMAIN = "YOUR PROJECT DOMAIN";
		
		public static void Main (string[] args)
		{
			var api = new Tor.Api.Client(API_KEY, PROJECT_DOMAIN);
			var response = api.NewTicket(new Tor.Api.Ticket(){
				Email = "your@email.address",
				FromName = "John Doe",
				Subject = "lorem ipsum...",
				Body = "lorem ipsum...",
				Html = false,
				Date = DateTime.Now,
				Labels = new string[] {"label 1", "label 2"},
				Attachment = "attachment.txt"
			});
			Console.WriteLine("ticket {0}, id {1}", response.TicketNumber, response.TicketId);
		}
	}
}
