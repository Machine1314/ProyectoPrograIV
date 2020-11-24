namespace ProyectoPrograIV
{
    public static class Sesion
    {
        public static string Nombre { get; set; }
        public static string Apellido { get; set; }
        public static string Mail { get; set; }
        public static string Cedula { get; set; }
        public static int Id_user { get; set; }
        public static int Id_cita { get; set; }
        public static int Id_medico { get; set; }
        public static int Id_especialidad { get; set; }
        public static int Id_Procedimiento { get; set; }
        public static void Clear()
        {
            Nombre = null;
            Apellido = null;
            Mail = null;
            Cedula = null;
            Id_user = 0;
            Id_cita = 0;
            Id_medico = 0;
            Id_especialidad = 0;
            Id_Procedimiento = 0;
        }
    }
}
