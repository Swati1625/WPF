using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using System.Data;

namespace WpfApplication
{

    public partial class MainWindow : Window
    {
        static SQLiteConnection dbConnection = new SQLiteConnection("Data Source=Users.sqlite;Version=3;");
        public MainWindow()
        {
            //CreateDB();
            InitializeComponent();
            GetUsers();
        }

        private void CreateDB()
        {
            SQLiteConnection.CreateFile("Users.sqlite");
            dbConnection.Open();
            string sql = "create table Users ( FirstName varchar(50), LastName varchar(50))";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();
            dbConnection.Close();
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            dbConnection.Open();
            string sql = "insert into Users (FirstName, LastName) values ('" + firstName.Text + "','" + lastName.Text + "' )";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();
            dbConnection.Close();
            Reset();
            GetUsers();
        }
        private void Reset()
        {
            firstName.Text = "";
            lastName.Text = "";
        }
        private void GetUsers()
        {
            dbConnection.Open();
            string sql = "select * from users";
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, dbConnection);
            DataTable dtUser = new DataTable();
            adapter.Fill(dtUser);
            var userList = dtUser.AsEnumerable().Select(r => new Users()
            {
                FirstName = (string)r["FirstName"],
                LastName = (string)r["LastName"]
            }
            ).ToList();
            UsersGrid.ItemsSource = userList;
            dbConnection.Close();

           
        }

       
        
        private class Users
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
      
    }
}
