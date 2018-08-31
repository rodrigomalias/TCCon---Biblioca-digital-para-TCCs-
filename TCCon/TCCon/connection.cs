using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;

namespace TCCon
{
	public class DatabaseConnection
	{
		private string
			DataSource = "den1.mysql4.gear.host",
			Port = "3306",
			Catalog = "tccondatabase",
			UserId = "tccondatabase",
			Password = "tccon123!";
					
		public MySqlConnection connect()
		{
			return new MySqlConnection(
				@"Data Source='" + DataSource + "';" +
				"port='" + Port + "';" +
				"Initial Catalog='" + Catalog + "'; " +
				"User ID='" + UserId + "'; " +
				"Password='" + Password + "';");
		}
	}
}