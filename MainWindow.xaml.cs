using System;
using System.Diagnostics;
using System.Globalization;
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
        public List<Kvittering> firmListWithFormattedDates { get; set; }
        DaoKvitering daoKvitering = new DaoKvitering();
        Converter converter = new Converter();
        Search search = new Search();
        List<Kvittering> firmlist = new List<Kvittering>();
        GoogleSøgning googleSøgning = new GoogleSøgning();

        public MainWindow()
        {
            InitializeComponent();
            PositionWindowAtTopLeft();
            updateList();
        }
        public void updateList()
        {
           upList();
            data.ItemsSource = firmListWithFormattedDates;


        }

      



        public async Task updateListAsync()
        {
            await Task.Run(() =>
            {
                upList();

                Application.Current.Dispatcher.Invoke(() =>
                {
                    data.ItemsSource = firmListWithFormattedDates;
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
            long ordrenummer = Int64.Parse(ordreNummerBox.Text);
            string email = emailBox.Text;
            string firmanavn = firmaNavnBox.Text;
            string produktNavn = produktNavnbox.Text;
            string produktPris = produktPrisBox.Text;
            String produktPrisDouble = produktPris.Replace(".",",");
            Kvittering kvittering = new Kvittering(købsDato, slutdato, ordrenummer, email, firmanavn, produktNavn, double.Parse(produktPrisDouble));
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
                data.ItemsSource = search.searchForeProductName(SearchBox.Text, firmListWithFormattedDates);
                
            }

        }
        private void PositionWindowAtTopLeft()
        {
            Left = 0;
            Top = 0;
        }

        private void data_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            googleSøgning.findEmail(data);
        }
        public void upList()
        {
            var firms = daoKvitering.GetAll();
            firmListWithFormattedDates = firms.Select(kvittering => new Kvittering
            {
                købsDato = DateOnly.Parse(kvittering.FormattedKøbsDato),
                slutDato = DateOnly.Parse(kvittering.FormattedSlutDato),
                ordreNummer = kvittering.ordreNummer,
                email = kvittering.email,
                firmaId = kvittering.firmaId,
                firmaNavn = kvittering.firmaNavn,
                produktNavn = kvittering.produktNavn,
                produktPris = kvittering.produktPris
            }).ToList();
        }
    }
}