using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FilesQueueConsumer.DTO
{
    public class FileDTO
    {
        public string filename { get; set; }
        public string NomeMaquina { get; set; }
        public string NomeAplicacao { get; set; }
        public DateTime Data { get; set; }

        public FileDTO()
        {
        }

        public void  ValidarInformacao()
        {
            var nameWithouExtention = filename.Split('.');
            var splitValues = nameWithouExtention[0].Split('_');
            NomeMaquina = splitValues[0];
            NomeAplicacao = splitValues[1];
            var timeString = splitValues[2];
            //No exemplo a string de data ficaria 2021101131559
            //senddo 2021(ano) 10(mes) 11(dia) 31(hora) 55(min) 9(segundo)
            //Hora como 31 não faz sentido então segui com um formato yyyyMMddHHmmss
            Data = DateTime.ParseExact(timeString, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            //Valida regex NomeMaquina
            if (!(Regex.IsMatch(NomeMaquina, @"^[a-zA-Z0-9]+$"))){
                throw new Exception("Formato invalido para nome máquina");
            }
        }

    }
}
