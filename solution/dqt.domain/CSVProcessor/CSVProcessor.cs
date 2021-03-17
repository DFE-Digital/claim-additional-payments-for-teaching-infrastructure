using CsvHelper;
using dqt.domain.Blob;
using Npgsql;
using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace dqt.domain
{
    public class CSVProcessor : ICSVProcessor
    {
        private readonly IBlobService _blobService;

        public CSVProcessor(IBlobService blobService)
        {
            _blobService = blobService;
        }

        public async Task SaveCSVDataToDatabase(Stream csvBLOB, string name)
        {

            await using var conn = new NpgsqlConnection(GetConnStr());
            await conn.OpenAsync();
            await ProcessCSVDataAsync(csvBLOB, conn);
            await _blobService.DeleteFile(name);
        }

        private async Task ProcessCSVDataAsync(Stream csvBLOB, NpgsqlConnection conn)
        {
            await TruncateBackupData(conn);
            await SaveCSVtoBackupTable(csvBLOB, conn);
            await CopyDataFromBackuptoOriginalTable(conn);
            await TruncateBackupData(conn);
        }

        private async Task CopyDataFromBackuptoOriginalTable(NpgsqlConnection conn)
        {
            using var tran = await conn.BeginTransactionAsync();
            using var truncateTableCommand = new NpgsqlCommand("TRUNCATE TABLE \"QualifiedTeachers\";", conn);
            await truncateTableCommand.ExecuteNonQueryAsync();

            using var revertBackupCommand = new NpgsqlCommand("INSERT INTO \"QualifiedTeachers\" SELECT * from \"QualifiedTeachersBackup\"", conn);
            await revertBackupCommand.ExecuteNonQueryAsync();
            await tran.CommitAsync();

        }

        private async Task SaveCSVtoBackupTable(Stream csvBLOB, NpgsqlConnection conn)
        {
            try
            {
                using var writer = conn.BeginBinaryImport("COPY \"QualifiedTeachersBackup\" (\"Id\", \"Name\", \"DoB\", \"ITTSubject1Code\", \"ITTSubject2Code\", \"ITTSubject3Code\", \"NINumber\", \"QTSAwardDate\",\"Trn\" , \"ActiveAlert\") FROM STDIN (FORMAT BINARY)");

                using (var memoryStream = new MemoryStream())
                {
                    using var tr = new StreamReader(csvBLOB);
                    using var csv = new CsvReader(tr, CultureInfo.InvariantCulture);
                    var csv_row = new CSVData();
                    var csvRows = csv.EnumerateRecords(csv_row);
                    var i = 0;

                    foreach (var row in csvRows)
                    {
                        i++;
                        writer.StartRow();
                        writer.Write(i, NpgsqlTypes.NpgsqlDbType.Integer);
                        writer.Write(row.name, NpgsqlTypes.NpgsqlDbType.Text);
                        writer.Write(row.dob, NpgsqlTypes.NpgsqlDbType.Timestamp);
                        writer.Write(row.ITTSubject1Code, NpgsqlTypes.NpgsqlDbType.Text);
                        writer.Write(row.ITTSubject2Code, NpgsqlTypes.NpgsqlDbType.Text);
                        writer.Write(row.ITTSubject3Code, NpgsqlTypes.NpgsqlDbType.Text);
                        writer.Write(row.niNumber, NpgsqlTypes.NpgsqlDbType.Text);
                        writer.Write(row.qtsAwardDate, NpgsqlTypes.NpgsqlDbType.Timestamp);
                        writer.Write(row.trn, NpgsqlTypes.NpgsqlDbType.Text);
                        writer.Write(row.ActiveAlert, NpgsqlTypes.NpgsqlDbType.Boolean);
                    }
                }
                await writer.CompleteAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error persisting DQT data set.", ex);
            }
        }

        private async Task TruncateBackupData(NpgsqlConnection conn)
        {
            using var truncateBackupTableCommand = new NpgsqlCommand("TRUNCATE TABLE \"QualifiedTeachersBackup\";", conn);
            await truncateBackupTableCommand.ExecuteNonQueryAsync();
        }

        private string GetConnStr()
        {
            var server = Environment.GetEnvironmentVariable("DatabaseServerName");
            var database = Environment.GetEnvironmentVariable("DatabaseName");
            var username = Environment.GetEnvironmentVariable("DatabaseUsername");
            var password = Environment.GetEnvironmentVariable("DatabasePassword");
            return @$" Server={server};Database={database};Port=5432;User Id={username};Password={password};Ssl Mode=Require;";
        }
    }
}
