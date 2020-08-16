using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Bogus;
using MailKit.Net.Pop3;
using MailKit.Security;
using NUnit.Framework;

namespace TestDataGeneration
{
	public class TestData
	{
		
		[SetUp]
		public void Setup()
		{
		}

		public static List<string> Mails()
		{
			string messages = null;
			string subject = null;
			List<string> MailList = new List<string>();
			using (var client = new Pop3Client())
			{
				client.Connect("pop.gmail.com", 995, SecureSocketOptions.SslOnConnect);
				// client.AuthenticationMechanisms.Remove("XOAUTH2");
				client.Authenticate("jatestuser1@gmail.com", "justanswer@123");
				for (int i = 0; i < client.Count; i++)
				{
					var message = client.GetMessage(i);
					Console.WriteLine("Subject: {0}", message.Subject);
				}
				client.Disconnect(true);
			}

			return MailList;
		}

		[Test]
		public void ReadEmail()
		{
			var mails = Mails();
			Console.WriteLine("Subject: "+mails[0]);
		}

		[Test]
		public void GetTestData()
		{
			var person = new Person();
			Console.WriteLine($"FirstName: {person.FirstName}");
			Console.WriteLine($"LastName: {person.LastName}");
			Console.WriteLine($"PhoneNo: {person.Phone}");
			Console.WriteLine($"Street: {person.Address.Street}");
			Console.WriteLine($"City: {person.Address.City}");
			Console.WriteLine($"State: {person.Address.State}");
			Console.WriteLine($"Zipcode: {person.Address.ZipCode}");
		}
	}
}