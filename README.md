# PrimeNumberGRPC
A GRPC implementation of Prime number validator server and client on .Net
## Prerequisties
* .Net Framework 
* make - if intend to use Makefile to build
## Server
The Prime Number Server folder contains the solution for GRPC server that is used to listen to incoming requests and validate prime numbers
### In Visual studio
* git clone https://github.com/uzairrajput7/PrimeNumberGRPC.git
* Open the .sln file
* Rebuild solution and run
### For Cli
* git clone https://github.com/uzairrajput7/PrimeNumberGRPC.git
* cd PrimeNumberServer
* dotnet clean
* dotnet build
* dotnet run
## Client
A simple console GPRC client written in .Net to send requests to Server for Prime number validation.
### In Visual studio
* git clone https://github.com/uzairrajput7/PrimeNumberGRPC.git
* Open the .sln file
* Rebuild solution and run
### For Cli
* git clone https://github.com/uzairrajput7/PrimeNumberGRPC.git
* cd PrimeNumberServer
* dotnet clean
* dotnet build
* dotnet run
### Using MakeFile
* Make sure 'make' is installed and in your Path variable
*  git clone https://github.com/uzairrajput7/PrimeNumberGRPC.git
*  make
*  It should build the binaries for PrimeNumberServer and Client under \bin\debug\net6.0 respectively.
