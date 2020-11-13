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
        List<int> NbrOfMales = new List<int>();
        List<int> NbrOfFemales = new List<int>();
        Random rng = new Random(1234);
        public Form1()
        {
            InitializeComponent();
            //Population= GetPopulation(string.Format(@"{0}", textBox1.Text));
            BirthProbabilities = GetBirthProbabilities(@"C:\Temp\születés.csv");
            DeathProbabilities = GetDeathProbabilities(@"C:\Temp\halál.csv");
            //dataGridView1.DataSource = Population;



        }

        private void Simstep(int year, Person person)
        {
            if (!person.IsAlive) return;
            byte age = (byte)(year - person.BirthYear);
            double pDeath = (from x in DeathProbabilities
                             where x.Gender == person.Gender && x.Age == age
                             select x.DProbability).FirstOrDefault();
            if (rng.NextDouble() <= pDeath) person.IsAlive = false;
            if (person.IsAlive && person.Gender == Gender.Female)
            {
                double pBirth = (from x in BirthProbabilities
                                 where x.Age == age
                                 select x.BProbability).FirstOrDefault();
                if (rng.NextDouble() <= pBirth)
                {
                    Person újszülött = new Person();
                    újszülött.BirthYear = year;
                    újszülött.NbrOfChildren = 0;
                    újszülött.Gender = (Gender)(rng.Next(1, 3));
                    Population.Add(újszülött);
                }
            }

        }

        public List<Person> GetPopulation(string csvpath)
        {
            List<Person> population = new List<Person>();
            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
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
                        Age = byte.Parse(line[0]),
                        NbrOfChildren = int.Parse(line[1]),
                        BProbability = double.Parse(line[2])
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
                        Age = byte.Parse(line[1]),
                        DProbability = double.Parse(line[2])
                    });
                }

            }

            return deathprobabilities;
        }
        void Simulation()
        {
            for (int year = 2005; year <= numericUpDown1.Value; year++)
            {
                for (int i = 0; i < Population.Count; i++)
                {
                    Simstep(year, Population[i]);
                }
                int nbrOfMales = (from x in Population
                                  where x.Gender == Gender.Male && x.IsAlive
                                  select x).Count();
                NbrOfMales.Add(nbrOfMales);
                int nbrOfFemales = (from x in Population
                                    where x.Gender == Gender.Female && x.IsAlive
                                    select x).Count();
                NbrOfFemales.Add(nbrOfFemales);
                Console. WriteLine(string.Format("Év:{0} Fiúk:{1} Lányok:{2}", year, nbrOfMales, nbrOfFemales));
            }
        }
        void DisplayResults()
        {
            for (int year = 2005; year <= numericUpDown1.Value; year++)
            {
                richTextBox1.Text += "Szimulációs év: " + year +
                                        "\n\t Fiúk: " + NbrOfMales[year - 2005] +
                                        "\n\t Lányok: " + NbrOfFemales[year - 2005] + "\n\n";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Population = GetPopulation(string.Format(@"{0}", textBox1.Text));
            NbrOfFemales.Clear();
            NbrOfMales.Clear();
            richTextBox1.Clear();
            Simulation();
            DisplayResults();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = ofd.FileName;
            }
        }


    }
}
