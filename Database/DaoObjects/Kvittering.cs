using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvitteringer.Database.DaoObjects
{
    public class Kvittering
    {
        public int kvitId { get; set; }
        public DateOnly købsDato { get; set; }
        public DateOnly slutDato { get; set; }
        public long ordreNummer { get; set; }
        public string email { get; set; }
        public int firmaId { get; set; }
        public string firmaNavn { get; set; }
        public string produktNavn { get; set; }
        public double produktPris { get; set; }


        public Kvittering(DateOnly købsDato, DateOnly slutDato, long ordreNummer, string email, string firmaNavn, string produktNavn, double produktPris)
        {
            this.købsDato = købsDato;
            this.slutDato = slutDato;
            this.ordreNummer = ordreNummer;
            this.email = email;
            this.firmaId = firmaId;
            this.firmaNavn = firmaNavn;
            this.produktNavn = produktNavn;
            this.produktPris = produktPris;
        }
        public string FormattedKøbsDato => købsDato.ToString("dd/MM/yyyy"); // Change the format as needed
        public string FormattedSlutDato => slutDato.ToString("dd/MM/yyyy"); // Change the format as needed

        public Kvittering(int kvitId, DateOnly købsDatoOnly, DateOnly slutDato, long ordreNummer, int firmaId, string email, string produktNavn, double produktPris)
        {
            this.kvitId = kvitId;
            this.købsDato = købsDato;
            this.slutDato = slutDato;
            this.ordreNummer = ordreNummer;
            this.email = email;
            this.firmaId = firmaId;
            this.produktNavn = produktNavn;
            this.produktPris = produktPris;
        }

        public Kvittering()
        {
        }

        public Kvittering(string firmaNavn, DateOnly købsDato, DateOnly slutDato, long ordreNummer, string email, string produktNavn, double produktPris)
        {
            this.firmaNavn = firmaNavn;
            this.købsDato = købsDato;
            this.slutDato = slutDato;
            this.ordreNummer = ordreNummer;
            this.email = email;
            this.produktNavn = produktNavn;
            this.produktPris = produktPris;
        }

    }

}

