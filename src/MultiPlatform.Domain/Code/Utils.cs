﻿
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace MultiPlatform.Domain.Code
{
    public static class Utils
    {
      
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
      

        public static String convertToString(this Enum eff)
        {

            return Enum.GetName(eff.GetType(), eff);

        }


        public static string ToJson(this object objectToSave)
        {
            try
            {
                if (objectToSave == null) return null;
                var st = new JsonSerializerSettings();
                st.StringEscapeHandling = StringEscapeHandling.EscapeHtml;
                return JsonConvert.SerializeObject(objectToSave, Formatting.None, st);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public static TJson FromJson<TJson>(this string jsonString)
        {
            try
            {
            

                return JsonConvert.DeserializeObject<TJson>(jsonString);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}
