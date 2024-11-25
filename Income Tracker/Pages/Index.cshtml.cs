using Income_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;

namespace Income_Tracker.Pages
{
    public class IndexModel : PageModel
    {
        public List<IncomeTrackerModel> Records { get; set; }

        private readonly IConfiguration _configuration;

        public IndexModel(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }

        public void OnGet()
        {
            Records = GetAllRecords();
        }

        private List<IncomeTrackerModel> GetAllRecords()
        {
            using (var connection = new
                SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"SELECT * FROM income_tracker ";

                var tableData = new List<IncomeTrackerModel>();
                SqliteDataReader reader = tableCmd.ExecuteReader();

                while (reader.Read())
                {
                    tableData.Add(new IncomeTrackerModel
                    {
                        Id = reader.GetInt32(0),
                        Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat),
                        Quantity = reader.GetDecimal(2)
                    });
                }

                return tableData;
            }
        }
    }
}
