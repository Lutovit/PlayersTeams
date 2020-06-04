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
using System.Threading;
using System.Data.Entity;

namespace app_6_1
{
    /// <summary>
    /// Логика взаимодействия для AddOrEdit.xaml
    /// </summary>
    /// 
    public enum Pos 
    {
        vratar,
        zaschitnik,
        poluzaschitnik,
        napadauschii   
    }
    public class Posicion
    {
        public Pos posicion { set; get; }
        public Posicion() { }
        public Posicion(Pos p) 
        {
            posicion = p;        
        }
        
        public override string ToString()
        {
            return posicion.ToString();
        }       

    }

    /*   В базе данных столбец таблицы куда будет закидываться значение позиции игрока на поле  должен называться  pos_posicion и иметь тип int. 
     pos - поле типа данных Posicion (класс Posicion) в классе Player
     posicion - поле типа Pos (перечисление Pos) в классе Posicion    */


    public partial class AddOrEdit : Window
    {
        public event EventHandler<MyArgs> onAdd;
        public event EventHandler<ExtendedMyArgs> onEdit;
        public static int PlayerID = 0;

        SocerContext sc;

        public ObservableCollection<Posicion> posicion = new ObservableCollection<Posicion> 
        {
            new Posicion(Pos.vratar),
            new Posicion(Pos.zaschitnik),
            new Posicion(Pos.poluzaschitnik),
            new Posicion(Pos.napadauschii)
        };



        public AddOrEdit()
        {
            InitializeComponent();
            PosicionComboBox.ItemsSource = posicion;
            //GetData()
            GetDataAsync();                                                                              //асинхронная версия
        }


        public void GetData()
        {
            using (sc = new SocerContext())
            {
                var teams = sc.Teams.ToList();
                TeamComboBox.ItemsSource = teams;
            }

        }

        public async void GetDataAsync() 
        {
            using (sc = new SocerContext())
            {
                var teams = await sc.Teams.ToListAsync<Team>();
                TeamComboBox.ItemsSource = teams;
            }

        }


        public AddOrEdit(ExtendedMyArgs e)
        {
            InitializeComponent();
            //ShowSelectedPlayerData(e);
            ShowSelectedPlayerDataAsync(e);                                                               //асинхронная версия
        }


        private void ShowSelectedPlayerData(ExtendedMyArgs e)
        {
            int posIndex = 0;
            int teamIndex = 0;

            PlayerID = e.Id;
            //MessageBox.Show(PlayerID.ToString());
            TextBoxName.Text = e.name;
            TextBoxAge.Text = e.age.ToString();

            using (sc = new SocerContext())
            {
                var teams = sc.Teams.ToList();
                TeamComboBox.ItemsSource = teams;

                foreach (Team t in teams) 
                {
                    if (t.Id == e.team_id) teamIndex = teams.IndexOf(t);               
                }
                //MessageBox.Show(teamIndex.ToString());
            }            

            PosicionComboBox.ItemsSource = posicion;

            posIndex = (Int32)e.posicion.posicion;

            PosicionComboBox.SelectedIndex = posIndex;
            TeamComboBox.SelectedIndex = teamIndex;             
            
        }



        private async void ShowSelectedPlayerDataAsync(ExtendedMyArgs e)
        {
            int posIndex = 0;
            int teamIndex = 0;

            PlayerID = e.Id;
            //MessageBox.Show(PlayerID.ToString());
            TextBoxName.Text = e.name;
            TextBoxAge.Text = e.age.ToString();

            using (sc = new SocerContext())
            {
                var teams = await sc.Teams.ToListAsync<Team>();
                TeamComboBox.ItemsSource = teams;

                foreach (Team t in teams)
                {
                    if (t.Id == e.team_id) teamIndex = teams.IndexOf(t);
                }
                //MessageBox.Show(teamIndex.ToString());
            }

            PosicionComboBox.ItemsSource = posicion;

            posIndex = (Int32)e.posicion.posicion;

            PosicionComboBox.SelectedIndex = posIndex;
            TeamComboBox.SelectedIndex = teamIndex;

        }


        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            if (PlayerID == 0)
            {
                Addplayer();
            }
            else EditPlayerData();

        }


        private void EditPlayerData()
        {
            if (TextBoxName.Text.Length > 0 && TextBoxAge.Text.Length > 0)
            {
                ExtendedMyArgs extarg = new ExtendedMyArgs();

                extarg.Id = PlayerID;
                extarg.name = TextBoxName.Text;
                extarg.age = Int32.Parse(TextBoxAge.Text);
                extarg.posicion= (Posicion)PosicionComboBox.SelectedItem;

                Team tempTeam = (Team)TeamComboBox.SelectedItem;     // при добавлении игрока инициализировать его поле TemId а не поле Team! 

                extarg.team_id = tempTeam.Id;
                


                OnEdit(extarg);
                Close();
            }
            else MessageBox.Show("You should insert all data or exit!");
        }


        public void Addplayer()
        {
            if (TextBoxName.Text.Length > 0 && TextBoxAge.Text.Length > 0 )
            {
                MyArgs b = new MyArgs();

                b.name = TextBoxName.Text;
                b.age = Int32.Parse(TextBoxAge.Text);
                b.posicion = (Posicion)PosicionComboBox.SelectedItem;
                //b.team = (Team)TeamComboBox.SelectedItem;

                Team tempTeam = (Team)TeamComboBox.SelectedItem;     // при добавлении игрока инициализировать его поле TemId а не поле Team! 

                b.team_id = tempTeam.Id;                
                
                OnAdd(b);
                Close();
            }
            else MessageBox.Show("You should insert all data or exit!");
        }


        public void OnAdd(MyArgs e)
        {
            onAdd?.Invoke(this, e);
        }

        public void OnEdit(ExtendedMyArgs m)
        {
            onEdit?.Invoke(this, m);
        }


    }
}
