all : buildserver buildclient

buildserver: 
	cd PrimeNumberServer && dotnet clean && dotnet restore && dotnet build && cd ..

buildclient: 
	cd RevolvingGamesClient && dotnet clean && dotnet restore && dotnet build