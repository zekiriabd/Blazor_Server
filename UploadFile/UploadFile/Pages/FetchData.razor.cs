using Microsoft.AspNetCore.Components.Forms;
using Sylvan.Data.Excel;
using System.IO;

namespace UploadFile.Pages
{
    public partial class FetchData
    {
        private bool isUploading = false;
        private string dropClass = string.Empty;
        private string ErrorMessage = string.Empty;
        private string Result { get; set; }

        private async Task AddFilesToQueue(InputFileChangeEventArgs e)
        {
            try
            {
                dropClass = string.Empty;
                ErrorMessage = string.Empty;

                if (e.FileCount > 1)
                {
                    ErrorMessage = $"A maximum of 1 is allowed, you have selected {e.FileCount} files!";
                }
                else
                {
                    isUploading = true;
                    await using FileStream fs = new($"c:\\MyImages\\{e.File.Name}", FileMode.Create);
                    await e.File.OpenReadStream().CopyToAsync(fs);
                    fs.Flush();
                    fs.Close();

                    var reader = ExcelDataReader.Create($"c:\\MyImages\\{e.File.Name}");
                    var sheet = reader.WorksheetName;
                    while (reader.Read())
                    {
                        Result = Result +" , " + reader.GetString(0);
                    }

                    isUploading = false;
                    StateHasChanged();
                }
            }
            catch(Exception ex)
            {
                var x = ex.Message;
            }
        } 

        void HandleDragEnter()
        {
            dropClass = "dropzone-active";
        }
        void HandleDragLeave()
        {
            dropClass = string.Empty;
        }
    }
}
