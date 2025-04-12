using Dizignit.Core;
using Dizignit.Core.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Dizignit.DAL
{
    public class SQLLogger : ILogger, ILogBytes
    {
        private Trans _trans;
        private readonly string _connectionString;
        private readonly ILogger _logger;   

        public SQLLogger()
        {
            _trans = new Trans();
            // Constructor should be called as early as often to get accurate stat date and time 
            _connectionString = Environment.GetEnvironmentVariable("SQL-ModelTranzitConnectionString");
            _logger = this;
        }

        private bool _log()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Create the SqlCommand object
                    using (SqlCommand command = new SqlCommand("TransLogInsert", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters
                        command.Parameters.Add(new SqlParameter("@UTCTransStartUTC", SqlDbType.DateTime));
                        command.Parameters["@UTCTransStartUTC"].Value = _trans.UTCTransStartUTC;

                        command.Parameters.Add(new SqlParameter("@RawBitmapImage", SqlDbType.VarBinary));
                        command.Parameters["@RawBitmapImage"].Value = _trans.BinaryData;

                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                        Console.WriteLine("Transaction logged successfully.");
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while logging the transaction: {ex.Message}");
                    return false;
                }
            }
        }

        public void Log(byte[] message)
        {
            _trans.BinaryData = message;
            _logger.Log();
        }

        public bool Log()
        {
            return _log();
        }
    }
}


