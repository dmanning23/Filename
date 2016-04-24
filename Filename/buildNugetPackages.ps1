nuget pack .\FilenameBuddy.nuspec -IncludeReferencedProjects -Prop Configuration=Release
nuget push *.nupkg