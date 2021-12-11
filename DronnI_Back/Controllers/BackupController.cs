using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DronnI_Back.Models;
using DronnI_Back.Models.DbModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Data.SqlClient;

namespace DronnI_Back.Controllers
{
    [Route("api/[controller]")]
    public class BackupController : Controller
    {
        IWebHostEnvironment _appEnviropment;
        ApplicationContext appCtx;
        IConfiguration _configuration;
        const string DbName = "DronnI";
        string connectionString;

        [HttpPost("Test")]
        public IActionResult Test()
        {
            return BadRequest(new { errorText = "Invalid OwnerId" });
        }
        public BackupController(IConfiguration configuration, IWebHostEnvironment appEnvironment, ApplicationContext ctx)
        {
            _configuration = configuration;
            _appEnviropment = appEnvironment;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
            appCtx = ctx;
        }

        [HttpPost("addBackup")]
        public void BackUpDataBase()
        {
            const string rootPath = "BackUps";
            var date = DateTime.Now;
            string fileName = "BackUp" + date.TimeOfDay.ToString().Replace(":", "_").Replace(".", "_") + ".bak";
            string path = String.Format("{0}\\{1}\\", _appEnviropment.WebRootPath, rootPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path += fileName;
            string queryString = $@"BACKUP DATABASE {DbName} TO DISK = '{path}'";

            appCtx.Add(new Backup
            {
                Name = fileName,
                Path = path,
                CreatingTime = date
            });
            appCtx.SaveChanges();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteReader();
            }


        }
        [HttpPost("updateBackup")]
        public void RestoreDataBase(int id)
        {
            Backup backup = appCtx.Backups.FirstOrDefault(d => d.Id == id);
            if (backup != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string useMaster = "USE master";
                    SqlCommand useMasterCommand = new SqlCommand(useMaster, connection);

                    string alterFirst = $@"ALTER DATABASE {DbName} SET Single_User WITH Rollback Immediate";
                    SqlCommand alterfirstCommand = new SqlCommand(alterFirst, connection);

                    string restore = string.Format("RESTORE DATABASE {0} FROM DISK = '{1}'", DbName, backup.Path);
                    SqlCommand restoreCommand = new SqlCommand(restore, connection);

                    string alterSecond = $@"ALTER DATABASE {DbName} SET Multi_User";
                    SqlCommand alterSecondCommand = new SqlCommand(alterSecond, connection);

                    connection.Open();

                    useMasterCommand.ExecuteNonQuery();
                    alterfirstCommand.ExecuteNonQuery();
                    restoreCommand.ExecuteNonQuery();
                    alterSecondCommand.ExecuteNonQuery();

                    connection.Close();
                }
            }
        }
    }
}
