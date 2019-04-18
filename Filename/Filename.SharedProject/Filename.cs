using System;
using System.Linq;
using System.Collections;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace FilenameBuddy
{
	/// <summary>
	/// This is a class for manipulating filenames
	/// This is used for MonoGame, where all resource files are put in a folder called "Content"
	/// </summary>
	public class Filename
	{
		#region Fields

		/// <summary>
		/// The full path and filename
		/// </summary>
		private string _filename;

		#endregion //Fields

		#region Properties

		/// <summary>
		/// The location of the program, used to get relative paths
		/// </summary>
		public static string ProgramLocation { get; private set; }

		/// <summary>
		/// get or set the filename member variable
		/// </summary>
		public string File
		{
			get { return _filename; }
			set
			{
				_filename = ReplaceSlashes(value);
				HasFilename = !string.IsNullOrEmpty(value);
			}
		}

		public bool HasFilename { get; private set; }

		#endregion //Properties

		#region Methods

		public static string ReplaceSlashes(string initialString)
		{
			if (!string.IsNullOrEmpty(initialString))
			{
#if !WINDOWS
				return initialString.Replace('\\', '/');
#else
				return initialString.Replace('/', '\\');
#endif
			}
			else
			{
				return string.Empty;
			}
		}

		static Filename()
		{
			//Get the current working directory
			SetCurrentDirectory();
		}

		/// <summary>
		/// construct a blank filename
		/// </summary>
		public Filename()
		{
			HasFilename = false;
		}

		/// <summary>
		/// Set the cwd to the program location
		/// </summary>
		static public void SetCurrentDirectory()
		{
#if WINDOWS
			ProgramLocation = Directory.GetCurrentDirectory() + @"\";
#endif
		}

		/// <summary>
		/// Set the current directory of the application.  
		/// This will set the program location to the folder right before "Content"
		/// </summary>
		/// <param name="strCurrentDirectory">The current directory of the application.</param>
		static public void SetCurrentDirectory(string currentDirectory)
		{
			//tokenize teh string
			string[] pathinfo = currentDirectory.Split(new Char[] { '/', '\\' });

			//find the content folder
			StringBuilder progBuilder = new StringBuilder();
			for (int i = 0; i < pathinfo.Length; i++)
			{
				//stop before we hit the content folder
				if (pathinfo[i] == "Content")
				{
					break;
				}

#if ANDROID || __IOS__
				progBuilder.AppendFormat(@"{0}/", pathinfo[i]);
#else
				progBuilder.AppendFormat(@"{0}\", pathinfo[i]);
#endif
			}

			ProgramLocation = progBuilder.ToString();
		}

		/// <summary>
		/// construct a filename from a string that is an absolute filename
		/// </summary>
		/// <param name="relFilename">the string to use as an absolute filename</param>
		public Filename(string relFilename)
		{
			SetRelFilename(relFilename);
		}

		/// <summary>
		/// copy constructor
		/// </summary>
		/// <param name="strFilename"></param>
		public Filename(Filename filename)
		{
			if (null != filename)
			{
				File = filename._filename;
			}
		}

		/// <summary>
		/// Construct this filename as a location relative to another filename
		/// </summary>
		/// <param name="currentLocation"></param>
		/// <param name="relativeFilename"></param>
		public Filename(Filename currentLocation, string relativeFilename)
			: this($"{currentLocation.GetRelPath()}{relativeFilename}")
		{
		}

		/// <summary>
		/// set this filename from a relative path & file
		/// </summary>
		/// <param name="relFilename"></param>
		public void SetRelFilename(string relFilename)
		{
			//take the program location and append the filename to the end
			StringBuilder fileBuilder = new StringBuilder();
			fileBuilder.Append(ProgramLocation);
#if ANDROID || __IOS__
			fileBuilder.Append(@"Content/");
#else
			fileBuilder.Append(@"Content\");
#endif
			fileBuilder.Append(relFilename);
			File = fileBuilder.ToString();
			HasFilename = true;
		}

		/// <summary>
		/// Get the path from a filename
		/// </summary>
		/// <returns>The path to a file with a ' at the end</returns>
		public string GetPath()
		{
			StringBuilder totalPath = new StringBuilder();
			if (!String.IsNullOrEmpty(_filename))
			{
				//tokenize teh string
				string[] pathinfo = _filename.Split(new Char[] { '/', '\\' });

				//put the path back together
				for (int i = 0; i < pathinfo.Length - 1; i++)
				{
#if ANDROID || __IOS__
					totalPath.AppendFormat(@"{0}/", pathinfo[i]);
#else
					totalPath.AppendFormat(@"{0}\", pathinfo[i]);
#endif
				}
			}

			return totalPath.ToString();
		}

		/// <summary>
		/// Get teh relative path, which is everything after "Content"
		/// </summary>
		/// <returns>the path to the file, starting at but excluding the "Content" directory</returns>
		public string GetRelPath()
		{
			if (String.IsNullOrEmpty(_filename))
			{
				return "";
			}

			//tokenize teh string
			string[] pathinfo = GetPath().Split(new Char[] { '/', '\\' });

			//find the content folder
			int contentFolderIndex = 1;
			while (contentFolderIndex < pathinfo.Length)
			{
				//skip over the content folder itself
				if (pathinfo[contentFolderIndex - 1] == "Content")
				{
					break;
				}

				contentFolderIndex++;
			}

			//put the path back together
			StringBuilder relativePath = new StringBuilder();
			while (contentFolderIndex < pathinfo.Length)
			{
				if (!String.IsNullOrEmpty(pathinfo[contentFolderIndex]))
				{
#if ANDROID || __IOS__
					relativePath.AppendFormat(@"{0}/", pathinfo[contentFolderIndex]);
#else
					relativePath.AppendFormat(@"{0}\", pathinfo[contentFolderIndex]);
#endif

				}
				contentFolderIndex++;
			}

			//return that whole thing we constructed
			return relativePath.ToString();
		}

		/// <summary>
		/// Get only the filename 
		/// </summary>
		/// <returns>Get the filename with no path info</returns>
		public string GetFile()
		{
			return Path.GetFileName(_filename);
		}

		/// <summary>
		/// Get the file entension of a filename
		/// </summary>
		/// <returns>file extension (no '.')</returns>
		public string GetFileExt()
		{
			return Path.GetExtension(_filename);
		}

		/// <summary>
		/// Get only the filename, no path or extension
		/// NOTE: this will not work correctly if the file doesnt have an extension!!!
		/// </summary>
		/// <returns>the filename with no path info or extension</returns>
		public string GetFileNoExt()
		{
			return Path.GetFileNameWithoutExtension(_filename);
		}

		/// <summary>
		/// Get the path and filename, but no extension
		/// </summary>
		/// <returns>the path\filename (no extension)</returns>
		public string GetPathFileNoExt()
		{
			return GetPath() + GetFileNoExt();
		}

		public string GetRelPathFileNoExt()
		{
			return GetRelPath() + GetFileNoExt();
		}

		/// <summary>
		/// Get the filename with path info after the "Content" folder
		/// </summary>
		/// <returns>teh path and filename info</returns>
		public string GetRelFilename()
		{
			return GetRelPath() + GetFile();
		}

		public void SetFilenameRelativeToPath(Filename currentLocation, string relativeFilename)
		{
#if ANDROID
			File = $"{currentLocation.GetPath()}{relativeFilename}";
#else
			var uri1 = new Uri($"{currentLocation.GetPath()}{relativeFilename}");
			File = uri1.PathAndQuery.ToString();
#endif
		}

		public string GetFilenameRelativeToPath(Filename path)
		{
			var uri1 = new Uri(path.File);
			var uri2 = new Uri(File);
			var result = uri1.MakeRelativeUri(uri2).ToString();
			return ReplaceSlashes(result);
		}

		/// <summary>
		/// override the ToString function to get the whole path + filename
		/// </summary>
		/// <returns>the whole path and filename</returns>
		public override string ToString()
		{
			return _filename;
		}

		public bool Compare(Filename inst)
		{
			return File == inst.File;
		}

		/// <summary>
		/// Get a list of all the files in the content folder
		/// </summary>
		/// <returns></returns>
		public static IEnumerable<Filename> ContentFiles(string folder = "", string extension = "")
		{
			if (string.IsNullOrEmpty(extension))
			{
				return AllContentFiles(folder);
			}
			else
			{
				return ContentFilesOfType(folder, extension);
			}
		}

		/// <summary>
		/// Get a list of all the files in the content folder
		/// </summary>
		/// <returns></returns>
		public static IEnumerable<Filename> AllContentFiles(string folder)
		{
			string filter = GetFilterLocation(folder);
			var entries = Directory.GetFileSystemEntries(filter, "*", SearchOption.AllDirectories);
			return entries.Select(x => new Filename() { File = x });
		}

		/// <summary>
		/// Get a list of the files in the content folder of a certain type
		/// </summary>
		/// <returns></returns>
		public static IEnumerable<Filename> ContentFilesOfType(string folder, string extension)
		{
			string filter = GetFilterLocation(folder);
			var entries = Directory.GetFileSystemEntries(filter, "*", SearchOption.AllDirectories);
			return entries.Select(x => new Filename() { File = x }).Where(x => x.GetFileExt() == extension);
		}

		private static string GetFilterLocation(string folder)
		{
			if (string.IsNullOrEmpty(folder))
			{
				return $"{Filename.ProgramLocation}Content";
			}
			else
			{
#if ANDROID || __IOS__
				return $@"{Filename.ProgramLocation}Content/{folder}";
#else
				return $@"{Filename.ProgramLocation}Content\{folder}";
#endif
			}
		}

#endregion //Methods
	}
}