using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.Types;

namespace ProyectoPrograIV
{
    public class Cita : INotifyPropertyChanged
    {
        int Id_cita;
        int Id_medico;
        string Nombre_Usuario;
        string Nombre_Medico;
        MySql.Data.Types.MySqlDateTime Fecha;
        TimeSpan Tiempo;

        public int Id_cita1 { get => Id_cita; set => Id_cita = value; }
        public int Id_medico1 { get => Id_medico; set => Id_medico = value; }
        public string Nombre_Usuario1 { get => Nombre_Usuario; set => Nombre_Usuario = value; }
        public string Nombre_Medico1 { get => Nombre_Medico; set => Nombre_Medico = value; }
        
        public TimeSpan Tiempo1 { get => Tiempo; set => Tiempo = value; }
        public MySqlDateTime Fecha1 { get => Fecha; set => Fecha = value; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
