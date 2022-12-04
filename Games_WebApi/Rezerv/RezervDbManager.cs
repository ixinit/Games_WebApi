using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games_WebApi.Rezerv
{

    public class RezervDbManager
    {
        String curentDbFile;
        String dbPath = "DataBase/";

        List<String> databases;

        public String CurentDbFile { get => curentDbFile; }
        public List<String> DataBases { get => databases; }

        public RezervDbManager()
        {
            createDbPath();
            getCurentDbFile();
            getDataBases();
        }
        public void createDbPath()
        {
            if (!Directory.Exists(dbPath))
                Directory.CreateDirectory(dbPath);
        }

        public void getDataBases()
        {
            databases = new List<String>();
            foreach (string filename in Directory.GetFiles(dbPath))
            {
                if(filename.EndsWith(".mdb"))
                    databases.Add(filename);
            }
        }

        public void getCurentDbFile()
        {
            curentDbFile = GameShopContext.connectionString.Substring(45); 
        }

        public void setNewDbFile(int index)
        {
            GameShopContext.connectionString = GameShopContext.connectionString.Substring(0, 45) + databases[index];
            Console.WriteLine($"New Prowider: {GameShopContext.connectionString}");
        }

        public void deleteCopy(int id)
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

        public void createCopy()
        {
            File.Copy(curentDbFile, $"{dbPath}db_copy_{DateTime.Now.ToString().Replace(' ', '_').Replace(':','.')}.mdb");
        }

    }
}
