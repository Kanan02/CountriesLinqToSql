using CountriesLinq.SeedData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CountriesLinq
{
    public partial class Form1 : Form
    {
        CountriesDBEntities countriesDB;
        public Form1()
        {
            InitializeComponent();
            countriesDB = new CountriesDBEntities();
            comboBox1.Items.Add("Select all continents");
            comboBox1.Items.Add("Select all countries");
            comboBox1.Items.Add("Select countries of");
            comboBox1.Items.Add("Select cities of");
            comboBox1.Items.Add("Select all capitals");
            comboBox1.Items.Add("Top 5 largest countries");
            comboBox1.Items.Add("Top 5 populated countries");
            comboBox1.Items.Add("Top 5 populated cities");
            comboBox1.Items.Add("Top 3 largest continents");
            comboBox1.Items.Add("Top 3 populated continents");
            foreach (var item in countriesDB.Continents)
            {
                comboBoxCont.Items.Add (item.Name);
            }
            foreach (var item in countriesDB.Countries)
            {
                comboBoxCountries.Items.Add(item.Name);
            }
            comboBoxCont.Visible=false;
            comboBoxCountries.Visible=false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (countriesDB.Database.CreateIfNotExists())
            {
                countriesDB.SaveChanges();
                MessageBox.Show("DB is created!");
            }
            else
            {
                MessageBox.Show("DB was already created!");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataSeeder seeder = new DataSeeder(countriesDB.Database.Connection.ConnectionString);
            seeder.SeedData();
            countriesDB.SaveChanges();
            MessageBox.Show("Data is seeded!");
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
            switch ((sender as ComboBox).SelectedIndex)
            {
                case 0:
                    dataGridView1.DataSource = countriesDB.Continents.ToList();  
                    break;
                case 1:
                    dataGridView1.DataSource=countriesDB.Countries.ToList();
                    break;
                case 2:
                    comboBoxCont.Visible = true;
                    return;
                case 3:
                    comboBoxCountries.Visible = true;
                    return;
                case 4:
                    dataGridView1.DataSource = countriesDB.Cities.Where(c => c.IsCapital == true).ToList();
                    break;
                case 5:
                    dataGridView1.DataSource = (from count in countriesDB.Countries
                                                orderby count.Area descending
                                                select new
                                                {
                                                    name = count.Name,
                                                    Area = count.Area
                                                }

                                                ).Take(5).ToList();
                    break;
                case 6:
                    dataGridView1.DataSource = (from count in countriesDB.Countries
                                                orderby count.Cities.Sum(x=>x.Population) descending
                                                select new
                                                {
                                                    name = count.Name,
                                                    Population = count.Cities.Sum(x => x.Population)
                                                }

                                                ).Take(5).ToList();
                    break;
                case 7:
                    dataGridView1.DataSource = (from city in countriesDB.Cities
                                                orderby city.Population descending
                                                select new
                                                {
                                                    name = city.Name,
                                                    Population = city.Population
                                                }

                                                ).Take(5).ToList();
                    break;
                case 8:
                    dataGridView1.DataSource = (from cont in countriesDB.Continents
                                                orderby cont.Countries.Sum(x => x.Area) descending
                                                select new
                                                {
                                                    Name = cont.Name,
                                                    Area = cont.Countries.Sum(x => x.Area)
                                                }

                                               ).Take(3).ToList();
                    break;
                case 9:
                    dataGridView1.DataSource = (from cont in countriesDB.Continents
                                                orderby cont.Countries.Sum(x => x.Cities.Sum(p=>p.Population)) descending
                                                select new
                                                {
                                                    Name = cont.Name,
                                                    Population = cont.Countries.Sum(x => x.Cities.Sum(p => p.Population))
                                                }

                                               ).Take(3).ToList();
                    break;
                default:
                    break;
            }
            comboBoxCont.Visible=false;
            comboBoxCountries.Visible=false;
        }
        private void comboBoxCont_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ContId = (sender as ComboBox).SelectedIndex+1;
            dataGridView1.DataSource=countriesDB.Countries.Where(x=>x.ContinentId==ContId).ToList();
        }

        private void comboBoxCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
            int CountId = (sender as ComboBox).SelectedIndex+1;
            dataGridView1.DataSource = countriesDB.Cities.Where(x => x.CountryId == CountId).ToList();
        }
    }
}
