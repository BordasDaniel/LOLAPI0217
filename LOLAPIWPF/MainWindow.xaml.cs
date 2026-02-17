using LOLAPI0217.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace LOLAPIWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<Champion> _champions = [];
        private readonly List<string> _languages = new()
        {
            "en_US",
            "hu_HU",
            "de_DE",
            "fr_FR",
            "es_ES"
        };

        private string? _version;

        public MainWindow()
        {
            InitializeComponent();
            ChampionListBox.ItemsSource = _champions;
            LanguageComboBox.ItemsSource = _languages;
            LanguageComboBox.SelectedIndex = 0;
            Loaded += MainWindow_Loaded;
            LanguageComboBox.SelectionChanged += LanguageComboBox_SelectionChanged;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadChampionsAsync();
        }

        private async void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded)
            {
                return;
            }

            await LoadChampionsAsync();
        }

        private async Task EnsureVersionAsync()
        {
            if (!string.IsNullOrWhiteSpace(_version))
            {
                return;
            }

            using HttpClient client = new();
            string url = "https://ddragon.leagueoflegends.com/api/versions.json";
            var responseAPI = await client.GetStringAsync(url);
            var response = JsonSerializer.Deserialize<string[]>(responseAPI);
            _version = response?.FirstOrDefault();
        }

        private async Task LoadChampionsAsync()
        {
            try
            {
                await EnsureVersionAsync();
                if (string.IsNullOrWhiteSpace(_version))
                {
                    return;
                }

                var language = LanguageComboBox.SelectedItem as string ?? "en_US";
                using HttpClient client = new();
                string url = $"https://ddragon.leagueoflegends.com/cdn/{_version}/data/{language}/champion.json";
                var responseAPI = await client.GetStringAsync(url);
                var response = JsonSerializer.Deserialize<ChampionDatas>(responseAPI);

                _champions.Clear();
                foreach (var champion in response?.Data?.Values ?? Enumerable.Empty<Champion>())
                {
                    _champions.Add(champion);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba történt az adatok betöltésekor: {ex.Message}", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ListBoxItem_MouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is ListBoxItem item)
            {
                item.IsSelected = true;
            }
        }

        private void DetailsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (ChampionListBox.SelectedItem is not Champion champion)
            {
                return;
            }

            var detailsWindow = new ChampionDetailsWindow(champion)
            {
                Owner = this
            };

            detailsWindow.ShowDialog();
        }
    }
}