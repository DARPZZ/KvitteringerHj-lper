using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Kvitteringer
{
    public class Converter
    {
        public string KøbDay { get; set; }
        public string KøbMonth { get; set; }
        public string købYear { get; set; }
        public string slutDay { get; set; }
        public string slutMonth { get; set; }
        public string slutYear { get; set; }
        public void convertKøbsDato(DateOnly købsDato,DatePicker købsDatoPicker)
        {
            try
            {   
                var købsdatoSplitter = købsDatoPicker.Text.Split('-');
                købYear = købsdatoSplitter[2];
                Debug.WriteLine(købsdatoSplitter[2]);
                KøbMonth = købsdatoSplitter[1];
                KøbDay = købsdatoSplitter[0];
            }catch (Exception ex)
            {
                Debug.WriteLine("fejl i contertStartDato "+ex.Message);
            }

        }
        public void convertSlutDato (DateOnly slutdato,DatePicker købsDatoPicker)
        {
            try
            {
                var købsdatoSplitter = købsDatoPicker.Text.Split('-');
                slutYear = købsdatoSplitter[2];
                int powerInt = int.Parse(slutYear) + 2;
                slutYear = powerInt.ToString();
                slutMonth = købsdatoSplitter[1];
                slutDay = købsdatoSplitter[0];
            }catch(Exception ex)
            {
                Debug.WriteLine("Fejl i convert slut dato "+  ex.Message);
            }
        }
    }}
