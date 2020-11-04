using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoPrograIV
{
    class Sesion
    {
        private string nombre;
        private string apellido;
        private string mail;
        private  string cedula;
        private int id_user;
        private  int id_cita;
        private int id_medico;
        private int id_especialidad;

        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public string Mail { get => mail; set => mail = value; }

     

        public int Id_user { get => id_user; set => id_user = value; }

        public int Id_medico { get => id_medico; set => id_medico = value; }
        public int Id_especialidad { get => id_especialidad; set => id_especialidad = value; }
        public string Cedula { get => cedula; set => cedula = value; }
        public int Id_cita { get => id_cita; set => id_cita = value; }

        public Sesion()
        {
        }
    }
}
