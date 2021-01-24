using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace FileToObject.Extensions
{
    public static class FileCharacterExtensions
    {
        public static string RemoveDiatrics(this string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                return new string(text.ToString().Normalize(NormalizationForm.FormD).Where(ch => char.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark).ToArray());
            }

            return text;
        }

        public static string RemoverCaracteresEspeciais(this string texto)
        {
            string caracteres = @",\\ºª”";
            for (int i = 0; i < caracteres.Length; i++)
                texto = texto.Replace(caracteres[i].ToString(), String.Empty).Trim();

            //texto = texto.Replace("0x07", "");

            string comAcentos = "'™®!@#$%¨&+,*()?:;={}][/ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç /";
            string semAcentos = "                        AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc  ";
            
            for (int i = 0; i < comAcentos.Length; i++)
                texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString()).Trim();

            return texto;
        }
    }
}
