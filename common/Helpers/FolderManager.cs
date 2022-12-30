using System.IO;

namespace common.Helpers
{
    public static class FolderManager
    {
        private static string _serverPath;

        private static string _dataFolder = "Data";

        private static string _imagesFolder = "Data\\Images";

        private static string _categoryFolder = "Data\\Images\\Category";

        public static void InitializeFolderManager(string serverPath)
        {
            _serverPath = serverPath;

            CreateDataFolderIfNotExist();

            CreateImagesFolderIfNotExist();

            CreateCategoryFolderIfNotExist();
        }

        private static void CreateDataFolderIfNotExist()
        {
            if (!Directory.Exists(Path.Combine(_serverPath, _dataFolder)))
            {
                Directory.CreateDirectory(Path.Combine(_serverPath, _dataFolder));
            }
        }

        private static void CreateImagesFolderIfNotExist()
        {
            if (!Directory.Exists(Path.Combine(_serverPath, _imagesFolder)))
            {
                Directory.CreateDirectory(Path.Combine(_serverPath, _categoryFolder));
            }
        }

        private static void CreateCategoryFolderIfNotExist()
        {
            if (!Directory.Exists(Path.Combine(_serverPath, _dataFolder)))
            {
                Directory.CreateDirectory(Path.Combine(_serverPath, _dataFolder));
            }
        }

        public static string GetCategoryFolder() => Path.Combine(_serverPath, _categoryFolder);

        public static string Convert(string path, string host) {
            return path.Replace(_serverPath, host).Replace('\\', '/');
        }
    }
}
