using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using Threats.Data.Entities;

namespace Threats.Data.Database;

public class DatabaseConnection
{
    private readonly string connectionString;


    public DatabaseConnection(string path)
    {
        connectionString = new SQLiteConnectionStringBuilder("")
        {
            DataSource = path,
            ReadOnly = true,

        }.ToString();
    }

    public void Load()
    {
        var connection = new SQLiteConnection(connectionString.ToString());
        connection.Open();

        LoadNegativeTypes(connection);

        connection.Close();
    }

    private void LoadNegativeTypes(SQLiteConnection connection)
    {
        var adapter = new SQLiteDataAdapter("SELECT * from negative_types", connection);
        var dataset = new DataSet();
        adapter.Fill(dataset);

        var table = dataset.Tables[0];

        var types = new List<NegativeType>();
        foreach (DataRow row in table.Rows)
        {
            var type = new NegativeType((long)row[0], (string)row[1]);
            types.Add(type);
        }

        Console.WriteLine(types);
    }
}
