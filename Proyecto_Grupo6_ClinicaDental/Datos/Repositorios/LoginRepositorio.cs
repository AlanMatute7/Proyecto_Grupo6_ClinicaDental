﻿using Datos.Interfaces;
using Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Datos.Repositorios
{
    public class LoginRepositorio : ILoginRepositorio
    {
        private string CadenaConexion;

        public LoginRepositorio(string _cadenaConexion)
        {
            CadenaConexion = _cadenaConexion;
        }

        private MySqlConnection Conexion()
        {
            return new MySqlConnection (CadenaConexion);
        }

        public async Task<bool> ValidarUsuario(Login login)
        { 
            bool valido = false;
            try
            {
                using MySqlConnection conexion = Conexion();
               await  conexion.OpenAsync();
                string sql = "SELECT 1 FROM usuario WHERE CodigoUsuario = @CodigoUsuario AND Clave = @Clave;";
                valido = await conexion.ExecuteScalarAsync<bool>(sql, new { login.CodigoUsuario, login.Clave });
            }
            catch(Exception ex)
            {

            }
            return valido;
        }
    }
}
