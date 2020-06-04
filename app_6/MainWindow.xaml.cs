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
using System.Data.Entity;
using System.Threading;

namespace app_6_1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SocerContext sc;


        public MainWindow()
        {
            InitializeComponent();

            //GetData();                                                                                      // синхронный вариант            

            GetDataAsync();                                                                               //асинхронный вариант
        }

        public void GetData()                                             
        {
            using (sc = new SocerContext())
            {
                List<Player> players = sc.Players.Include("Team").ToList();
                DataGridOfPlayers.ItemsSource = players;
            }
        }

        public async void GetDataAsync()                                                                 //асинхронный вариант
        {
            using (sc = new SocerContext()) 
            {                
                List<Player> lp = await sc.Players.Include("Team").ToListAsync<Player>();
                DataGridOfPlayers.ItemsSource = lp;
            }                                    
        }                       


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrEdit b = new AddOrEdit();
            b.Owner = this;
            b.Show();

            //b.onAdd += AddPlayer;
            b.onAdd += AddPlayerAsync;
        }

        private void EditPlayer(object sender, ExtendedMyArgs e)
        {
            //MessageBox.Show(e.Id + " ; " + e.name + " ; " + e.age + " ; " + e.posicion + " ; " + e.team_id );

            using (sc = new SocerContext())
            {
                Player temp = new Player();

                temp = sc.Players.Find(e.Id);

                temp.Name = e.name;
                temp.Age = e.age;
                temp.pos = e.posicion;
                temp.TeamId = e.team_id;
                

                sc.SaveChanges();
                ReNewDataGrid();
            }

        }


        private async void EditPlayerAsync(object sender, ExtendedMyArgs e)
        {
            //MessageBox.Show(e.Id + " ; " + e.name + " ; " + e.age + " ; " + e.posicion + " ; " + e.team_id );

            using (sc = new SocerContext())
            {
                Player temp = new Player();

                temp = await sc.Players.FindAsync(e.Id);

                temp.Name = e.name;
                temp.Age = e.age;
                temp.pos = e.posicion;
                temp.TeamId = e.team_id;


                await sc.SaveChangesAsync();
                ReNewDataGridAsync();
            }

        }

        private void AddPlayer(object sender, MyArgs e)
        {
            Player pl = new Player { Name = e.name, Age = e.age, pos=e.posicion, TeamId=e.team_id};   // при добавлении игрока инициализировать его поле TemId а не поле Team!

            if (pl != null)
            {
                using (sc = new SocerContext())
                {
                    sc.Players.Add(pl);

                    sc.SaveChanges();
                    //MessageBox.Show("Object dobavlen v bazu! =) ");
                    ReNewDataGrid();
                }
            }
            else MessageBox.Show("object ne sozdan! ");
        }

        private async void AddPlayerAsync(object sender, MyArgs e)
        {
            Player pl = new Player { Name = e.name, Age = e.age, pos = e.posicion, TeamId = e.team_id };   // при добавлении игрока инициализировать его поле TeamId а не поле Team!

            if (pl != null)
            {
                using (sc = new SocerContext())
                {
                    sc.Players.Add(pl);
                    try
                    {
                        await sc.SaveChangesAsync();
                        //MessageBox.Show("Object dobavlen v bazu! =) ");
                        ReNewDataGridAsync();
                    }
                    catch 
                    {
                        MessageBox.Show("Something goes wrong with AddPlayerAsync ! =)");              
                    }

                }
            }
            else MessageBox.Show("object ne sozdan! ");
        }


        public void ReNewDataGrid()
        {
            List<Player>players = sc.Players.Include("Team").ToList();   // досутп к свойству Team навигационное свойство
            DataGridOfPlayers.ItemsSource = players;
        }


        public async void ReNewDataGridAsync()
        {
            try
            {
                List<Player> players = await sc.Players.Include("Team").ToListAsync<Player>();   // досутп к свойству Team навигационное свойство
                DataGridOfPlayers.ItemsSource = players;
            }
            catch 
            {
                MessageBox.Show("Something goes wrong with ReNewDataGridAsync ! =)");
            }
        }


        public void ReNewDataGrid_2(object sender, EventArgs e)
        {
            using (SocerContext sc = new SocerContext()) 
            {
                List<Player> players = sc.Players.Include("Team").ToList();   // досутп к свойству Team навигационное свойство
                DataGridOfPlayers.ItemsSource = players;
                //MessageBox.Show("ReNewDataGrid_2 is done!  ");
            }
        }

        public async void ReNewDataGrid_2Async(object sender, EventArgs e)
        {
            using (SocerContext sc = new SocerContext())
            {
                List<Player> players = await sc.Players.Include("Team").ToListAsync<Player>();   // досутп к свойству Team навигационное свойство
                DataGridOfPlayers.ItemsSource = players;
                //MessageBox.Show("ReNewDataGrid_2 is done!  ");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

            if (DataGridOfPlayers.SelectedItems.Count > 0)
            {
                using (sc = new SocerContext())
                {
                    foreach (Player p in DataGridOfPlayers.SelectedItems)
                    {
                        Player temp = sc.Players.Find(p.Id);
                        sc.Players.Remove(temp);
                    }


                    sc.SaveChanges();
                    MessageBox.Show("Objects are remooved from the base! =) ");
                    ReNewDataGrid();
                }

            }
            else MessageBox.Show("No selected rows! ");
        }

        private void DataGridOfPlayers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            using (sc = new SocerContext())
            {
                Player temp = (Player)DataGridOfPlayers.SelectedItem;
                ExtendedMyArgs m = new ExtendedMyArgs();

                if (temp.TeamId != null)
                {
                    m.Id = temp.Id;
                    m.name = temp.Name;
                    m.age = temp.Age;
                    m.posicion = temp.pos;
                    m.team_id = (Int32)temp.TeamId;
                    //MessageBox.Show("Ветка temp.TeamId != null ");
                }
                else 
                {
                    m.Id = temp.Id;
                    m.name = temp.Name;
                    m.age = temp.Age;
                    m.posicion = temp.pos;
                    m.team_id = 0;
                    //MessageBox.Show("Ветка temp.TeamId == null ");
                }                               

                AddOrEdit b = new AddOrEdit(m);
                b.Owner = this;
                b.Show();

                b.onEdit += EditPlayerAsync;
            }
        }

        private void AddTeamButton_Click(object sender, RoutedEventArgs e)
        {
            Teams t = new Teams();
            t.Owner = this;
            t.Show();
            t.onSomethingChanged += ReNewDataGrid_2Async;
        }


        
    }
}
