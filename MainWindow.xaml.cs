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
        
        public MainWindow()
        {
            InitializeComponent();

            var firms = daoKvitering.GetAll();
            List<Kvittering> firmList = [.. firms];
            data.ItemsSource = firmList;
        }

        public void opretKvitering()
        {

            DateOnly købsDato = new DateOnly();
            DateOnly slutdato = new DateOnly();
            converter.convertKøbsDato(købsDato, købsDatoPicker);
            converter.convertSlutDato(slutdato, købsDatoPicker);
            købsDato = new DateOnly(int.Parse( converter.købYear),int.Parse( converter.KøbMonth), int.Parse( converter.KøbDay));
            slutdato = new DateOnly(int.Parse(converter.slutYear), int.Parse(converter.slutMonth), int.Parse( converter.slutDay));
            int ordrenummer = int.Parse(ordreNummerBox.Text);
            string email = emailBox.Text;
            string firmanavn = firmaNavnBox.Text;
            string produktNavn = produktNavnbox.Text;
            int produktPris = int.Parse( produktPrisBox.Text);
            Kvittering kvittering = new Kvittering(købsDato, slutdato, ordrenummer, email, firmanavn, produktNavn,produktPris);
            daoKvitering.Create(kvittering);
        }

        private void Opret_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("click");
            opretKvitering();
        }
    }
}