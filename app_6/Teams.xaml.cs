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
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Threading;

namespace app_6_1
{
    /// <summary>
    /// Логика взаимодействия для Teams.xaml
    /// </summary>
    public partial class Teams : Window
    {
        SocerContext sc;    // объект контекста для связи с базой данных
        public event EventHandler onSomethingChanged;   // событие запускаемое при любом изменении в списке команд



        public Teams()    // конструктор
        {
            InitializeComponent();
            //Getdata();
            GetdataAsync();
        }


        public void Getdata() 
        {
            using (sc = new SocerContext())
            {
                var teams = sc.Teams.ToList();
                TeamDataGrid.ItemsSource = teams;
            }
        }



        public async void GetdataAsync()
        {
            using (sc = new SocerContext())
            {
                var teams = await sc.Teams.ToListAsync();
                TeamDataGrid.ItemsSource = teams;
            }
        }



        private void Exit_Button_Click(object sender, RoutedEventArgs e)  // обработчик события нажатия кнопки "Exit"
        {
            Close();
        }



        private void Add_Team_Button_Click(object sender, RoutedEventArgs e)  // обработчик события нажатия кнопки "Add team"
        {
            TeamAddOrEdit t = new TeamAddOrEdit();
            t.Owner = this;
            t.Show();
            //t.onTeamAdd += AddTeam;
            t.onTeamAdd += AddTeamAsync;
        }

        private void AddTeam(object sender, TeamArgs e)   // добавление команды
        {
            Team nt = new Team { TeamName = e.TeamName, Coach = e.CoachName };

            if (nt != null)
            {
                using (sc = new SocerContext())
                {
                    sc.Teams.Add(nt);
                    sc.SaveChanges();
                    //MessageBox.Show("New team is added!");


                    ReNewDataGrid();
                    onSomethingChanged(this, EventArgs.Empty);
                }
            }
            else MessageBox.Show("Object ne sozdan!");
            
        }


        private async void AddTeamAsync(object sender, TeamArgs e)   // добавление команды асинхронная версия
        {
            Team nt = new Team { TeamName = e.TeamName, Coach = e.CoachName };

            if (nt != null)
            {
                using (sc = new SocerContext())
                {
                    sc.Teams.Add(nt);
                    await sc.SaveChangesAsync();
                    //MessageBox.Show("New team is added!");


                    ReNewDataGridAsync();
                    onSomethingChanged(this, EventArgs.Empty);
                }
            }
            else MessageBox.Show("Object ne sozdan!");

        }



        private void DeliteTeam_Button_Click(object sender, RoutedEventArgs e)  // удаление команды или команд (Id=1 - команда по умолчанию)
        {
            int numbeofselecteditem = TeamDataGrid.SelectedItems.Count;

            List<int> playersIdToDelite = new List<int>();

            switch (numbeofselecteditem) 
            {
                case 0:
                    MessageBox.Show("No selected rows! ");
                    break;
                case 1:
                    Team temp = (Team)TeamDataGrid.SelectedItem;
                    if (temp.Id == 1)
                    {
                        MessageBox.Show("Team Id=1 (Not defined) is forbidden to delite! ");
                    }
                    else
                    {
                        playersIdToDelite.Add(temp.Id);                        
                        //DeliteTeams(playersIdToDelite);
                        DeliteTeamsAsync(playersIdToDelite);
                    }
                    break;
                default:
                    foreach (Team t in TeamDataGrid.SelectedItems) 
                    {
                        playersIdToDelite.Add(t.Id);                 
                    }
                    //DeliteTeams(playersIdToDelite);
                    DeliteTeamsAsync(playersIdToDelite);
                    break;
            }            
            
        }

        private void TeamDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)   // обработчик события двоейное нажатие на строку 
        {
            using (sc = new SocerContext())
            {
                Team temp = (Team)TeamDataGrid.SelectedItem;
                if (temp.Id != 1)                                                                 // id=1 имеет команда и тренер "not defined".
                {
                    ExtendedTeamArgs m = new ExtendedTeamArgs() { Id = temp.Id, TeamName = temp.TeamName, CoachName = temp.Coach };

                    TeamAddOrEdit t = new TeamAddOrEdit(m);
                    t.Owner = this;
                    t.Show();

                    //t.onTeamEdit += EditTeam;
                    t.onTeamEdit += EditTeamAsync;
                }
                else MessageBox.Show("This row is forbidden to edit!");

            }
        }

