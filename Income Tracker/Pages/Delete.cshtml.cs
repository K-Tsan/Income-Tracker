using Income_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;

namespace Income_Tracker.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [BindProperty]
        public IncomeTrackerModel IncomeTracker {  get; set; }

        public DeleteModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet(int id)
        {
            IncomeTracker = GetById(id);
            return Page();
        }

        private IncomeTrackerModel GetById(int id)
        {
            var incomeTrackerRecord = new IncomeTrackerModel();
            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = 
                    $"SELECT * FROM income_tracker WHERE Id = {id}";
                SqliteDataReader reader = tableCmd.ExecuteReader();

                while (reader.Read()) 
                {
                    incomeTrackerRecord.Id = reader.GetInt32(0);
                    incomeTrackerRecord.Date = DateTime.Parse(reader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat);
                    incomeTrackerRecord.Quantity = reader.GetDecimal(2);
                }

                return incomeTrackerRecord;

            }
        }

        public IActionResult OnPost(int id)
        {
            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"DELETE from income_tracker WHERE Id = {id}";
                tableCmd.ExecuteNonQuery();
            }

            return RedirectToPage("./Index");
        }
    }
}
