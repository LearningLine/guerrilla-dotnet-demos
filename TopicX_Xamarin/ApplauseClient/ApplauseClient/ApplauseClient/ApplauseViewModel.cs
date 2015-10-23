using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.AspNet.SignalR.Client;

namespace ApplauseClient
{
	public class ApplauseViewModel : BaseViewModel
	{

		public ICommand ApplaudCommand { get; set; }

		public int VoteCount
		{
			get { return voteCount; }
			set
			{
				if (value == voteCount) return;
				voteCount = value;
				OnPropertyChanged();
			}
		}

		private bool isApplauding;
		private int voteCount;
		private IHubProxy hubProxy;
		private string votingFor;

		public bool IsApplauding
		{
			get { return isApplauding; }
			set
			{
				if (value == isApplauding) return;
				isApplauding = value;
				OnPropertyChanged();
			}
		}

		public ApplauseViewModel()
		{
			ApplaudCommand = new DelegateCommand(OnApplaud, _ => true);

			Activate();
		}

		public async void Activate()
		{
			var connection = new HubConnection("http://gnetapplauseserver.azurewebsites.net");
			hubProxy = connection.CreateHubProxy("ApplauseHub");
			hubProxy.On<string>("getReadyFor", GetReadyFor);
			hubProxy.On<string>("startApplauding", StartApplauding);
			hubProxy.On<string>("stopApplauding", StopApplauding);
			await connection.Start();
		}

		private void StartApplauding(string name)
		{
			//Show the button
			VotingFor = name;
			IsApplauding = true;
		}

		private void StopApplauding(string name)
		{
			IsApplauding = false;
			VotingFor = "";
		}

		public string VotingFor
		{
			get { return votingFor; }
			set
			{
				if (value == votingFor) return;
				votingFor = value;
				OnPropertyChanged();
			}
		}

		private void GetReadyFor(string name)
		{
			//Show something
			VotingFor = name;
		}


		private void OnApplaud(object obj)
		{
			if (!IsApplauding)
				return;
			
			//Send the vote
			hubProxy.Invoke<int>("vote",votingFor);
			VoteCount++;
		}
	}
}
