using LOLAPI0217.Model;
using System.Windows;

namespace LOLAPIWPF
{
    public partial class ChampionDetailsWindow : Window
    {
        public ChampionDetailsWindow(Champion champion)
        {
            InitializeComponent();
            DataContext = champion;
        }
    }
}
