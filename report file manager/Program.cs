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
            Console.WriteLine($"Type {convertedType}");
        else
            Console.WriteLine("Invalid record: Unknown report type");
        return isValidType;
    }
    static bool validateStatus(string status, out ReportStatus convertedStatus) {
        bool isValidStatus = Enum.TryParse<ReportStatus>(status, true, out convertedStatus);
        if (isValidStatus)
            Console.WriteLine($"Type {convertedStatus}");
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

    static bool checkLineValidity(string unit, string type, string priority, string score, string status)
    {
        bool isValidType;
        bool allValid;
        bool isValidStatus;
        bool isValidPriority;
        bool isValidScore;
        isValidType = validateType(type, out ReportType convertedType);
        isValidStatus = validateStatus(status, out ReportStatus convertedStatus);
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
        allValid = isValidType && isValidStatus && isValidPriority && isValidScore;
        if (allValid) {Console.WriteLine($"Unit:{unit}\nType:{convertedType}\nPriority:{validPriority}\nScore:{validScore}\nStatus:{convertedStatus}"); }
        return allValid;
    }

    static void insertDataToArr(string [][] allValidRows,string[] unitNames,string[] reportType,string[] reportStatus,int[] priorities,double[] score) {
        for(int i = 0; i < allValidRows.Length; i++)
        {
            if (allValidRows[i] == null)
            {
                continue;
            }
                unitNames[i] = allValidRows[i][0];
                reportType[i] = allValidRows[i][1];
                priorities[i] = int.Parse(allValidRows[i][2]);
                score[i] = double.Parse(allValidRows[i][3]);
                reportStatus[i] = allValidRows[i][4];
        }

    }
    static string[][] analyzeLine(string[] rows) {
        string trimLine="";
        bool isValidLine;
        string[] splitReportArr;
        string[][] allValidRows = new string[rows.Length][];
        if (rows.Length == 0)
        {
            Console.WriteLine();
        }

        for (int i = 0; i < rows.Length; i++)
            {
            trimLine = rows[i].Trim();
            if (string.IsNullOrEmpty(trimLine))
                continue;
            splitReportArr = trimLine.Split(',', StringSplitOptions.RemoveEmptyEntries);
            if (splitReportArr.Length != 5)
                continue;
            isValidLine = checkLineValidity(splitReportArr[0], splitReportArr[1], splitReportArr[2], splitReportArr[3], splitReportArr[4]);
            if (isValidLine)
            {
                Console.WriteLine("Valid record processed");
                string[] validRow = new string[5];
                validRow[0] = splitReportArr[0];
                validRow[1] = splitReportArr[1];
                validRow[2] = splitReportArr[2];
                validRow[3] = splitReportArr[3];
                validRow[4] = splitReportArr[4];
                allValidRows[i] = validRow;
            }
         }
        return allValidRows;
    }
    static double CalculateAverage(double[] score,int validCount) {
        double sum = 0.0;
        for(int i = 0; i < validCount; i++)
            {
            sum += score[i];
            }
        return sum /validCount;
    }
    static double FindMaxScore(double[] score) {
        double maxScore = -1.0;
        for(int i = 0; i < score.Length; i++)
        {
            if (score[i] > maxScore)
            {
                maxScore = score[i];
            }
        }
        return maxScore;
    }
    static double FindMinScore() {
        double minScore = 101.0;
        for (int i = 0; i < score.Length; i++)
        {
            if (score[i] > maxScore)
            {
                maxScore = score[i];
            }
        }
        return maxScore;
    }
    }
    static int CountByStatus() { }
    static int CountByType() { }
    static void DisplayBasicStatistic() { }
    static void displayStatusCount() { }
    static void DisplyTypeCounts() { }
    static void DisplayHighestPriority() { }
    static void DisplayAveragesByPriority() { }

    static void Main()
    {
        string nameOfFile = "reports.txt";
        string[]? allRows;
        string[] rows;
        string[][] allValidRows;
        const int MAX_REPORTS = 100;
        int validCount = 0;
        string[] unitNames = new string[MAX_REPORTS];
        string[] reportType = new string[MAX_REPORTS];
        string[] reportStatus = new string[MAX_REPORTS];
        int[] priorities = new int[MAX_REPORTS];
        double[] score = new double[MAX_REPORTS];

        rows = new string[MAX_REPORTS];
        allRows = openFile(nameOfFile);
        if(allRows is null)
        {
            Environment.Exit(1);
        }
        allValidRows=analyzeLine(allRows);
        for(int i = 0; i < allValidRows.Length; i++)
        {
            if (allValidRows[i] != null)
                validCount += 1;
        }
        if (allValidRows.Length > 0) { 
            Console.WriteLine($"Proccessing complete.\nFile loaded: {allRows.Length} lines found\nValid records:{validCount}\ninvalid records:{allRows.Length - validCount}");
            insertDataToArr(allValidRows, unitNames, reportType, reportStatus, priorities, score);
            Console.WriteLine($"Stored {validCount} valid records for analysis");
            CalculateAverage(score,validCount);
            FindMaxScore(score);
            FindMinScore(score);
            CountByStatus();
            CountByType();
            DisplayBasicStatistic();
            displayStatusCount();
            DisplyTypeCounts();
            DisplayHighestPriority();
            DisplayAveragesByPriority();
        }
            else {
            Console.WriteLine("Error there is no any valid rows in file");
        }
        
    }
}