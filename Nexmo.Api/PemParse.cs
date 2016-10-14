//// heavily borrowed from:

//**********************************************************************************
//
//OpenSSLKey
// .NET 2.0  OpenSSL Public & Private Key Parser
//
/*
Copyright (c) 2000  JavaScience Consulting,  Michel Gallant

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
//***********************************************************************************
//
//  opensslkey.cs
//
//  Reads and parses:
//    (1) OpenSSL PEM or DER public keys
//    (2) OpenSSL PEM or DER traditional SSLeay private keys (encrypted and unencrypted)
//    (3) PKCS #8 PEM or DER encoded private keys (encrypted and unencrypted)
//  Keys in PEM format must have headers/footers .
//  Encrypted Private Key in SSLEay format not supported in DER
//  Removes header/footer lines.
//  For traditional SSLEAY PEM private keys, checks for encrypted format and
//  uses PBE to extract 3DES key.
//  For SSLEAY format, only supports encryption format: DES-EDE3-CBC
//  For PKCS #8, only supports PKCS#5 v2.0  3des.
//  Parses private and public key components and returns .NET RSA object.
//  Creates dummy unsigned certificate linked to private keypair and
//  optionally exports to pkcs #12
//
// See also: 
//  http://www.openssl.org/docs/crypto/pem.html#PEM_ENCRYPTION_FORMAT 
//**************************************************************************************

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Nexmo.Api
{
    public class PemParse
    {
        private const string pemprivheader = "-----BEGIN RSA PRIVATE KEY-----";
        private const string pemprivfooter = "-----END RSA PRIVATE KEY-----";

        public static RSACryptoServiceProvider DecodePEMKey(string pemstr)
        {
            if (!pemstr.StartsWith(pemprivheader) || !pemstr.EndsWith(pemprivfooter)) return null;
            var pemprivatekey = DecodeOpenSSLPrivateKey(pemstr);
            if (pemprivatekey != null)
                return DecodeRSAPrivateKey(pemprivatekey);
            return null;
        }

        private static byte[] DecodeOpenSSLPrivateKey(string instr)
        {
            var pemstr = instr.Trim();
            byte[] binkey;
            if (!pemstr.StartsWith(pemprivheader) || !pemstr.EndsWith(pemprivfooter))
                return null;

            var sb = new StringBuilder(pemstr);
            sb.Replace(pemprivheader, ""); //remove headers/footers, if present
            sb.Replace(pemprivfooter, "");

            var pvkstr = sb.ToString().Trim(); //get string after removing leading/trailing whitespace

            try
            {
                // if there are no PEM encryption info lines, this is an UNencrypted PEM private key
                binkey = Convert.FromBase64String(pvkstr);
                return binkey;
            }
            catch (FormatException)
            {
                //if can't b64 decode, it must be an encrypted private key
                //Console.WriteLine("Not an unencrypted OpenSSL PEM private key");  
            }

            var str = new StringReader(pvkstr);

            //-------- read PEM encryption info. lines and extract salt -----
            if (!str.ReadLine().StartsWith("Proc-Type: 4,ENCRYPTED"))
                return null;
            var saltline = str.ReadLine();
            if (!saltline.StartsWith("DEK-Info: DES-EDE3-CBC,"))
                return null;
            var saltstr = saltline.Substring(saltline.IndexOf(",") + 1).Trim();
            var salt = new byte[saltstr.Length/2];
            for (var i = 0; i < salt.Length; i++)
                salt[i] = Convert.ToByte(saltstr.Substring(i*2, 2), 16);
            if (str.ReadLine() != "")
                return null;

            //------ remaining b64 data is encrypted RSA key ----
            var encryptedstr = str.ReadToEnd();

            try
            {
                //should have b64 encrypted RSA key now
                binkey = Convert.FromBase64String(encryptedstr);
            }
            catch (FormatException)
            {
                // bad b64 data.
                return null;
            }

            //------ Get the 3DES 24 byte key using PDK used by OpenSSL ----
            throw new NotSupportedException("Encrypted key not supported");
            //////////SecureString despswd = GetSecPswd("Enter password to derive 3DES key==>");
            ////////////Console.Write("\nEnter password to derive 3DES key: ");
            ////////////String pswd = Console.ReadLine();
            //////////byte[] deskey = GetOpenSSL3deskey(salt, despswd, 1, 2);    // count=1 (for OpenSSL implementation); 2 iterations to get at least 24 bytes
            //////////if (deskey == null)
            //////////    return null;
            ////////////showBytes("3DES key", deskey) ;

            ////////////------ Decrypt the encrypted 3des-encrypted RSA private key ------
            //////////byte[] rsakey = DecryptKey(binkey, deskey, salt);   //OpenSSL uses salt value in PEM header also as 3DES IV
            //////////if (rsakey != null)
            //////////    return rsakey;  //we have a decrypted RSA private key
            //////////else
            //////////{
            //////////    Console.WriteLine("Failed to decrypt RSA private key; probably wrong password.");
            //////////    return null;
            //////////}
        }

        public static RSACryptoServiceProvider DecodeRSAPrivateKey(byte[] privkey)
        {
            byte[] MODULUS, E, D, P, Q, DP, DQ, IQ;

            // ---------  Set up stream to decode the asn.1 encoded RSA private key  ------
            using (var mem = new MemoryStream(privkey))
            using (var binr = new BinaryReader(mem)) //wrap Memory Stream with BinaryReader for easy reading
            {
                byte bt = 0;
                ushort twobytes = 0;
                var elems = 0;
                try
                {
                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                        binr.ReadByte(); //advance 1 byte
                    else if (twobytes == 0x8230)
                        binr.ReadInt16(); //advance 2 bytes
                    else
                        return null;

                    twobytes = binr.ReadUInt16();
                    if (twobytes != 0x0102) //version number
                        return null;
                    bt = binr.ReadByte();
                    if (bt != 0x00)
                        return null;


                    //------  all private key components are Integer sequences ----
                    elems = GetIntegerSize(binr);
                    MODULUS = binr.ReadBytes(elems);

                    elems = GetIntegerSize(binr);
                    E = binr.ReadBytes(elems);

                    elems = GetIntegerSize(binr);
                    D = binr.ReadBytes(elems);

                    elems = GetIntegerSize(binr);
                    P = binr.ReadBytes(elems);

                    elems = GetIntegerSize(binr);
                    Q = binr.ReadBytes(elems);

                    elems = GetIntegerSize(binr);
                    DP = binr.ReadBytes(elems);

                    elems = GetIntegerSize(binr);
                    DQ = binr.ReadBytes(elems);

                    elems = GetIntegerSize(binr);
                    IQ = binr.ReadBytes(elems);

                    //Console.WriteLine("showing components ..");
                    //if (verbose)
                    //{
                    //    showBytes("\nModulus", MODULUS);
                    //    showBytes("\nExponent", E);
                    //    showBytes("\nD", D);
                    //    showBytes("\nP", P);
                    //    showBytes("\nQ", Q);
                    //    showBytes("\nDP", DP);
                    //    showBytes("\nDQ", DQ);
                    //    showBytes("\nIQ", IQ);
                    //}

                    // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                    var RSA = new RSACryptoServiceProvider();
                    var RSAparams = new RSAParameters();
                    RSAparams.Modulus = MODULUS;
                    RSAparams.Exponent = E;
                    RSAparams.D = D;
                    RSAparams.P = P;
                    RSAparams.Q = Q;
                    RSAparams.DP = DP;
                    RSAparams.DQ = DQ;
                    RSAparams.InverseQ = IQ;
                    RSA.ImportParameters(RSAparams);
                    return RSA;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        private static int GetIntegerSize(BinaryReader binr)
        {
            byte bt = 0;
            byte lowbyte = 0x00;
            byte highbyte = 0x00;
            var count = 0;
            bt = binr.ReadByte();
            if (bt != 0x02) //expect integer
                return 0;
            bt = binr.ReadByte();

            if (bt == 0x81)
                count = binr.ReadByte(); // data size in next byte
            else if (bt == 0x82)
            {
                highbyte = binr.ReadByte(); // data size in next 2 bytes
                lowbyte = binr.ReadByte();
                byte[] modint = {lowbyte, highbyte, 0x00, 0x00};
                count = BitConverter.ToInt32(modint, 0);
            }
            else
            {
                count = bt; // we already have the data size
            }

            while (binr.ReadByte() == 0x00)
                count -= 1;
            binr.BaseStream.Seek(-1, SeekOrigin.Current); //last ReadByte wasn't a removed zero, so back up a byte
            return count;
        }
    }
}