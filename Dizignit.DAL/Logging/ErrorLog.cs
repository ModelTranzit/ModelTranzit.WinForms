using Azure.Core;
using Dizignit.Core.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Dizignit.DAL.Logging
{
    public class ErrorLog : ILoggable
    {
        private string _requestID { get; set; }
        private string _errorMessage { get; set; }
        private string _stackTrace { get; set; }
        private string _connectionString { get; set; }
        private DateTime _errorDateTimeUTC { get; set; }
        public ErrorLog(string requestId, string errorMessage, string StackTrace)
        {
            _connectionString = Environment.GetEnvironmentVariable("SQL-ModelTranzitConnectionString");
            _requestID = requestId;
            _errorMessage = errorMessage;
            _stackTrace = StackTrace;
            _errorDateTimeUTC = DateTime.UtcNow;
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
                    using (SqlCommand command = new SqlCommand("ErrorLogInsert", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters
                        command.Parameters.Add(new SqlParameter("@ErrorMessage", SqlDbType.NVarChar));
                        command.Parameters["@ErrorMessage"].Value = _errorMessage;

                        command.Parameters.Add(new SqlParameter("@StackTrace", SqlDbType.NVarChar));
                        command.Parameters["@StackTrace"].Value = _stackTrace;

                        command.Parameters.Add(new SqlParameter("@RequestID", SqlDbType.VarChar));
                        command.Parameters["@RequestID"].Value = _requestID;

                        // Execute the stored procedure
                        command.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    var errorLog = new ErrorLog(_requestID, ex.Message, ex.StackTrace);
                    errorLog.Log();
                    return false;
                }
            }
        }
    }
}
