using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogPreprocessor
{
    class Program
    {
        static void Main(string[] args)
        {
           /* Console.WriteLine(" \"hello\"".Trim('"'));
            Console.ReadLine();
            return;
           */ var crawler = new LogCrawler();
            var logStatements = crawler.CrawlLogStatements(@"E:\MSSE Program\MSSE 2nd Semester\Software Analytics\project\Software-Analytics\IITDU.SIUSC\IITDU.SIUSC\CVAnalyzer");
            
            File.WriteAllLines(@"E:\MSSE Program\MSSE 2nd Semester\Software Analytics\project\Software-Analytics\logdata\new_log_statements.txt",logStatements);
           /* FileUtility fileUtility = new FileUtility();
            fileUtility.ConvertToJsonFile(@"E:\MSSE Program\MSSE 2nd Semester\Software Analytics\project\Software-Analytics\logdata\transaction.txt", @"E:\MSSE Program\MSSE 2nd Semester\Software Analytics\project\Software-Analytics\logdata\transaction_1.json");
           */ Console.ReadLine();
        }
    }
}
