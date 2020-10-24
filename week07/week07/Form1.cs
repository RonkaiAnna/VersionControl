using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using week07.Entities;

namespace week07
{
    public partial class Form1 : Form
    {
        List<Person> Population = new List<Person>();
        List<BirthProbablity> BirthProbabilities = new List<BirthProbablity>();
        List<DeatProbability> DeathProbabilities = new List<DeatProbability>();
        Random rng = new Random(1234);
        public Form1()
        {
            InitializeComponent();
            Population= GetPopulation(@"C:\Temp\nép-teszt.csv");
            BirthProbabilities = GetBirthProbabilities(@"C:\Temp\születés.csv");
            DeathProbabilities = GetDeathProbabilities(@"C:\Temp\halál.csv");
            dataGridView1.DataSource = Population;

            for (int year = 2005; year <= 2024; year++)
            {
                for (int i = 0; i < Population.Count; i++)
                {
                    //szimuláció magja
                }
                int nbrOfMales = (from x in Population
                                  where x.Gender == Gender.Male && x.IsAlive
                                  select x).Count();
                int nbrOfFemales = (from x in Population
                                    where x.Gender == Gender.Female && x.IsAlive
                                    select x).Count();
                Console.WriteLine(string.Format("Év:{0} Fiúk:{1} Lányok:{2}", year, nbrOfMales, nbrOfFemales));
            }

        }

        public List<Person> GetPopulation(string csvpath)
        {
            List<Person> population = new List<Person>();
            using (StreamReader sr= new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    population.Add(new Person()
                    {
                        BirthYear = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        NbrOfChildren = int.Parse(line[2])
                    });
                }

            }

            return population;
        }
        public List<BirthProbablity> GetBirthProbabilities(string csvpath)
        {
            List<BirthProbablity> birthprobabilities = new List<BirthProbablity>();
            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    birthprobabilities.Add(new BirthProbablity()
                    {
                        Age=int.Parse(line[0]),
                        NbrOfChildren = int.Parse(line[1]),
                        BProbability=double.Parse(line[2])
                    });
                }

            }

            return birthprobabilities;
        }
        public List<DeatProbability> GetDeathProbabilities(string csvpath)
        {
            List<DeatProbability> deathprobabilities = new List<DeatProbability>();
            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    deathprobabilities.Add(new DeatProbability()
                    {
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[0]),
                        Age = int.Parse(line[1]),
                        DProbability = double.Parse(line[2])
                    });
                }

            }

            return deathprobabilities;
        }
    }
}
