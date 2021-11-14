using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountriesLinq.SeedData
{
    class DataSeeder
    {
        public string ConnectionString { get; set; }
        public DataSeeder(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public void SeedData()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                string str1 = @"INSERT INTO Continents (Name) VALUES ('Africa');
INSERT INTO Continents (Name) VALUES ('America');
INSERT INTO Continents (Name) VALUES ('Eurasia');
INSERT INTO Continents (Name) VALUES ('Australia');
INSERT INTO Continents (Name) VALUES ('Antarctida');";
                string str2 = @"INSERT INTO Countries (Name,ContinentId,Area)VALUES ('France',3,2000000);
INSERT INTO Countries (Name,ContinentId,Area)VALUES ('Greece',3,200000);
INSERT INTO Countries (Name,ContinentId,Area)VALUES ('England',3,1500000);
INSERT INTO Countries (Name,ContinentId,Area)VALUES ('Azerbaijan',3,90000);
INSERT INTO Countries (Name,ContinentId,Area)VALUES ('Brazil',2,2480000);
INSERT INTO Countries (Name,ContinentId,Area)VALUES ('Australia',4,7000000);
INSERT INTO Countries (Name,ContinentId,Area)VALUES ('C1',5,5000000);
INSERT INTO Countries (Name,ContinentId,Area)VALUES ('OAE',1,1200000);
INSERT INTO Countries (Name,ContinentId,Area)VALUES ('Kenia',1,190000);
INSERT INTO Countries (Name,ContinentId,Area)VALUES ('Canada',2,1200000);";
                string str3 = @"INSERT INTO Cities (Name,CountryId,Population,IsCapital)VALUES ('Baku',4,3000000,1);
INSERT INTO Cities (Name,CountryId,Population,IsCapital)VALUES ('Paris',1,10000000,1);
INSERT INTO Cities (Name,CountryId,Population,IsCapital)VALUES ('London',3,15000000,1);
INSERT INTO Cities (Name,CountryId,Population,IsCapital)VALUES ('Brazilia',5,19000000,1);
INSERT INTO Cities (Name,CountryId,Population,IsCapital)VALUES ('Lankaran',4,500000,0);
INSERT INTO Cities (Name,CountryId,Population,IsCapital)VALUES ('Strasbourg',1,2000000,0);
INSERT INTO Cities (Name,CountryId,Population,IsCapital)VALUES ('Rio',5,11000000,0);";
                SqlCommand cmd = new SqlCommand(str1, con);
                SqlCommand cmd2 = new SqlCommand(str2, con);
                SqlCommand cmd3 = new SqlCommand(str3, con);
                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                cmd3.ExecuteNonQuery();
            }
        }
    }
}