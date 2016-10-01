using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            String NewIp = "";
            // 根据读入文件生成随机IP
            Console.WriteLine("根据指定文件生成随机IP");
            for (int i = 0; i < 10; i++)
            {
                NewIp = GenRandIpInFile("ipList.csv");
                Console.WriteLine(NewIp);
            }

            // 指定起始范围生成随机IP
            Console.WriteLine("指定起始范围生成随机IP");
            for (int i = 0; i < 10; i++)
            {
                NewIp = GenRandIp("10.127.0.1", "10.250.10.5");
                Console.WriteLine(NewIp);
            }
        }

        // 将IPv4字符串转换为数字格式
        static long IpStringToNum(string Ip)
        {
            // 对IP 用 . 进行分割
            String[] IpArr = Ip.Split('.');
            // 转换为16进制8位的
            string IpHexS = "";
            // 循环每个数字
            foreach (String IpNum in IpArr)
            {
                // 转换为十六进制并追加
                IpHexS += int.Parse(IpNum).ToString("x2");
            }
            // 将十六进制IP转换为长整数数字 并 返回
            return Convert.ToInt64(IpHexS, 16);
        }

        // 将长整数IP转换为IPv4字符串
        static String IpNumToString(long IpNum)
        {
            // 将数字转换为16进制
            String IpHexS = IpNum.ToString("x8");
            // 每两位分割 为 四个数字
            MatchCollection IpArr = new Regex("([A-Za-z0-9]{2})").Matches(IpHexS);
            // 十六进制转十进制拼接为字符串
            string[] IpStringArr = new string[4];
            int i = 0;  // 数组位数
            foreach (Match IpAddr in IpArr)
            {
                // 获取每节十六进制并转为10进制
                IpStringArr[i++] = Convert.ToInt32(IpAddr.Value, 16).ToString();
            }
            // 使用 . 拼接数组并返回
            return String.Join(".", IpStringArr);
        }

        // 可靠的随机数种子生成器
        static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        // 随机IP生成根据文件
        static String GenRandIpInFile(String FileName)
        {
            // 读取文件的对象
            StreamReader sr = new StreamReader(FileName, Encoding.Default);
            // 保存读取文本的数组
            List<String> FileList = new List<String>();
            String line = "";   // 保存读取内容的临时变量
            // 循环读取文本
            while ((line = sr.ReadLine()) != null)
            {
                // 读取的结果追加到数组
                FileList.Add(line);
            }
            // 随机取一个数组成员ID
            int LineId = new Random(GetRandomSeed()).Next(0, FileList.Count);
            // 取出成员并拆解为数组
            String[] IpInfo = FileList[LineId].Split(',');
            // 根据范围随机生成一个IP
            String NewIpR = GenRandIp(IpInfo[0], IpInfo[1]);
            return NewIpR;
        }

        // 根据范围随机产生IP
        static string GenRandIp(String IpStart, String IpEnd)
        {
            // 将IP转换为数字
            long IpStarlNum = IpStringToNum(IpStart);
            long IpEndNum   = IpStringToNum(IpEnd);
            // 范围不正确 直接返回开始值
            if (IpStarlNum >= IpEndNum) {
                return IpStart;
            }
            // 计算IP差值
            long IpDiff = IpEndNum - IpStarlNum;
            // 产生差值范围内的随机数
            long RandKey = (long)Math.Floor((new Random(GetRandomSeed())).NextDouble() * IpDiff);
            // IP起始值加差值得到新IP值
            long NewIpNum = IpStarlNum + RandKey;
            // 将IP数字转为IPv4 并 返回
            return IpNumToString(NewIpNum);
        }

    }
}
