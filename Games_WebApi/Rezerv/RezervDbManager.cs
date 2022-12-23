using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Games_WebApi.Rezerv
{
    public static class RezervDbManager
    {
        private static String curentDbFile;
        private static String dbPath = "DataBase/";
        private static List<String> databases;
        private static int time_sleep;
        private static int count_db;

        public static String CurentDbFile { get => curentDbFile; }
        public static List<String> DataBases { get => databases; }

        public static RezervSettings Settings;

        static RezervDbManager()
        {
            createDbPath();
            getCurentDbFile();
            getDataBases();

            Settings = new RezervSettings();
            time_sleep = Settings.TimerToCreate;
            count_db = Settings.MaxRezervs;
        }

        public static void createDbPath()
        {
            if (!Directory.Exists(dbPath))
                Directory.CreateDirectory(dbPath);
        }

        public static void getDataBases()
        {
            databases = new List<String>();
            foreach (string filename in Directory.GetFiles(dbPath))
            {
                if (filename.EndsWith(".mdb"))
                    databases.Add(filename);
            }
        }

        public static void getCurentDbFile()
        {
            curentDbFile = GameShopContext.connectionString.Substring(45);
        }

        public static void setNewDbFile(int index)
        {
            GameShopContext.connectionString = GameShopContext.connectionString.Substring(0, 45) + databases[index];
            Console.WriteLine($"New Prowider: {GameShopContext.connectionString}");
        }

        public static void deleteCopy(int id)
        {
            if (curentDbFile.Equals(databases[id]))
            {
                File.Delete(databases[id]);
                getDataBases();
                setNewDbFile(0);
                getCurentDbFile();
            }
            else
            {
                File.Delete(databases[id]);
                getDataBases();
            }
        }

        public static void createIntermediateCopy()
        {
            if (time_sleep != -1)
            {
                Task task = new Task(
                    () =>
                    {
                        Thread.Sleep(time_sleep);
                        createCopy();
                        delelteOld();
                        createIntermediateCopy();
                    });
                task.Start();
            }
        }

        public static void delelteOld()
        {
            DateTime tmpTime;
            int delete_index = 2;
            // 2 смещение - нужные файлы в директории(резервый чистой бд)
            
            try
            {
                for (int j = 0; j < databases.Count - 1 - count_db; j++)
                {
                    tmpTime = DateTime.Parse(databases[2].Substring(17, 19).Split('_')[0] + " " + databases[4].Substring(17, 19).Split('_')[1].Replace('.', ':'));

                    for (int i = 2; i <= count_db; i++)
                    {
                        DateTime time = DateTime.Parse(databases[i].Substring(17, 19).Split('_')[0] + " " + databases[i].Substring(17, 19).Split('_')[1].Replace('.', ':'));
                        if (tmpTime > time)
                        {
                            tmpTime = time;
                            delete_index = i;
                        }
                    }

                    deleteCopy(delete_index);
                    Console.WriteLine($"[Rezerv] Bakup {databases[delete_index]} was deleted");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Rezerv dele error\n"+e);
                /*Console.WriteLine(databases[2].Substring(17, 19).Split('_')[0] + " " + databases[4].Substring(17, 19).Split('_')[1].Replace('.', ':'));
                Console.WriteLine(DateTime.Now.ToString());*/
            }
            getDataBases();
            
        }

        public static void createCopy()
        {
            File.Copy(curentDbFile, $"{dbPath}db_copy_{DateTime.Now.ToString().Replace(' ', '_').Replace(':', '.')}.mdb");
            Console.WriteLine($"[Rezerv] Bakup {dbPath}db_copy_{DateTime.Now.ToString().Replace(' ', '_').Replace(':', '.')}.mdb was created");
        }

    }
}
