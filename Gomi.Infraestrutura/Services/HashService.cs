using Gomi.Infraestrutura.Enums;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Gomi.Infraestrutura.Services
{
    public class HashService
    {
        public const string ChaveGeral = "4b55df75e2e804bab559aa885be40310";

        public const string ChavePrivadaInformacao = @"M+PQyJU31MQk4kgWAPR94XzADA4
wEznWhXV1khL+W7roetOcTeFXwdKTMQ018XINoahwWqKwg2OZohanZT6Vm74xez70QnLZoPSFdyvLIjaN8
1fh5msI+jORkogFw/OfkTGVzSZOyFchvSPUBFOTxcqU1wzzLhPpE3yj2lK3I9kDDg/50gAxIl4WdPeti/w
2F0H0+RyBrvon4GQOyIlYjUA/qnr/ogm6QliyDde76l03VNam6BN3LTFGZ4+oic73eiAsMkR6WdHhAh5wJ
7di+stkz+kiZYOjiktIIKZ+g/jJtQTJHhEjOEGrlWgk0lXR75HngW9YYK5asXDjhBzzVHsntB44nwC4dk/
mKxDMie2/3UUMjK6651jsggs1BIZ7vQyCLhEdFZHCcMxT72Ir3w1V4FID+XQclMO6V4t9q7Hs7gHNIHESE
qvTIs+otk3YcHwSLp7cMmoGNeNvFYT/3pbVOVUqfvxlJZQMj0Ji5XKWeg2BDDg+h9RxdIJw28fopXdWt3g
Y3kWt3HJ9MyoXs2T9yojC2j0lq0WG8PTjKHapz17eYe808sv9JTPa0PBPYB+/IHmr/nD/ovvO5UUw7uRUf
W0tG1BfaQyFi+xMsyCgT1URPEmbI8AZ1iel2+YW3Fnj5bY9KoUiJcFrlnDb8VkLB09Yf+0NsJVC+0sopFz
O0H4YerEf00IfcF4X/tVwlbhNESk0Q/F/y9vdzrupghFDYXfa4RjUEjCmrAztXeUDkgRXTJSjGGuoMtnDj
aWqCTm78/9hC7/w/oV2ZxmB1U906Q5pC8on0Y4Ob75ctNXF4LncbcxXGu93AvxVutOIAQZ6/EmXb2NMNm2
T2B/W0gjcFcAyuGTQfXrEEJIuJttePLAzK2Zqf7iYPlGJWlVeMdwy11o82UOUOIyG3HROx647J0a/4PmSZ
HDKW3WbdaUp9i7unzZe3GeYUCj7E+k9YhX8mheknIj0CioasfzHN+F9J0UnvBJXluRYRZiTUcqoA6wOeub
b/Jyqidoog736PepzvHb+RXPCKvj/97DIjeMCu+IqxFFw0h264Uv3g7BYAgCNgivBy8pa4r0EYGuqdkBbs
ecazX85Rvyt5L+t66xGsqfp2zf6OcNgjpDyaD9filZgkAviDL79+pRMfJhzbZIFxn5/naJ6kFs6X4YV9UY
ht6KUpKHE";

        public static string Calcular(string informacao, byte[] saltoBytes = null, AlgoritimoHash algoritmoHash = AlgoritimoHash.SHA256)
        {
            if (saltoBytes == null)
            {
                const int tamanhoMinimoDoSalto = 8;
                const int tamanhoMaximoDoSalto = 12;
                var aleatorio = new Random();
                var tamanhoDoSalto = aleatorio.Next(tamanhoMinimoDoSalto, tamanhoMaximoDoSalto);
                saltoBytes = new byte[tamanhoDoSalto];
                var rng = new RNGCryptoServiceProvider();
                rng.GetNonZeroBytes(saltoBytes);
            }
            string valorHash;
            var informacaoBytes = Encoding.ASCII.GetBytes(informacao);
            var informacaoComSaltoBytes = new byte[informacaoBytes.Length + saltoBytes.Length];
            for (var i = 0; i < informacaoBytes.Length; i++)
                informacaoComSaltoBytes[i] = informacaoBytes[i];
            for (var i = 0; i < saltoBytes.Length; i++)
                informacaoComSaltoBytes[informacaoBytes.Length + i] = saltoBytes[i];
            HashAlgorithm hash;
            switch (algoritmoHash)
            {
                case AlgoritimoHash.SHA1:
                    hash = new SHA1Managed();
                    break;
                case AlgoritimoHash.SHA384:
                    hash = new SHA384Managed();
                    break;
                case AlgoritimoHash.SHA512:
                    hash = new SHA512Managed();
                    break;
                default:
                    hash = new SHA256Managed();
                    break;
            }
            try
            {
                var hashBytes = hash.ComputeHash(informacaoComSaltoBytes);
                var hashComSaltoBytes = new byte[hashBytes.Length + saltoBytes.Length];
                for (var i = 0; i < hashBytes.Length; i++)
                    hashComSaltoBytes[i] = hashBytes[i];
                for (var i = 0; i < saltoBytes.Length; i++)
                    hashComSaltoBytes[hashBytes.Length + i] = saltoBytes[i];
                valorHash = Convert.ToBase64String(hashComSaltoBytes);
            }
            finally
            {
                hash.Clear();
            }
            return valorHash;
        }

        public static bool Testar(string informacao, string valorHash, AlgoritimoHash algoritmoHash = AlgoritimoHash.SHA256)
        {
            var hashComSaltoBytes = Convert.FromBase64String(valorHash);
            int tamanhoHashEmBits;
            switch (algoritmoHash)
            {
                case AlgoritimoHash.SHA1:
                    tamanhoHashEmBits = 160;
                    break;
                case AlgoritimoHash.SHA384:
                    tamanhoHashEmBits = 384;
                    break;
                case AlgoritimoHash.SHA512:
                    tamanhoHashEmBits = 512;
                    break;
                default:
                    tamanhoHashEmBits = 256;
                    break;
            }
            var tamanhoHashEmBytes = tamanhoHashEmBits / 8;
            if (hashComSaltoBytes.Length < tamanhoHashEmBytes)
                return false;
            var saltoBytes = new byte[hashComSaltoBytes.Length - tamanhoHashEmBytes];
            for (var i = 0; i < saltoBytes.Length; i++)
                saltoBytes[i] = hashComSaltoBytes[tamanhoHashEmBytes + i];
            var resultadoHashTexto = Calcular(informacao, saltoBytes, algoritmoHash);
            return (valorHash == resultadoHashTexto);
        }

        public static object CalcularMd5Informacao(string informacao, bool formatoTexto = true)
        {
            if (formatoTexto)
            {
                var hash = CalcularMd5(Encoding.ASCII.GetBytes(informacao));
                var sb = new StringBuilder();
                for (var i = 0; i < hash.Length; i++)
                    sb.Append(hash[i].ToString("x2"));
                return sb.ToString();
            }
            return CalcularMd5(Encoding.ASCII.GetBytes(informacao));
        }

        private static byte[] CalcularMd5(object objeto)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                return objeto.GetType() == typeof(FileStream) ? md5.ComputeHash((FileStream)objeto) : md5.ComputeHash((byte[])objeto);
            }
        }

        public static byte[] CalcularMd5Arquivo(string nomeArquivo)
        {
            using (var stream = new FileStream(nomeArquivo, FileMode.Open, FileAccess.Read))
            {
                return CalcularMd5(stream);
            }
        }

        public static bool TestarMd5Informacao(string informacao, string valorHash)
        {
            return CalcularMd5Informacao(informacao).ToString() == valorHash.Replace("-", "");
        }

        public static bool TestarMd5Arquivo(string nomeArquivo, string valorHash)
        {
            return BitConverter.ToString(CalcularMd5Arquivo(nomeArquivo)).Replace("-", "") == valorHash.Replace("-", "");
        }

        public static string CalcularECifrarMd5Informacao(string informacao, bool formatoTexto = true)
        {
            var md5 = CalcularMd5Informacao(informacao, formatoTexto);
            if (md5 is string) return TripleDes.Cifrar(md5.ToString(), RetornarChavePrivadaInformacaoDecifrada());
            return TripleDes.Cifrar(BitConverter.ToString((byte[])md5), RetornarChavePrivadaInformacaoDecifrada());
        }

        public static bool DecifrarETestarMd5Informacao(string informacao, string valorHash)
        {
            return CalcularMd5Informacao(informacao).ToString() == TripleDes.Decifrar(valorHash, RetornarChavePrivadaInformacaoDecifrada()).Replace("-", "");
        }

        public static string RetornarChavePrivadaInformacaoDecifrada()
        {
            return TripleDes.Decifrar(ChavePrivadaInformacao, ChaveGeral).Replace(Environment.NewLine, string.Empty);
        }
    }

    public class TripleDes
    {
        /// <summary>
        /// Cifra a informação com base no modo e padding informados
        /// </summary>
        /// <param name="chave"></param>
        /// <param name="informacao"></param>
        /// <param name="mode"></param>
        /// <param name="paddingMode"></param>
        /// <returns>Informação cifrada</returns>
        public static string Cifrar(string informacao, string chave, CipherMode mode = CipherMode.ECB, PaddingMode paddingMode = PaddingMode.PKCS7)
        {
            byte[] results;
            var utf8 = new UTF8Encoding();
            var hashProvider = new MD5CryptoServiceProvider();
            var tdesKey = hashProvider.ComputeHash(utf8.GetBytes(chave));
            var tdesAlgorithm = new TripleDESCryptoServiceProvider
            {
                Key = tdesKey,
                Mode = mode,
                Padding = paddingMode
            };
            var dataToEncrypt = utf8.GetBytes(informacao);
            try
            {
                var encryptor = tdesAlgorithm.CreateEncryptor();
                results = encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
            }
            finally
            {
                tdesAlgorithm.Clear();
                hashProvider.Clear();
            }
            return Convert.ToBase64String(results);
        }

        /// <summary>
        /// Decifra a informação com base no modo e padding
        /// </summary>
        /// <param name="informacao"></param>
        /// <param name="chave"></param>
        /// <param name="mode"></param>
        /// <param name="paddingMode"></param>
        /// <returns>Informação decifrada</returns>
        public static string Decifrar(string informacao, string chave, CipherMode mode = CipherMode.ECB, PaddingMode paddingMode = PaddingMode.PKCS7)
        {
            byte[] results;
            var utf8 = new UTF8Encoding();
            var hashProvider = new MD5CryptoServiceProvider();
            var tdesKey = hashProvider.ComputeHash(utf8.GetBytes(chave));
            var tdesAlgorithm = new TripleDESCryptoServiceProvider
            {
                Key = tdesKey,
                Mode = mode,
                Padding = paddingMode
            };
            var dataToDecrypt = Convert.FromBase64String(informacao);
            try
            {
                var decryptor = tdesAlgorithm.CreateDecryptor();
                results = decryptor.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
            }
            finally
            {
                tdesAlgorithm.Clear();
                hashProvider.Clear();
            }
            return utf8.GetString(results);
        }
    }
}
