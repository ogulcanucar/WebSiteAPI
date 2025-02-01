using WebSiteAPI.Infrastructure.Operations;

namespace WebSiteAPI.Infrastructure.Services.Storage
{
    public class Storage
    {
        protected delegate bool HasFile(string pathOrContainerName, string fileName);
        protected async Task<string> FileRenameAsync(string pathOrContainerName, string fileName, HasFile hasFileMethod, bool first = true)
        {
            string newFileName = string.Empty;

            if (first)
            {
                string extension = Path.GetExtension(fileName);
                string oldName = Path.GetFileNameWithoutExtension(fileName);
                newFileName = $"{NameOperation.CharacterRegulatory(oldName)}{extension}";
            }
            else
            {
                int indexNo1 = fileName.LastIndexOf('-');
                int indexNo2 = fileName.LastIndexOf('.');

                if (indexNo1 != -1 && indexNo2 != -1 && indexNo1 < indexNo2 - 1)
                {
                    string fileNo = fileName.Substring(indexNo1 + 1, indexNo2 - indexNo1 - 1);
                    if (int.TryParse(fileNo, out int _fileNo))
                    {
                        _fileNo++;
                        newFileName = fileName.Remove(indexNo1 + 1, indexNo2 - indexNo1 - 1).Insert(indexNo1 + 1, _fileNo.ToString());
                    }
                }

                if (string.IsNullOrEmpty(newFileName))
                {
                    newFileName = $"{Path.GetFileNameWithoutExtension(fileName)}-2{Path.GetExtension(fileName)}";
                }
            }

            if (hasFileMethod(pathOrContainerName, newFileName))
            {
                newFileName = await FileRenameAsync(pathOrContainerName, newFileName, hasFileMethod, false);
            }

            return newFileName;
        }



    }
}
