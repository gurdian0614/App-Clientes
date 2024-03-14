using App_Clientes.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Clientes.Services
{
    public class ClienteService
    {
        private readonly SQLiteConnection _dbConnection;

        public ClienteService()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Cliente.db3");
            _dbConnection = new SQLiteConnection(dbPath);
            _dbConnection.CreateTable<Cliente>();
        }

        /// <summary>
        /// Obtiene el listado de clientes
        /// </summary>
        /// <returns>Listado de clientes</returns>
        public List<Cliente> GetAll()
        {
            return _dbConnection.Table<Cliente>().ToList();
        }

        /// <summary>
        /// Agrega nuevo registro
        /// </summary>
        /// <param name="cliente">Objeto a guatrdar en la db</param>
        /// <returns>Numero mayor que cero si se guardó correctamente</returns>
        public int Insert(Cliente cliente)
        {
            return _dbConnection.Insert(cliente);
        }

        /// <summary>
        /// Atualiza registro existente
        /// </summary>
        /// <param name="cliente">Objeto a actualizar</param>
        /// <returns>Numero mayor que cero si se actualizó correctamente</returns>
        public int Update(Cliente cliente) { 
            return _dbConnection.Update(cliente);
        }

        /// <summary>
        /// Elimina un registro
        /// </summary>
        /// <param name="cliente">Objeto a eliminarse</param>
        /// <returns>Numero mayor que cero si se eliminó correctamente</returns>
        public int Delete(Cliente cliente) 
        { 
            return _dbConnection.Delete(cliente); 
        }
    }
}
