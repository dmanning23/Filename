using NUnit.Framework;
using System;
using FilenameBuddy;
using System.IO;

namespace FilenameBuddyTests
{
	[TestFixture()]
	public class Test
	{
		/// <summary>
		/// get the current working directory
		/// </summary>
		/// <returns>The location.</returns>
		string progLocation()
		{
			return Directory.GetCurrentDirectory() + @"\";
		}

		[Test()]
		public void StaticConstructor()
		{
			//get teh program location
			Assert.AreEqual(progLocation(), Filename.ProgramLocation);
		}

		[Test()]
		public void DefaultConstructor()
		{
			//default constructor = no filename
			Filename dude = new Filename();
			Assert.IsTrue(string.IsNullOrEmpty(dude.File));
		}

		[Test()]
		public void Constructor()
		{
			//set the filename in teh constructor
			Filename dude = new Filename("test");
			Assert.AreEqual("test", dude.File);
		}

		[Test()]
		public void SetFilename()
		{
			//set the name and get it back
			Filename dude = new Filename();
			dude.File = "test";
			Assert.AreEqual("test", dude.File);
		}

		[Test()]
		public void SetRelFilename()
		{
			Filename dude = new Filename();
			dude.SetRelFilename("test");
			Assert.AreEqual(progLocation() + "test", dude.File);
		}

		[Test()]
		public void GetPath()
		{
			Filename dude = new Filename();
			dude.SetRelFilename("test");
			Assert.AreEqual(progLocation(), dude.GetPath());
		}

		[Test()]
		public void GetPathWithExt()
		{
			Filename dude = new Filename();
			dude.SetRelFilename("test.txt");
			Assert.AreEqual(progLocation(), dude.GetPath());
		}

		[Test()]
		public void GetPathWithSub()
		{
			Filename dude = new Filename();
			dude.SetRelFilename("test.txt");
			Assert.AreEqual(progLocation(), dude.GetPath());
		}
	}
}

