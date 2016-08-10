using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

public static class Extensions
{
    public static void xDropDownDegerSec(this DropDownList ddl, string deger)
    {
        ListItem item = ddl.Items.FindByValue(deger);
        if (item != null)
        {
            ddl.SelectedIndex = ddl.Items.IndexOf(item);
        }
    }

    public static void xDropDownDoldur(this DropDownList ddl, object data, string text, string value, string ilkDeger)
    {
        if ((((ddl != null) && (data != null)) && (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(value))) && !string.IsNullOrEmpty(ilkDeger))
        {
            ddl.Items.Clear();
            try
            {
                ddl.DataSource = data;
                ddl.DataTextField = text;
                ddl.DataValueField = value;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem(ilkDeger, "0"));
            }
            catch
            {
            }
        }
    }

    public static DataTable xKeyFieldEkle(this DataTable dt, string columnName)
    {
        if (!dt.Columns.Contains(columnName))
        {
            DataColumn column = new DataColumn(columnName, typeof(int));
            dt.Columns.Add(column);
        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i][columnName] = i + 1;
        }
        return dt;
    }

    public static string xListeNoktaliBirlestir(this List<int> liste)
    {
        string str = string.Empty;
        try
        {
            for (int i = 0; i < liste.Count; i++)
            {
                str = str + liste[i].ToString();
                if (i != (liste.Count - 1))
                {
                    str = str + ".";
                }
            }
        }
        catch
        {
        }
        return str;
    }

    public static bool? xToBoolean(this object o)
    {
        bool? nullable = null;
        try
        {
            if ((o != null) && (o != DBNull.Value))
            {
                nullable = new bool?(Convert.ToBoolean(o));
            }
        }
        catch
        {
        }
        return nullable;
    }

    public static bool xToBooleanDefault(this object o)
    {
        bool flag = false;
        try
        {
            if ((o != null) && (o != DBNull.Value))
            {
                flag = Convert.ToBoolean(o);
            }
        }
        catch
        {
        }
        return flag;
    }

    public static decimal xToCompoundInterest(this decimal price, int installment, decimal interest)
    {
        return 0M;
    }

    public static DateTime? xToDateTime(this object o)
    {
        DateTime? nullable = null;
        try
        {
            if ((o != null) && (o != DBNull.Value))
            {
                nullable = new DateTime?(Convert.ToDateTime(o));
            }
        }
        catch
        {
        }
        return nullable;
    }

    public static DateTime xToDateTimeDefault(this object o)
    {
        DateTime time = new DateTime(0x76c, 1, 1);
        try
        {
            if ((o != null) && (o != DBNull.Value))
            {
                time = Convert.ToDateTime(o);
            }
        }
        catch
        {
        }
        return time;
    }

    public static decimal? xToDecimal(this object o)
    {
        decimal? nullable = null;
        try
        {
            if ((o != null) && (o != DBNull.Value))
            {
                nullable = new decimal?(Convert.ToDecimal(o));
            }
        }
        catch
        {
        }
        return nullable;
    }

    public static decimal xToDecimalDefault(this object o)
    {
        decimal num = 0M;
        try
        {
            if ((o != null) && (o != DBNull.Value))
            {
                num = Convert.ToDecimal(o);
            }
        }
        catch
        {
        }
        return num;
    }

    public static long xToFileIdByFileName(this string fileName)
    {
        return fileName.Substring(1, fileName.IndexOf(')') - 1).xToInt64Default();
    }

    public static List<long> xToFileIdListForHtml(this string html)
    {
        List<long> list = null;
        try
        {
            MatchCollection matchs = Regex.Matches(html, "<img.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase);
            if ((matchs == null) || (matchs.Count <= 0))
            {
                return list;
            }
            list = new List<long>();
            foreach (Match match in matchs)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(match.Groups[1].Value);
                list.Add(fileNameWithoutExtension.xToFileIdByFileName());
            }
        }
        catch
        {
            throw;
        }
        return list;
    }

    public static string xToFileNameFormat(this string fileName, long fileId)
    {
        return string.Format("({0})-{1}", fileId, fileName);
    }

    public static Guid? xToGuid(this string str)
    {
        Guid? nullable = null;
        try
        {
            if (!string.IsNullOrEmpty(str))
            {
                nullable = new Guid?(Guid.Parse(str));
            }
        }
        catch
        {
        }
        return nullable;
    }

    public static Guid xToGuidDefault(this string str)
    {
        Guid empty = Guid.Empty;
        try
        {
            if (!string.IsNullOrEmpty(str))
            {
                empty = Guid.Parse(str);
            }
        }
        catch
        {
        }
        return empty;
    }

    public static int? xToInt(this object o)
    {
        int? nullable = null;
        try
        {
            if ((o != null) && (o != DBNull.Value))
            {
                nullable = new int?(Convert.ToInt32(o));
            }
        }
        catch
        {
        }
        return nullable;
    }

    public static short? xToInt16(this object o)
    {
        short? nullable = null;
        try
        {
            if ((o != null) && (o != DBNull.Value))
            {
                nullable = new short?(Convert.ToInt16(o));
            }
        }
        catch
        {
        }
        return nullable;
    }

    public static short xToInt16Default(this object o)
    {
        short num = 0;
        try
        {
            if ((o != null) && (o != DBNull.Value))
            {
                num = Convert.ToInt16(o);
            }
        }
        catch
        {
        }
        return num;
    }

    public static int? xToInt32(this object o)
    {
        int? nullable = null;
        try
        {
            if ((o != null) && (o != DBNull.Value))
            {
                nullable = new int?(Convert.ToInt32(o));
            }
        }
        catch
        {
        }
        return nullable;
    }

    public static int xToInt32Default(this object o)
    {
        int num = 0;
        try
        {
            if ((o != null) && (o != DBNull.Value))
            {
                num = Convert.ToInt32(o);
            }
        }
        catch
        {
        }
        return num;
    }

    public static long? xToInt64(this object o)
    {
        long? nullable = null;
        try
        {
            if ((o != null) && (o != DBNull.Value))
            {
                nullable = new long?(Convert.ToInt64(o));
            }
        }
        catch
        {
        }
        return nullable;
    }

    public static long xToInt64Default(this object o)
    {
        long num = 0L;
        try
        {
            if ((o != null) && (o != DBNull.Value))
            {
                num = Convert.ToInt64(o);
            }
        }
        catch
        {
        }
        return num;
    }

    public static int xToIntDefault(this object o)
    {
        int num = 0;
        try
        {
            if ((o != null) && (o != DBNull.Value))
            {
                num = Convert.ToInt32(o);
            }
        }
        catch
        {
        }
        return num;
    }

    public static string xToLegalCardFormat(this string cardNumber)
    {
        string str = string.Empty;
        if (cardNumber.Length != 0x10)
        {
            return str;
        }
        string str2 = cardNumber.Substring(0, 4);
        string str3 = cardNumber.Substring(4, 4);
        string str4 = cardNumber.Substring(12, 4);
        return (str2 + "-" + str3.Substring(0, 2) + "**-****-" + str4);
    }

    public static string xToMD5(this string veri)
    {
        MD5 md = MD5.Create();
        byte[] bytes = Encoding.ASCII.GetBytes(veri);
        byte[] buffer2 = md.ComputeHash(bytes);
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < buffer2.Length; i++)
        {
            builder.Append(buffer2[i].ToString("x2"));
        }
        return builder.ToString();
    }

    public static string xToSeoUrl(this string url)
    {
        string str = string.Empty;
        try
        {
            url = url.Trim().ToLower();
            url = Regex.Replace(url, "ş", "s");
            url = Regex.Replace(url, "ı", "i");
            url = Regex.Replace(url, "ö", "o");
            url = Regex.Replace(url, "ü", "u");
            url = Regex.Replace(url, "ç", "c");
            url = Regex.Replace(url, "ğ", "g");
            url = Regex.Replace(url, @"[^a-z0-9]", " ");
            url = Regex.Replace(url, @"\s+", " ");
            url = url.Trim();
            url = Regex.Replace(url, " ", "-");
            str = url;
        }
        catch
        {
        }
        return str;
    }

    public static decimal xToSimpleInterest(this decimal amount, int installment, decimal interest)
    {
        double num = Convert.ToDouble(amount);
        double num2 = Convert.ToDouble(interest) / 100.0;
        double num3 = (num * num2) * installment;
        return Convert.ToDecimal(num3);
    }

    public static decimal xToTaxAmount(this decimal amount, decimal taxRate)
    {
        double num = Convert.ToDouble(taxRate);
        return ((Convert.ToDouble(amount) * num) / 100.0).xToDecimalDefault();
    }

    public static string xToUnicodeForTrCharacter(this string text)
    {
        text = text.Replace("İ", "İ");
        text = text.Replace("ı", "ı");
        text = text.Replace("Ş", "Ş");
        text = text.Replace("ş", "ş");
        text = text.Replace("Ğ", "Ğ");
        text = text.Replace("ğ", "ğ");
        text = text.Replace("\x00d6", "\x00d6");
        text = text.Replace("\x00f6", "\x00f6");
        text = text.Replace("\x00e7", "\x00e7");
        text = text.Replace("\x00c7", "\x00c7");
        text = text.Replace("\x00fc", "\x00fc");
        text = text.Replace("\x00dc", "\x00dc");
        return text;
    }

    public static decimal xToWithoutTaxAmount(this decimal amount, decimal taxRate)
    {
        double num = Convert.ToDouble(taxRate);
        return (Convert.ToDouble(amount) / (1.0 + (num / 100.0))).xToDecimalDefault();
    }

    public static decimal xToWithTaxAmount(this decimal amount, decimal taxRate)
    {
        double num = Convert.ToDouble(taxRate);
        double num2 = Convert.ToDouble(amount);
        return (num2 + ((num2 * num) / 100.0)).xToDecimalDefault();
    }

    public static byte[] xStringToBytes(this string str)
    {
        byte[] bytes = new byte[str.Length * sizeof(char)];
        System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
        return bytes;
    }

    public static string xByteToString(this byte[] bytes)
    {
        char[] chars = new char[bytes.Length / sizeof(char)];
        System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
        return new string(chars);
    }

    public static int xToPageCount(this int totalCount, int pageSize)
    {
        return (int)Math.Ceiling((double)totalCount / pageSize);
    }

    /// <summary>
    /// Telefon numarası üzerinde bulunan () gibi değerleri temizler ve boşlukları alır.
    /// </summary>
    public static string xToCleanPhoneNumber(this string phoneNumber)
    {
        string phone = null;

        if (!String.IsNullOrEmpty(phoneNumber))
        {
            phone = phoneNumber.Replace("(", "").Replace(")", "").Trim().TrimStart().TrimEnd();
        }

        return phone;
    }

    /// <summary>
    /// Gönderilen eMail adresinin geçerli olup olmadığını kontrol eder.
    /// </summary>
    public static bool xIsValidMail(this string eMail)
    {
        string pattern = @"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$";
        return Regex.IsMatch(eMail, pattern);
    }

    /// <summary>
    /// Gönderilen ip adresinin geçerli olup olmadığını kontrol eder.
    /// </summary>
    public static bool xIsValidIpAddress(this string ipAddress)
    {
        bool isValidIp = true;

        try
        {
            System.Net.IPAddress.Parse(ipAddress);
        }
        catch
        {
            isValidIp = false;
        }

        return isValidIp;
    }

    /// <summary>
    /// Gönderilen TCKN bilgisinin geçerli olup olmadığını kontrol eder.m
    /// </summary>
    public static bool xIsValidTCKN(this string tckNo)
    {
        ulong num;
        if (!tckNo.Length.Equals(11))
        {
            return false;
        }
        if (!ulong.TryParse(tckNo, out num))
        {
            return false;
        }
        if ((num < 0x2540be400L) || (num > 0x174876e7ffL))
        {
            return false;
        }
        if ((num % ((ulong)2L)) != 0L)
        {
            return false;
        }
        byte num2 = 0;
        for (int i = 0; i < 10; i++)
        {
            num2 = (byte)(num2 + Convert.ToByte(tckNo.Substring(i, 1)));
        }
        int num4 = num2 % 10;
        if (!num4.Equals((int)Convert.ToByte((ulong)(num % ((ulong)10L)))))
        {
            return false;
        }
        return true;
    }
}
