# GenRandIP
    根据指定IP文件列表或者IP范围
    生成随机IP


# Demo
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
