using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.SqlServer.Management.Smo;
using Smo = Microsoft.SqlServer.Management.Smo;

namespace Back_Dr.Backup
{
  public  class GenerateScript
    {


        public   void Generate()
        {
            var fileName = @"C:\SYSTEM\backup.txt";
            var connectionString = @"Data Source=localhost\SQLEXPRESS; Database=drsale; Integrated Security=true;";
            var databaseName = "drsale";
            var schemaName = "dbo";
            try
            {
                var server = new Smo.Server(new ServerConnection(new SqlConnection(connectionString)));
                var options = new Smo.ScriptingOptions();
                var databases = server.Databases[databaseName];

                options.FileName = fileName;
                options.EnforceScriptingOptions = true;
                options.WithDependencies = true;
                options.IncludeHeaders = true;
                options.ScriptDrops = false;
 
                options.AppendToFile = true;
                options.ScriptSchema = true;
                options.ScriptData = true;
                options.Triggers = true;
                options.Indexes = true;

                var tables = databases.Tables.Cast<Table>().Where(i => i.Schema == schemaName);
                var views = databases.Views.Cast<View>().Where(i => i.Schema == schemaName);
                var procedures = databases.StoredProcedures.Cast<StoredProcedure>().Where(i => i.Schema == schemaName);
                var trigger = databases.Triggers.Cast<Trigger>();


                Console.WriteLine("SQL Script Generator");

                Console.WriteLine("\nTable Scripts:");
                foreach (Smo.Table table in tables)
                {
                    databases.Tables[table.Name, schemaName].EnumScript(options);
                    Console.WriteLine(table.Name);
                }

                options.ScriptData = false;
                options.WithDependencies = false;

                Console.WriteLine("\nView Scripts:");
                foreach (Smo.View view in views)
                {
                    databases.Views[view.Name, schemaName].Script(options);
                    Console.WriteLine(view.Name);
                }

                Console.WriteLine("\nStored Procedure Scripts:");
                foreach (Smo.StoredProcedure procedure in procedures)
                {
                    databases.StoredProcedures[procedure.Name, schemaName].Script(options);
                    Console.WriteLine(procedure.Name);
                }


                //trigger
                foreach (Smo.Trigger triggerss in trigger)
                {
                    databases.StoredProcedures[triggerss.Name, schemaName].Script(options);
                    Console.WriteLine(triggerss.Name);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occured: {ex.Message}");
            }

        }


    }
}
