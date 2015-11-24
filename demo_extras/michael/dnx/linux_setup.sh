
# Add the new apt-get feed (may require sudo prefix)
echo ""
read -p "Add the new apt-get feed for dotnet aptitude packages... [enter]"
sh -c 'echo "deb [arch=amd64] http://apt-mo.trafficmanager.net/repos/dotnet/ trusty main" > /etc/apt/sources.list.d/dotnetdev.list' 
apt-key adv --keyserver apt-mo.trafficmanager.net --recv-keys 417A0893 
echo ""

read -p "Let's update the server to the latest packages... [enter]"
apt-get update
apt-get upgrade -Y

# echo ""
# read -p "Next install Node, NPM, and Yoman"
# apt-get install node npm git -Y
# npm install -g yo
# npm install -g generator-aspnet

# Install .NET Core
echo ""
read -p "Install .NET Core: apt-get install dotnet ... [enter]"
apt-get install dotnet

# Add some starter code
echo ""
read -p "Build some starter console app code ... [enter]"
cd ~/
mkdir dnx_src
cd dnx_src
dotnet init

# Run the app
echo ""
read -p "Download the runtime via NuGet ... [enter]"
dotnet restore

echo ""
read -p "Time to run the app! [enter]"
dotnet run
