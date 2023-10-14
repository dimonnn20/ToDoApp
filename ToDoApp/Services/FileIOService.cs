using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    internal class FileIOService
    {
        private readonly string PATH;

        public FileIOService(String path)
        {
            PATH = path;
        }
        public BindingList<ToDoModel> LoadData() 
        {
            bool fileExists = File.Exists(PATH);
            if (!fileExists)
            {
                File.CreateText(PATH).Dispose();
                return new BindingList<ToDoModel>();
            }
            using (StreamReader reader = File.OpenText(PATH))
            { 
                string fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<ToDoModel>>(fileText);
            }

        }

        public void SaveData(BindingList<ToDoModel> toDoDataList)
        {
            using (StreamWriter writer =File.CreateText(PATH))
            { 
                string output = JsonConvert.SerializeObject(toDoDataList);
                writer.Write(output);
            }
        }
    }
}
