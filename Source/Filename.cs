﻿using System;
using System.IO;
using System.Text;

namespace FilenameBuddy
{
	/// <summary>
	/// This is a class for manipulating filenames
	/// This is used for MonoGame, where all resource files are put in a folder called "Content"
	/// </summary>
	public class Filename
	{
		#region Members

		/// <summary>
		/// The full path and filename
		/// </summary>
		private string m_strFilename;

		#endregion

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
			get { return m_strFilename; }
			set { m_strFilename = value; }
		}

		#endregion //Properties

		#region Methods

		static Filename()
		{
#if WINDOWS
			//Get the current working directory
			ProgramLocation = Directory.GetCurrentDirectory() + @"\";
#else
			//xbox doesnt support current working directory
			ProgramLocation = "";
#endif
		}

		/// <summary>
		/// construct a blank filename
		/// </summary>
		public Filename()
		{
		}

		/// <summary>
		/// Set the current directory of the application.  
		/// This will set the program location to the folder right before "Content"
		/// </summary>
		/// <param name="strCurrentDirectory">The current directory of the application.</param>
		static public void SetCurrentDirectory(string strCurrentDirectory)
		{
			//tokenize teh string
			string[] pathinfo = strCurrentDirectory.Split(new Char[] { '/', '\\' });

			//find the content folder
			StringBuilder progBuilder = new StringBuilder();
			for (int i = 0; i < pathinfo.Length; i++)
			{
				progBuilder.AppendFormat(@"{0}\", pathinfo[i]);

				//stop after we hit the content folder
				if (pathinfo[i] == "Content")
				{
					break;
				}
			}

			ProgramLocation = progBuilder.ToString();
		}

		/// <summary>
		/// construct a filename from a string that is an absolute filename
		/// </summary>
		/// <param name="strFileName">the string to use as an absolute filename</param>
		public Filename(string strFilename)
		{
			m_strFilename = strFilename;
		}

		/// <summary>
		/// set this filename from a relative path & file
		/// </summary>
		/// <param name="strRelFilename"></param>
		public void SetRelFilename(string strRelFilename)
		{
			//take the program location and append the filename to the end
			StringBuilder fileBuilder = new StringBuilder();
			fileBuilder.Append(ProgramLocation);
			fileBuilder.Append(@"Content\");
			fileBuilder.Append(strRelFilename);
			m_strFilename = fileBuilder.ToString();
		}

		/// <summary>
		/// Get the path from a filename
		/// </summary>
		/// <returns>The path to a file with a ' at the end</returns>
		public string GetPath()
		{
			StringBuilder strTotalPath = new StringBuilder();

			if (!String.IsNullOrEmpty(m_strFilename))
			{
				//tokenize teh string
				string[] pathinfo = m_strFilename.Split(new Char[] { '/', '\\' });

				//put the path back together
				for (int i = 0; i < pathinfo.Length - 1; i++)
				{
					strTotalPath.AppendFormat(@"{0}\", pathinfo[i]);
				}
			}

			return strTotalPath.ToString();
		}

		/// <summary>
		/// Get teh relative path, which is everything after "Content"
		/// </summary>
		/// <returns>the path to the file, starting at but excluding the "Content" directory</returns>
		public string GetRelPath()
		{
			if (String.IsNullOrEmpty(m_strFilename))
			{
					return "";
			}

			//tokenize teh string
			string[] pathinfo = GetPath().Split(new Char[] { '/', '\\' });

			//find the content folder
			int iContentFolderIndex = 1;
			while (iContentFolderIndex < pathinfo.Length)
			{
				//skip over the content folder itself
				if (pathinfo[iContentFolderIndex - 1] == "Content")
				{
					break;
				}

				iContentFolderIndex++;
			}

			//put the path back together
			string strRelativePath = "";
			while (iContentFolderIndex < pathinfo.Length)
			{
				if (0 != pathinfo[iContentFolderIndex].Length)
				{
					strRelativePath += pathinfo[iContentFolderIndex] + @"\";
				}
				iContentFolderIndex++;
			}

			//return that whole thing we constructed
			return strRelativePath;
		}

		/// <summary>
		/// Get only the filename 
		/// </summary>
		/// <returns>Get the filename with no path info</returns>
		public string GetFile()
		{
			if (!String.IsNullOrEmpty(m_strFilename))
			{
				//tokenize teh string
				string[] pathinfo = m_strFilename.Split(new Char[] { '/', '\\' });

				//return the last item
				return ((pathinfo.Length > 0) ? pathinfo[pathinfo.Length - 1] : "");
			}
			else
			{
				return "";
			}
		}

		/// <summary>
		/// Get the file entension of a filename
		/// </summary>
		/// <returns>file extension (no '.')</returns>
		public string GetFileExt()
		{
			if (!String.IsNullOrEmpty(m_strFilename))
			{
				//tokenize teh string
				string[] pathinfo = m_strFilename.Split(new Char[] { '.' });

				//return the last item
				return ((pathinfo.Length == 2) ? pathinfo[pathinfo.Length - 1] : "");
			}
			else
			{
				return "";
			}
		}

		/// <summary>
		/// Get only the filename, no path or extension
		/// NOTE: this will not work correctly if the file doesnt have an extension!!!
		/// </summary>
		/// <returns>the filename with no path info or extension</returns>
		public string GetFileNoExt()
		{
			if (!String.IsNullOrEmpty(m_strFilename))
			{
				//tokenize teh string
				string[] pathinfo = m_strFilename.Split(new Char[] { '/', '\\', '.' });

				//return the second to last item
				return ((pathinfo.Length > 1) ? pathinfo[pathinfo.Length - 2] : "");
			}
			else
			{
				return "";
			}
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

		/// <summary>
		/// override the ToString function to get the whole path + filename
		/// </summary>
		/// <returns>the whole path and filename</returns>
		public override string ToString()
		{
			return m_strFilename;
		}

		#endregion
	}
}