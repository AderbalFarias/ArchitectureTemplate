using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace ArchitectureTemplate.Infraestrutura.CrossCutting.Support.Extensions
{
    public static class LogFile
    {
        public static void Create(Exception exception, string filePath)
        {
            try
            {
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                var dt = DateTime.Now;

                var fileName = $"{dt:yyyyMMdd-HHmmss}-{exception.GetType().Name}.txt";

                fileName = Path.Combine(filePath, fileName);

                try
                {
                    using (var sw = new StreamWriter(fileName, true, Encoding.GetEncoding("ISO-8859-1")))
                    {
                        sw.Write(exception.ToString(), 0, exception.ToString().Length);
                    }
                }
                catch (Exception)
                {
                    var bf = new BinaryFormatter();
                    using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
                    {
                        bf.Serialize(fs, exception);
                    }
                }
            }
            catch
            {
                // ignored
            }
        }
    }
}
