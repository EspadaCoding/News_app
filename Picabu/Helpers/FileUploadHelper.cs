using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Picabu.Helpers
{
    public static class FileUploadHelper
    {
        public static async Task<string> UploadAsync(IFormFile file)
        {
            if (file != null && file.Length < 1073741821)
            {
                var filename = $"{Guid.NewGuid()}+{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                using (var fs = new FileStream(@$"wwwroot/uploads/{filename}", FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                    return $@"/uploads/{filename}";
                }
            }
            else
            {
                throw new Exception("File was not uploaded!");
            }

        }
    }
}
