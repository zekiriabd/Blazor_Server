using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.Data;

namespace Test
{
    public partial class ExportTestxController : ExportController
    {
        private readonly TestxContext context;
        private readonly TestxService service;
        public ExportTestxController(TestxContext context, TestxService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/Testx/categories/csv")]
        [HttpGet("/export/Testx/categories/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportCategoriesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCategories(), Request.Query), fileName);
        }

        [HttpGet("/export/Testx/categories/excel")]
        [HttpGet("/export/Testx/categories/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportCategoriesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCategories(), Request.Query), fileName);
        }
        [HttpGet("/export/Testx/roles/csv")]
        [HttpGet("/export/Testx/roles/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportRolesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetRoles(), Request.Query), fileName);
        }

        [HttpGet("/export/Testx/roles/excel")]
        [HttpGet("/export/Testx/roles/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportRolesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetRoles(), Request.Query), fileName);
        }
        [HttpGet("/export/Testx/users/csv")]
        [HttpGet("/export/Testx/users/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportUsersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetUsers(), Request.Query), fileName);
        }

        [HttpGet("/export/Testx/users/excel")]
        [HttpGet("/export/Testx/users/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportUsersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetUsers(), Request.Query), fileName);
        }
    }
}