        private void EditTeam(object sender, ExtendedTeamArgs e)    // редактирование данных команды
        {
            using (sc = new SocerContext())
            {
                Team temp = new Team();

                temp = sc.Teams.Find(e.Id);

                temp.TeamName = e.TeamName;
                temp.Coach = e.CoachName;


                sc.SaveChanges();
                ReNewDataGrid();
                onSomethingChanged(this, EventArgs.Empty);
            }
        }

        private  async void EditTeamAsync(object sender, ExtendedTeamArgs e)    // редактирование данных команды
        {
            using (sc = new SocerContext())
            {
                Team temp = new Team();

                temp = sc.Teams.Find(e.Id);

                temp.TeamName = e.TeamName;
                temp.Coach = e.CoachName;


                await sc.SaveChangesAsync();
                ReNewDataGridAsync();
                onSomethingChanged(this, EventArgs.Empty);
            }
        }



        public void ReNewDataGrid()   // обновление списка команд в датагриде
        {
            var teams = sc.Teams.ToList();
            TeamDataGrid.ItemsSource = teams;
        }

        public async void ReNewDataGridAsync()   // обновление списка команд в датагриде асинхронная версия
        {
            var teams = await sc.Teams.ToListAsync<Team>();
            TeamDataGrid.ItemsSource = teams;
        }



        private void TeamDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)  // обработчик собтия изменение выделенной строки в датагриде
        {
            /*   Загнал метод в блок try catch  чтобы не выдавало исключение при обращении к пустой строке 
             внизу TeamListBox. Но этого можно было достичь и поставив в xaml коде для элемента 
             TeamDataGrid свойство CanUserAddRows="False". Это свойство уберет пустую строку внизу листбокса 
             и таким образом исключит обращение к этой строке и выбрасывание исключения.   */

            using (sc = new SocerContext())
            {
                try
                {
                    TeamListBox.ItemsSource = null;     // данная строка очищает листбокс при обращении к пустой строке внизу    TeamListBox.            

                    Team temp = (Team)TeamDataGrid.SelectedItem;  // почему то нельзя обратиться напрямую к полю temp.Players с приведением к List<Players>. 

                    Team t = sc.Teams.Find(temp.Id);  // не совсем понял зачем нужен промежуточный объект с запросом к базе данных. Возможно нужен сам запрос ? 

                    List<Player> pl = (List<Player>)t.Players;

                    TeamListBox.ItemsSource = pl;
                }
                catch
                {

                }

            }


            /*
             Так не работает!

            Team temp = (Team)TeamDataGrid.SelectedItem;
            List<Player> pl = (List<Player>)temp.Players;
            TeamListBox.ItemsSource = pl;
            */

        }


        private void OnSomethingChanged(object sender, EventArgs e)  // метод запускающий событие при любом изменении списка команд
        {
            onSomethingChanged?.Invoke(this, EventArgs.Empty);
        }



        public void DeliteTeams(List<int> list)                                           //удаление команд
        {
            using (sc = new SocerContext()) 
            {
                List<Team> teams = sc.Teams.Include("Players").ToList();                  //доступ к связанным данным - свойство Players класса Team
                foreach (int i in list)
                {
                    Team temp = sc.Teams.Find(i);

                    sc.Teams.Remove(temp);
                }

                sc.SaveChanges();
                
                ReNewDataGrid();

                onSomethingChanged(this, EventArgs.Empty);
                MessageBox.Show("Selected teams are remooved from the base! =) ");
            }       
        }


        public  async void DeliteTeamsAsync(List<int> list)                                           //удаление команд
        {
            using (sc = new SocerContext())
            {
                List<Team> teams = await sc.Teams.Include("Players").ToListAsync<Team>();                  //доступ к связанным данным - свойство Players класса Team
                foreach (int i in list)
                {
                    Team temp = sc.Teams.Find(i);

                    sc.Teams.Remove(temp);
                }

                await sc.SaveChangesAsync();

                ReNewDataGridAsync();

                onSomethingChanged(this, EventArgs.Empty);
                MessageBox.Show("Selected teams are remooved from the base! =) ");
            }
        }
    }
}
