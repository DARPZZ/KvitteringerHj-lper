using System;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Kvitteringer.Database.DaoImplements;
using Kvitteringer.Database.DaoObjects;
using Microsoft.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Kvitteringer
{

    public partial class MainWindow : Window
    {
        DaoKvitering daoKvitering = new DaoKvitering();
        Converter converter = new Converter();
        Search search = new Search();
        List<Kvittering> firmlist = new List<Kvittering>();
        public MainWindow()
        {
            InitializeComponent();
            updateList();
        }
        public void updateList()
        {

            var firms = daoKvitering.GetAll();
            firmlist = [.. firms];
            data.ItemsSource = firmlist;
        }
        public async Task updateListAsync()
        {
            await Task.Run(() =>
            {
                var firms = daoKvitering.GetAll();
                firmlist = new List<Kvittering>(firms);

                Application.Current.Dispatcher.Invoke(() =>
                {
                    data.ItemsSource = firmlist;
                });
            });
        }

        public void opretKvitering()
        {

            DateOnly købsDato = new DateOnly();
            DateOnly slutdato = new DateOnly();
            converter.convertKøbsDato(købsDato, købsDatoPicker);
            converter.convertSlutDato(slutdato, købsDatoPicker);
            købsDato = new DateOnly(int.Parse(converter.købYear), int.Parse(converter.KøbMonth), int.Parse(converter.KøbDay));
            slutdato = new DateOnly(int.Parse(converter.slutYear), int.Parse(converter.slutMonth), int.Parse(converter.slutDay));

            int ordrenummer = int.Parse(ordreNummerBox.Text);
            string email = emailBox.Text;
            string firmanavn = firmaNavnBox.Text;
            string produktNavn = produktNavnbox.Text;
            int produktPris = int.Parse(produktPrisBox.Text);
            Kvittering kvittering = new Kvittering(købsDato, slutdato, ordrenummer, email, firmanavn, produktNavn, produktPris);
            daoKvitering.Create(kvittering);
        }
        private async void Opret_Click(object sender, RoutedEventArgs e)
        {
            opretKvitering();

            await updateListAsync();
        }

        private async void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchBox.Text == "" || SearchBox.Text == null)
            {
                await updateListAsync();
            }
            else
            {
                data.ItemsSource = "";
                data.ItemsSource = search.searchForeProductName()
            }

        }
    }
}