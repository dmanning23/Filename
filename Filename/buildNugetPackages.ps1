nuget pack .\Filename.Android\FilenameBuddy.Android.nuspec -IncludeReferencedProjects -Prop Configuration=Release
nuget pack .\Filename.iOS\FilenameBuddy.iOS.nuspec -IncludeReferencedProjects -Prop Configuration=Release
nuget pack .\Filename.DesktopGL\FilenameBuddy.DesktopGL.nuspec -IncludeReferencedProjects -Prop Configuration=Release
nuget pack .\Filename.WindowsUniversal\FilenameBuddy.WindowsUniversal.nuspec -IncludeReferencedProjects -Prop Configuration=Release
nuget push *.nupkg