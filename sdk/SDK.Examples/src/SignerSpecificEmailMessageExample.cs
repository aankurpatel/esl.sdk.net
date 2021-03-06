using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
	public class SignerSpecificEmailMessageExample
	{
		public static string apiToken = "YOUR TOKEN HERE";
		public static string baseUrl = "ENVIRONMENT URL HERE";

		public static void Main (string[] args)
		{
			// Create new esl client with api token and base url
			EslClient client = new EslClient (apiToken, baseUrl);
			FileInfo file = new FileInfo (Directory.GetCurrentDirectory() + "/src/document.pdf");

			DocumentPackage package = PackageBuilder.NewPackageNamed ("C# Package " + DateTime.Now)
					.DescribedAs ("This is a new package")
					.WithSigner(SignerBuilder.NewSignerWithEmail("john.smith@email.com")
					            .WithFirstName("John")
					            .WithLastName("Smith")
					            .WithEmailMessage("Hi John, could you sign this asap please?"))
					.WithDocument(DocumentBuilder.NewDocumentNamed("My Document")
					              .FromFile(file.FullName)
					              .WithSignature(SignatureBuilder.SignatureFor("john.smith@email.com")
					              		.OnPage(0)
					               		.AtPosition(500, 100))
					              .WithSignature (SignatureBuilder.InitialsFor("john.smith@email.com")
					                	.OnPage (0)
					                	.AtPosition (500, 200))
					              .WithSignature(SignatureBuilder.CaptureFor ("john.smith@email.com")
					               		.OnPage (0)
					               		.AtPosition (500, 300)))
					.Build ();

			PackageId id = client.CreatePackage (package);

			client.SendPackage(id);

			Console.WriteLine ("Package {0} was sent", id.Id);
		}
	}
}