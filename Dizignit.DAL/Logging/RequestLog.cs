using Dizignit.Core.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Dizignit.DAL.Logging
{
    public class RequestLog : ILoggable
    {
        public  string RequestID { get; private set; }
        private int _errorCode { get; set; }
        private string _connectionString { get; set; }
        public DateTime RequestDateTime { get; set; }

        public RequestLog()
        {
            RequestID = Guid.NewGuid().ToString();
            _errorCode = -1; // assume bad
            _connectionString = Environment.GetEnvironmentVariable("SQL-ModelTranzitConnectionString");
        }

        public bool Log()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Create the SqlCommand object
                    using (SqlCommand command = new SqlCommand("RequestLogInsert", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters
                        command.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.NVarChar));
                        command.Parameters["@ErrorCode"].Value = _errorCode;

                        // Add parameters
                        command.Parameters.Add(new SqlParameter("@RequestID", SqlDbType.VarChar));
                        command.Parameters["@RequestID"].Value = RequestID;

                        // Execute the stored procedure
                        command.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    var errorlog = new ErrorLog(RequestID, ex.Message, ex.StackTrace);
                    errorlog.Log();
                    Console.WriteLine($"An error occurred while logging the transaction: {ex.Message}");
                    return false;
                }
            }
        }
    }
}
