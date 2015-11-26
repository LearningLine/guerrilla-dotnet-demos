using System.Linq;
using JulMar.Windows.Mvvm;
using PickupLines.Data;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System;

namespace PickupLines.ViewModels
{
    public class MainViewModel : SimpleViewModel
    {
        private static Random RNG = new Random();
        private string _pickupLine;
        private string _response;
        private bool _badLine;
        private bool _goodLine;

        public string PickupLine
        {
            get { return _pickupLine; }
            set { _pickupLine = value; OnPropertyChanged(() => PickupLine); }
        }

        public string Response
        {
            get { return _response; }
            set { _response = value; OnPropertyChanged(() => Response); }
        }

        public bool BadLine
        {
            get { return _badLine; }
            set { _badLine = value; OnPropertyChanged(() => BadLine); }
        }

        public bool GoodLine
        {
            get { return _goodLine; }
            set { _goodLine = value; OnPropertyChanged(() => GoodLine); }
        }

        public void GetGoodLine()
        {
            PickupLine = CheesePickupLines.GetPickupLine();
        }

        public async void TryLine()
        {
            var response = PickupResponses.TryLine(PickupLine);

            SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
            var installedVoices = speechSynthesizer.GetInstalledVoices().Where(v => v.Enabled == true).ToList();
            if (installedVoices.Count > 0)
            {
                speechSynthesizer.SelectVoice(installedVoices[RNG.Next(installedVoices.Count)].VoiceInfo.Name);
                speechSynthesizer.SetOutputToDefaultAudioDevice();
                speechSynthesizer.SpeakAsync(response.Item1);
            }

            Response = response.Item1;
            GoodLine = (response.Item2 == true);
            BadLine = (response.Item2 == false);

            await Task.Delay(3000);
            GoodLine = BadLine = false;
        }
    }
}
