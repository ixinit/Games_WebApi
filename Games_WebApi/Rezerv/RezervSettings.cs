using System.IO;

namespace Games_WebApi.Rezerv
{
    public class RezervSettings
    {
        private string configFileName = "rezerv.cfg";
        private int maxRezervs;
        private int timerToCreate;

        public int MaxRezervs { get => maxRezervs; set => setMaxRezervs(value); }
        public int TimerToCreate { get => timerToCreate; set => setTimerToCreate(value); }

        public RezervSettings()
        {
            if (!File.Exists(configFileName))
            {
                File.Create(configFileName);
                using (StreamWriter streamWriter = new StreamWriter(configFileName))
                {
                    streamWriter.WriteLine((1000 * 60 * 24 * 30).ToString());
                    streamWriter.WriteLine(10);
                    streamWriter.Close();
                }
            }
            readConfig();
        }

        private void readConfig()
        {
            string[] lines = File.ReadAllLines(configFileName);
            timerToCreate = int.Parse(lines[0]);
            maxRezervs = int.Parse(lines[1]);
        }

        private void setMaxRezervs(int maxRez)
        {
            string[] lines = { $"{timerToCreate}", $"{maxRez}" };
            File.WriteAllLines(configFileName, lines);
        }

        private void setTimerToCreate(int timeToCre)
        {
            string[] lines = { $"{timeToCre}", $"{maxRezervs}" };
            File.WriteAllLines(configFileName, lines);
        }
    }
}
