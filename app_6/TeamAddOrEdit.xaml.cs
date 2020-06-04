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

namespace app_6_1
{
    /// <summary>
    /// Логика взаимодействия для TeamAddOrEdit.xaml
    /// </summary>
    public partial class TeamAddOrEdit : Window
    {
        public event EventHandler<TeamArgs> onTeamAdd;
        public event EventHandler<ExtendedTeamArgs> onTeamEdit;
        
        
        public int TeamID = 0;

        public TeamAddOrEdit()
        {
            InitializeComponent();
        }
        public TeamAddOrEdit(ExtendedTeamArgs m)
        {
            InitializeComponent();
            ShowSelectedTeamData(m);
        }

        private void ShowSelectedTeamData(ExtendedTeamArgs e)
        {
            TeamID = e.Id;
            TeamNameTextBox.Text = e.TeamName;
            CoachNameTextBox.Text = e.CoachName;
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Apply_Button_Click(object sender, RoutedEventArgs e)
        {
            if (TeamID == 0)
            {
                AddTeam();
            }
            else EditTeam();
        }

        private void EditTeam()
        {
            if (TeamNameTextBox.Text.Length > 0 && CoachNameTextBox.Text.Length > 0)
            {
                ExtendedTeamArgs extarg = new ExtendedTeamArgs();

                extarg.Id = TeamID;
                extarg.TeamName = TeamNameTextBox.Text;
                extarg.CoachName = CoachNameTextBox.Text;                

                OnTeamEdit(extarg);
                
                Close();
            }
            else MessageBox.Show("You should insert all data or exit!");
        }

        private void OnTeamEdit(ExtendedTeamArgs m)
        {
            onTeamEdit?.Invoke(this, m);
        }

        private void AddTeam()
        {
            if (TeamNameTextBox.Text.Length > 0 && CoachNameTextBox.Text.Length > 0)
            {
                TeamArgs m = new TeamArgs { TeamName = TeamNameTextBox.Text, CoachName = CoachNameTextBox.Text };
                OnTeamAdd(m);
                Close();
            }
            else MessageBox.Show("Enter Team name and Coach name!");

        }

        private void OnTeamAdd(TeamArgs m)
        {
            onTeamAdd?.Invoke(this, m);
        }

        


    }
}
