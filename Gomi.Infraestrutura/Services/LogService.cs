using System;
using System.Globalization;
using System.IO;

namespace Gomi.Infraestrutura.Services
{
    public static class LogService
    {
        private static string _sLogFormat;
        private static string _sErrorTime;
        public static string Arquivo { get; set; }
        private const string SPathName = "C:\\Log\\";

        public static void RegistrarExcecao(Exception exception)
        {
            CriarArquivo();
            try
            {
                var sw = new StreamWriter(Arquivo, true);
                sw.WriteLine("##################################################");
                sw.WriteLine(_sLogFormat + exception);
                sw.Flush();
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static void CriarArquivo()
        {
            try
            {
                _sLogFormat = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " ==> ";
                var sYear = DateTime.Now.Year.ToString(CultureInfo.InvariantCulture);
                var sMonth = DateTime.Now.Month.ToString(CultureInfo.InvariantCulture);
                var sDay = DateTime.Now.Day.ToString(CultureInfo.InvariantCulture);
                _sErrorTime = $"{sDay}-{sMonth}-{sYear}";
                Arquivo = $"{SPathName}{_sErrorTime}.log";
                if (!Directory.Exists(SPathName))
                    Directory.CreateDirectory(SPathName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
