using Dizignit.Core.Enums;
using Dizignit.Core.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Dizignit.DAL.Logging
{
    public class ImageLog : ILoggable
    {
        public string RequestID { get; private set; }
        private DateTime _transStartUTC { get; set; }
        private EImageType _ImageType { get; set; }
        private byte[] _binaryData { get; set; }
        private readonly string _connectionString;
        private ILoggable _log;

        public ImageLog(byte[] binaryData, EImageType imageType, string requestID)
        {
            // this a foriegn key
            RequestID = requestID;

            _connectionString = Environment.GetEnvironmentVariable("SQL-ModelTranzitConnectionString");
            _binaryData = binaryData;
            _transStartUTC = DateTime.UtcNow;
            _ImageType = imageType;
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
                    using (SqlCommand command = new SqlCommand("ImageLogInsert", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters
                        command.Parameters.Add(new SqlParameter("@UTCTransStartUTC", SqlDbType.DateTime));
                        command.Parameters["@UTCTransStartUTC"].Value = _transStartUTC;

                        command.Parameters.Add(new SqlParameter("@RawImage", SqlDbType.VarBinary));
                        command.Parameters["@RawImage"].Value = _binaryData;

                        command.Parameters.Add(new SqlParameter("@FileType", SqlDbType.Int));
                        command.Parameters["@FileType"].Value = _ImageType;

                        command.Parameters.Add(new SqlParameter("@RequestID", SqlDbType.VarChar));
                        command.Parameters["@RequestID"].Value = RequestID;

                        // Execute the stored procedure
                        command.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    var errorLog = new ErrorLog(RequestID, ex.Message, ex.StackTrace);
                    errorLog.Log();
                    return false;
                }
            }
        }
    }
}
