using System;
using System.IO;
using System.Diagnostics;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Filer
{
    [Serializable]
    public class UserPrefs
    {
        public Font font { get; set; }
        public Color color { get; set; }
        public bool flag { get; set; }
        private static byte[] key = new byte[32];
        private static byte[] iv = new byte[16];
        public object password { get; set; }
        public string login { get; set; }
        public UserPrefs()
        {

        }
        public void SavePrefs()
        {
            BinaryFormatter binformat = new BinaryFormatter();
            Stream fStream = new FileStream("pref.dat", FileMode.Create, FileAccess.Write, FileShare.None);
            binformat.Serialize(fStream, this);
            fStream.Close();
        }
        public static UserPrefs Get()
        {
            UserPrefs prefs = new UserPrefs();
            if (File.Exists("pref.dat"))
            {
                
                Stream fStream = new FileStream("pref.dat", FileMode.Open, FileAccess.Read, FileShare.None);
                BinaryFormatter binformat = new BinaryFormatter();
                prefs = (UserPrefs)binformat.Deserialize(fStream);
                fStream.Close();
            }
            return prefs;
        }
        [OnSerializing]
        internal void OnSerialising(StreamingContext context)
        {
            if (password!=null) password = Encrypt(password);
        }
        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            if (password!=null) password = Decrypt(password);
        }
        static public byte[] Encrypt(object password)
        {
            string pass = password as string;
            byte[] encrypted;
            using (Rijndael rijAlg = Rijndael.Create())
            {
                rijAlg.Key = key;
                rijAlg.IV = iv;
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(pass);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return encrypted;
        }

        static public string Decrypt(object password)
        {
            byte[] pass = password as byte[];
            string plaintext = null;
            using (Rijndael rijAlg = Rijndael.Create())
            {
                rijAlg.Key = key;
                rijAlg.IV = iv;
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
                using (MemoryStream msDecrypt = new MemoryStream(pass))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }
    }
}
