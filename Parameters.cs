using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;
using System.Reflection;
using System.Drawing;

namespace Sapper
{
	/// <summary>
    /// Keeping data and their loading from file.
	/// </summary>
	public class Parameters
	{
        public struct MineField
        {
            int Width;
            Size Size;
            public MineField(int width, Size size)
            {
                this.Width = width;
                this.Size = size;
            }
        }

        private struct GameDifficulty
        {
            public string TypeName;
            public int Width;
            public int Height;
            public int Mines;
            public GameDifficulty(string typeName, int width, int height, int mines)
            {
                this.TypeName = typeName;
                this.Width = width;
                this.Height = height;
                this.Mines = mines;
            }            
        }

        private static GameDifficulty[] GameDifficultys = {
            new GameDifficulty("новичков",  9, 9, 10),
            new GameDifficulty("любителей", 16, 16, 40),
            new GameDifficulty("профессионалов", 30, 16, 99)
        };

        public class Champion
        {
            private static string anonym = "Аноним";
            private static int maxTime = 999;

            public string Name;
            public int Time;

            public Champion() { Reset(); }

            public Champion(string name, int time) { this.Name = name; this.Time = time; }

            public void Reset()
            {
                Name = anonym;
                Time = maxTime;
            }
        };
         
        [Serializable]
        struct DataInit
        {
            public bool AlreadyPlayed;
            public bool Color;
            public int Difficulty;
            public int Height;
            public bool Mark;
            public int Mines;
            public string Name1;
            public string Name2;
            public string Name3;
            public bool Sound;
            public int Time1;
            public int Time2;
            public int Time3;
            public int Width;
            public int Xpos;
            public int Ypos;
        }

        private const string nameIniFile = "Sapper.ini";
        private const int leftDialogsLocation = 3;
        private const int champTopDialogLocation = 87;
        private const int customTopDialogLocation = 41;
        private System.Windows.Forms.Form ownerForm;
        private DataInit dataInit;

        private void SetDefaultDataInit()
        {
            dataInit = new DataInit();
            dataInit.AlreadyPlayed = false;
            dataInit.Color = true;
            dataInit.Difficulty = 0;
            dataInit.Height = 9;
            dataInit.Mark = true;
            dataInit.Mines = 10;
            dataInit.Sound = false;
            dataInit.Width = 9;
            dataInit.Xpos = 200;
            dataInit.Ypos = 200;

            ChampionsReset();
        }             
        
        public Parameters() { }

        public int Mines
        {
            get { return dataInit.Mines; }
            set { dataInit.Mines = value; }
        }

        public Size Size
        {
            get { return new Size(dataInit.Width, dataInit.Height); }
            set
            {
                dataInit.Width = value.Width;
                dataInit.Height = value.Height;
            }
        }

        public Point Location
        {
            get { return new Point(dataInit.Xpos, dataInit.Ypos); }
            set
            {
                dataInit.Xpos = value.X;
                dataInit.Ypos = value.Y;
            }
        }

        public int Difficulty
        {
            get { return dataInit.Difficulty; }
            set
            {
                dataInit.Difficulty = value;
                if (dataInit.Difficulty == 3)
                {
                    this.Custom();
                    return;
                }
                this.Size = new Size(GameDifficultys[dataInit.Difficulty].Width,
                    GameDifficultys[dataInit.Difficulty].Height);
                this.Mines = GameDifficultys[dataInit.Difficulty].Mines;
            }
        }

        public bool QuestionMark
        {
            get { return dataInit.Mark;  }
            set { dataInit.Mark = value; }
        }

        public bool Color
        {
            get { return dataInit.Color; }
            set { dataInit.Color = value; }
        }

        /// <summary>
        /// Set owner all dialogue windows, used for entering data.
        /// </summary>
        /// <param name="ownerForm">Owner for all dialogue windows.</param>
        public void SetDialogsOwner(System.Windows.Forms.Form ownerForm) { this.ownerForm = ownerForm; }

        public Champion[] GetChampions()
        {
            Champion[] champions = new Champion[3];
            champions[0] = new Champion(dataInit.Name1, dataInit.Time1);
            champions[1] = new Champion(dataInit.Name2, dataInit.Time2);
            champions[2] = new Champion(dataInit.Name3, dataInit.Time3);            
            return champions;
        }

        public void SetChampions(Champion[] champions)
        {
            dataInit.Name1 = champions[0].Name;
            dataInit.Name2 = champions[1].Name;
            dataInit.Name3 = champions[2].Name;

            dataInit.Time1 = champions[0].Time;
            dataInit.Time2 = champions[1].Time;
            dataInit.Time3 = champions[2].Time;
        }

        public void ChampionsReset()
        {
            Champion[] champions = GetChampions();
            foreach (Champion champ in champions) champ.Reset();
            SetChampions(champions);
        }
        
		/// <summary>
        /// Loading data from file.
		/// </summary>
        public void Load()
		{
			if(!File.Exists(nameIniFile))
			{
				this.SetDefaultDataInit();
                return;
			}
			using (FileStream readStream = File.OpenRead(nameIniFile))
			{
				SoapFormatter soapFormat = new SoapFormatter();
				try
				{
					dataInit = (DataInit)soapFormat.Deserialize(readStream);
				}
				catch
				{
					this.SetDefaultDataInit();
				}
			}	
		}

		/// <summary>
        /// Save program parameters in the file.
		/// </summary>
        public void Save()
		{
            if (File.Exists(nameIniFile)) File.Delete(nameIniFile);
			using (FileStream writeStream = File.Create(nameIniFile))
			{
				SoapFormatter soapFormat = new SoapFormatter();
                soapFormat.Serialize(writeStream, dataInit);
			}
		}

        /// <summary>
        /// Input custom parameters games field.
        /// </summary>
        public void Custom()
        {
            CustomForm customForm = new CustomForm();
            customForm.SetParameters(this);
            customForm.Location = new System.Drawing.Point(
                this.ownerForm.Left + leftDialogsLocation,
                this.ownerForm.Top + customTopDialogLocation);
            customForm.ShowDialog();
            customForm.Dispose();
        }

        /// <summary>
        /// Change the data if record is installed.
        /// </summary>
        /// <param name="time">Spented time</param>
        public void Modify(int time)
        {
            if ((this.Difficulty < 0) || (this.Difficulty > 2)) return;
            Champion[] champions = GetChampions();
            int currTime = champions[this.Difficulty].Time;
            if (time < currTime)
            {
                champions[this.Difficulty].Time = time;
                ChampionForm championForm = new ChampionForm(GameDifficultys[this.Difficulty].TypeName);
                championForm.Location = new System.Drawing.Point(
                    this.ownerForm.Left + leftDialogsLocation,
                    this.ownerForm.Top + champTopDialogLocation);
                championForm.ChampName = champions[this.Difficulty].Name;
                championForm.ShowDialog();
                champions[this.Difficulty].Name = championForm.ChampName;
                championForm.Dispose();
                this.SetChampions(champions);
                ShowBestTimes();
            }
        }

        /// <summary>
        /// Show dialogue window with information on champions.
        /// </summary>
        public void ShowBestTimes()
        {
            BestTimesForm bestTimesForm = new BestTimesForm();
            bestTimesForm.Location = new System.Drawing.Point(
                    this.ownerForm.Left + leftDialogsLocation,
                    this.ownerForm.Top + champTopDialogLocation);
            bestTimesForm.SetParameters(this);
            bestTimesForm.ShowDialog();
            bestTimesForm.Dispose();
        }
    }
}
