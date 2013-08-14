using System;
using System.IO;

namespace Filename
{
	/// <summary>
	/// This is a class for manipulating filenames
	/// </summary>
	public class FilenameBuddy
	{
		#region Members

		/// <summary>
		/// The location of the program, used to get relative paths
		/// </summary>
		private static string g_ProgramLocation;

		/// <summary>
		/// The full path and filename
		/// </summary>
		private string m_strFilename;

		#endregion

		#region Properties

		/// <summary>
		/// get or set the filename member variable
		/// </summary>
		public string Filename
		{
			get { return m_strFilename; }
			set { m_strFilename = value; }
		}

		#endregion //Properties

		#region Methods

		static FilenameBuddy()
		{
#if WINDOWS
			//Get the current working directory
			g_ProgramLocation = Directory.GetCurrentDirectory() + @"\";
#else
			//xbox doesnt support current working directory
			g_ProgramLocation = "";
#endif
		}

		/// <summary>
		/// construct a blank filename
		/// </summary>
		public FilenameBuddy()
		{
		}

		/// <summary>
		/// Set the current directory of the application.  
		/// This will set the program location to the folder right before "Resources"
		/// </summary>
		/// <param name="strCurrentDirectory">The current directory of the application.</param>
		static public void SetCurrentDirectory(string strCurrentDirectory)
		{
			//tokenize teh string
			string[] pathinfo = strCurrentDirectory.Split(new Char[] { '/', '\\' });

			//find the content folder
			g_ProgramLocation = "";
			for (int i = 0; i < pathinfo.Length; i++)
			{
				if (pathinfo[i].ToLower() == "resources")
				{
					break;
				}
				else
				{
					g_ProgramLocation += pathinfo[i] + @"\";
				}
			}
		}

		/// <summary>
		/// construct a filename from a string that is an absolute filename
		/// </summary>
		/// <param name="strFileName">the string to use as an absolute filename</param>
		public FilenameBuddy(string strFilename)
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
			m_strFilename = g_ProgramLocation + strRelFilename;
		}

		/// <summary>
		/// Get the path from a filename
		/// </summary>
		/// <returns>The path to a file with a '\' at the end</returns>
		public string GetPath()
		{
			string strTotalPath = "";

			if (!String.IsNullOrEmpty(m_strFilename))
			{
				//tokenize teh string
				string[] pathinfo = m_strFilename.Split(new Char[] { '/', '\\' });

				//put the path back together
				for (int i = 0; i < pathinfo.Length - 1; i++)
				{
					strTotalPath += pathinfo[i] + @"\";
				}
			}

			return strTotalPath;
		}

		/// <summary>
		/// Get teh relative path, which is everything after "Resources"
		/// </summary>
		/// <returns>the path to the file, starting at and including the "resources" directory</returns>
		public string GetRelPath()
		{
			if (!String.IsNullOrEmpty(m_strFilename))
			{
				//tokenize teh string
				string[] pathinfo = GetPath().Split(new Char[] { '/', '\\' });

				//find the content folder
				int iContentFolderIndex = 0;
				while (iContentFolderIndex < pathinfo.Length)
				{
					if (pathinfo[iContentFolderIndex].ToLower() == "resources")
					{
						break;
					}
					else
					{
						iContentFolderIndex++;
					}
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
			else
			{
				return "";
			}
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