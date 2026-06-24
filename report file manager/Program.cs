using System;
using System.Linq;
using System.IO;
namespace System.IO;

class Report()
{
    enum ReportType {Collect, Analyze, Recon, Intel}
    enum ReportStatus {Pending, Approved, Rejected}
    static string[]? openFile(string nameOfFile) {
        string[] allRows = [];
        if (!File.Exists(nameOfFile))
        {
            Console.WriteLine($"Error: file {nameOfFile} not found");
            return null;
        }
        allRows=File.ReadAllLines(nameOfFile);
        if (allRows.Length == 0)
        {
            Console.WriteLine("Error File is empty");
            return null;
        }
        return allRows;
            }
    static bool validateType(string type, out ReportType convertedType) {
        bool isValidType = Enum.TryParse<ReportType>(type, true, out convertedType);
        if(isValidType)
            Console.WriteLine($"Type{convertedType}");
        else
            Console.WriteLine("Invalid record: Unknown report type");
        return isValidType;
    }
    static bool validateStatus(string status, out ReportStatus convertedStatus) {
        bool isValidStatus = Enum.TryParse<ReportStatus>(status, true, out convertedStatus);
        if (isValidStatus)
            Console.WriteLine($"Type{convertedStatus}");
        else
            Console.WriteLine("Invalid record: Unknown report type"); return isValidStatus;
    }
    static bool validatePriority(string priority, out int validPriority)//int 1 to 5
    {
        if(int.TryParse(priority, out validPriority))
        {
            if (validPriority <= 5 && validPriority >= 1)
                return true;
            else
            {
                Console.WriteLine("Invalid record: Priority is out of range, priority should be in range 1 to 5");
                return false;
            }
        }
        Console.WriteLine("Invalid record: Priority is not valid number, priority should be an integer");
        return false;
    }
    static bool validateScore(string score, out double validScore)//double 0.0 to 100.0
    { 
        if(double.TryParse(score, out validScore))
        {
            if (validScore >= 0.0 && validScore <= 100.0)
                return true;
            else
            {
                Console.WriteLine("Invalid record: Score is out of range, score should be in range of 0.0 to 100.0");
                return false;
            }
        }
        Console.WriteLine("Invalid record: Score is not valid number, score should be a double");
        return false;
    }

    static bool checkLineValidity(string type,string priority,string score,string status)
    {
        bool isValidType;
        bool allValid;
        bool isValidStatus;
        bool isValidPriority;
        bool isValidScore;   
        isValidType = validateType(type, out ReportType convertedStatus);
        isValidStatus = validateStatus(status, out ReportStatus convertedType);
        isValidPriority = validatePriority(priority, out int validPriority);
        isValidScore = validateScore(score, out double validScore);
        if (!isValidType) {

            Console.WriteLine($"Invalid record: the type is not valid");
        }
        if (!isValidStatus) {
            Console.WriteLine($"Invalid record: the status is not valid");
        }
        if (!isValidPriority) {
            Console.WriteLine($"Invalid record: the priority is not valid");
        }
        if (!isValidScore) {
            Console.WriteLine($"Invalid record: the score is not valid");
        }
        allValid=isValidType&& isValidStatus&& isValidPriority&& isValidScore;
        return allValid;
    }
    static string[] analyzeLine(string[] rows) {
        string trimLine="";
        bool isValidLine;
        string[] splitReportArr;
        for (int i = 0; i < rows.Length; i++)
            {
            trimLine = rows[i].Trim();
            if (string.IsNullOrEmpty(trimLine))
                continue;
            splitReportArr = trimLine.Split(',', StringSplitOptions.RemoveEmptyEntries);
            if (splitReportArr.Length != 5)
                continue;
            isValidLine = checkLineValidity(splitReportArr[1], splitReportArr[2], splitReportArr[3], splitReportArr[4]);
            if(isValidLine)
                Console.WriteLine("Valid record processed");
            }
        return [];
        }
    static void Main()
    {
        string nameOfFile = "reports.txt";
        string[]? allRows;
        string[] rows;
        const int MAX_REPORTS = 100;
        rows = new string[MAX_REPORTS];
        allRows = openFile(nameOfFile);
        if(allRows is null)
        {
            Environment.Exit(1);
        }
        analyzeLine(allRows);
        Console.WriteLine($"File loaded: {allRows.Length} lines found");
    }
}