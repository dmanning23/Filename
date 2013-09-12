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
		public void SetRelFilename1()
		{
			Filename dude = new Filename();
			dude.SetRelFilename(@"Content\Buttnuts\test.txt");
			Assert.AreEqual(progLocation() + @"Content\Buttnuts\test.txt", dude.File);
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

		[Test()]
		public void GetRelPath()
		{
			Filename dude = new Filename();
			dude.SetRelFilename(@"Content\Buttnuts\test.txt");
			Assert.AreEqual(@"Buttnuts\", dude.GetRelPath());
		}

		[Test()]
		public void GetRelPath1()
		{
			Filename dude = new Filename();
			dude.SetRelFilename(@"Content\Buttnuts\assnuts\test.txt");
			Assert.AreEqual(@"Buttnuts\assnuts\", dude.GetRelPath());
		}

		[Test()]
		public void GetRelPath2()
		{
			Filename dude = new Filename();
			dude.SetRelFilename(@"Buttnuts\assnuts\test.txt");
			//NOTICE HOW IT BREAKS IF THERE IS NO CONTENT DIRECTORY!!!
			Assert.AreEqual(@"", dude.GetRelPath());
		}

		[Test()]
		public void GetFilename()
		{
			Filename dude = new Filename();
			dude.SetRelFilename(@"Content\Buttnuts\assnuts\test.txt");
			Assert.AreEqual(@"test.txt", dude.GetFile());
		}

		[Test()]
		public void GetFilename1()
		{
			Filename dude = new Filename();
			dude.SetRelFilename(@"Content\Buttnuts\assnuts\test");
			Assert.AreEqual(@"test", dude.GetFile());
		}

		[Test()]
		public void GetFileExt()
		{
			Filename dude = new Filename();
			dude.SetRelFilename(@"Content\Buttnuts\assnuts\test.txt");
			Assert.AreEqual(@"txt", dude.GetFileExt());
		}

		[Test()]
		public void GetFileExt1()
		{
			Filename dude = new Filename();
			dude.SetRelFilename(@"Content\Buttnuts\assnuts\test");
			Assert.AreEqual(@"", dude.GetFileExt());
		}

		[Test()]
		public void GetFileNoExt()
		{
			Filename dude = new Filename();
			dude.SetRelFilename(@"Content\Buttnuts\assnuts\test.txt");
			Assert.AreEqual(@"test", dude.GetFileNoExt());
		}

		[Test()]
		public void GetFileNoExtBreakIt()
		{
			//NOTICE HOW THE FILE WITHOUT EXTENSION DOESNT WORK!!!
			Filename dude = new Filename();
			dude.SetRelFilename(@"Content\Buttnuts\assnuts\test");
			Assert.AreNotEqual(@"test", dude.GetFileNoExt());
		}

		[Test()]
		public void GetPathFileNoExt()
		{
			Filename dude = new Filename();
			string testFile = @"Content\Buttnuts\assnuts\test.txt";
			dude.SetRelFilename(testFile);

			Assert.AreEqual(progLocation() +  @"Content\Buttnuts\assnuts\test", dude.GetPathFileNoExt());
		}

		[Test()]
		public void GetRelPathFileNoExt()
		{
			Filename dude = new Filename();
			string testFile = @"Content\Buttnuts\assnuts\test.txt";
			dude.SetRelFilename(testFile);

			Assert.AreEqual(@"Buttnuts\assnuts\test", dude.GetRelPathFileNoExt());
		}

		[Test()]
		public void GetRelFilename()
		{
			Filename dude = new Filename();
			string testFile = @"Content\Buttnuts\assnuts\test.txt";
			dude.SetRelFilename(testFile);

			Assert.AreEqual(@"Buttnuts\assnuts\test.txt", dude.GetRelFilename());
		}

		[Test()]
		public void GetRelFilename1()
		{
			Filename dude = new Filename();
			string testFile = @"Content\test.txt";
			dude.SetRelFilename(testFile);

			Assert.AreEqual(@"test.txt", dude.GetRelFilename());
		}

		[Test()]
		public void GetRelFilename2()
		{
			Filename dude = new Filename();
			string testFile = @"test.txt";
			dude.SetRelFilename(testFile);

			Assert.AreEqual(@"test.txt", dude.GetRelFilename());
		}
	}
}

