using CsvHelper;
using dqt.datalayer.Model;
using dqt.datalayer.Repository;
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
        private readonly IConfigSettings _configSettings;
        private readonly IRepository<QualifiedTeacher> repo;

        public CSVProcessor(IBlobService blobService, IConfigSettings configSettings, IRepository<QualifiedTeacher> repo)
        {
            _blobService = blobService;
            _configSettings = configSettings;
            this.repo = repo;
        }

        public async Task SaveCSVDataToDatabase(Stream csvBLOB, string name)
        {
            await repo.SetUpDB();
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

            using var revertBackupCommand = new NpgsqlCommand("INSERT INTO \"QualifiedTeachers\" ( \"Id\", \"Trn\", \"Name\", \"DoB\", \"NINumber\", \"QTSAwardDate\", \"ITTSubject1Code\", \"ITTSubject2Code\", \"ITTSubject3Code\", \"ActiveAlert\", \"QualificatonName\", \"ITTStartDate\") SELECT * from \"QualifiedTeachersBackup\"", conn);
            await revertBackupCommand.ExecuteNonQueryAsync();
            await tran.CommitAsync();

        }

        private async Task SaveCSVtoBackupTable(Stream csvBLOB, NpgsqlConnection conn)
        {
            try
            {
                using var writer = conn.BeginBinaryImport("COPY \"QualifiedTeachersBackup\" (\"Id\", \"Name\", \"DoB\", \"ITTSubject1Code\", \"ITTSubject2Code\", \"ITTSubject3Code\", \"NINumber\", \"QTSAwardDate\",\"Trn\" , \"ActiveAlert\", \"QualificatonName\", \"ITTStartDate\") FROM STDIN (FORMAT BINARY)");

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
                        writer.Write(row.fullname, NpgsqlTypes.NpgsqlDbType.Text);
                        writer.Write(row.birthdate, NpgsqlTypes.NpgsqlDbType.Date);
                        writer.Write(row.itt_sub1value, NpgsqlTypes.NpgsqlDbType.Text);
                        writer.Write(row.itt_sub2value, NpgsqlTypes.NpgsqlDbType.Text);
                        writer.Write(row.itt_sub3value, NpgsqlTypes.NpgsqlDbType.Text);
                        writer.Write(row.dfeta_ninumber, NpgsqlTypes.NpgsqlDbType.Text);
                        writer.Write(row.dfeta_qtsdate, NpgsqlTypes.NpgsqlDbType.Date);
                        writer.Write(row.dfeta_trn, NpgsqlTypes.NpgsqlDbType.Text);
                        writer.Write(row.active_alert, NpgsqlTypes.NpgsqlDbType.Boolean);
                        writer.Write(row.qual_name, NpgsqlTypes.NpgsqlDbType.Text);
                        writer.Write(row.itt_startdate, NpgsqlTypes.NpgsqlDbType.Date);
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
            return @$"Server={_configSettings.DatabaseServerName};Database={_configSettings.DatabaseName};Port=5432;User Id={_configSettings.DatabaseUsername};Password={_configSettings.DatabasePassword};Ssl Mode=Require;";
        }
    }
}
