
# Add the new apt-get feed (may require sudo prefix)
sh -c 'echo "deb [arch=amd64] http://apt-mo.trafficmanager.net/repos/dotnet/ trusty main" > /etc/apt/sources.list.d/dotnetdev.list' 
apt-key adv --keyserver apt-mo.trafficmanager.net --recv-keys 417A0893
apt-get update
apt-get upgrade -Y


apt-get install node npm git -Y
npm install -g yo
npm install -g generator-aspnet

# Install .NET Core
apt-get install dotnet

# Add some starter code
cd ~/
mkdir dnx_src
cd dnx_src
dotnet init

# Run the app
dotnet restore
dotnet run