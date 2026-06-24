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

    static void insertDataToArr(string [][] allValidRows,string[] unitNames,ReportType[] reportType,ReportStatus[] reportStatus,int[] priorities,double[] score) {
        int indexCounter = 0;
        for(int i = 0; i < allValidRows.Length; i++)
        {
            if (allValidRows[i] == null)
            {
                continue;
            }
                unitNames[indexCounter] = allValidRows[i][0];
                reportType[indexCounter] = Enum.Parse<ReportType>(allValidRows[i][1],true);
                priorities[indexCounter] = int.Parse(allValidRows[i][2]);
                score[indexCounter] = double.Parse(allValidRows[i][3]);
                reportStatus[indexCounter] = Enum.Parse<ReportStatus>(allValidRows[i][4],true);
                indexCounter += 1;
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
    static double FindMaxScore(double[] score,int validCount) {
        double maxScore = -1.0;
        for(int i = 0; i < validCount; i++)
        {
            if (score[i] > maxScore)
            {
                maxScore = score[i];
            }
        }
        return maxScore;
    }
    static double FindMinScore(double[] score,int validCount) {
        double minScore = 101.0;
        for (int i = 0; i < validCount; i++)
        {
            if (score[i] < minScore)
            {
                minScore = score[i];
            }
        }
        return minScore;
    }
    
    static int CountByStatus(ReportStatus[] reportStatus,int validCount,ReportStatus status) {
        int counter = 0;
        for(int s=0; s<validCount; s++) {
            if (status==reportStatus[s]) {
                counter += 1;
            }
        }
        return counter;
    }
    static int CountByType(ReportType[] reportType,int validCount, ReportType type) {
        int counter = 0;
        for (int s = 0; s < validCount; s++)
        {
            if (type== reportType[s])
            {
                counter += 1;
            }
        }
        return counter;
    }
    
    static void DisplayBasicStatistic(double[] score, int validCount) {
        double avgScore,minScore,maxScore;
        avgScore = CalculateAverage(score, validCount);
        maxScore = FindMaxScore(score, validCount);
        minScore = FindMinScore(score, validCount);
        Console.WriteLine($"===Report Statistics===\n" +
            $"Total Reports:{validCount}\n" +
            $"Averge Score:{avgScore}\n" +
            $"Highest Score:{maxScore}\n" +
            $"Lowest Score:{minScore}\n");

    }
    static void displayStatusCount(ReportStatus[] reportStatus,int validCount) {
        int pending = CountByStatus(reportStatus,validCount, ReportStatus.Pending);
        int approved = CountByStatus(reportStatus, validCount, ReportStatus.Approved);
        int Rejected = CountByStatus(reportStatus, validCount, ReportStatus.Rejected);
        Console.WriteLine($"===Reports by status===\n" +
            $"Pending:{pending}\n" +
            $"Approved:{approved}\n" +
            $"Rejected:{Rejected}\n");
    }
    static void DisplyTypeCounts(ReportType[] reportType, int validCount) {
        int collect = CountByType(reportType, validCount, ReportType.Collect);
        int analyze = CountByType(reportType, validCount, ReportType.Analyze);
        int recon = CountByType(reportType, validCount, ReportType.Recon);
        Console.WriteLine($"===Reports by Type===\n" +
            $"Collect:{collect}\n" +
            $"Analyze:{analyze}\n" +
            $"Recon:{recon}\n");
    }
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
        ReportType[] reportType = new ReportType[MAX_REPORTS];
        ReportStatus[] reportStatus = new ReportStatus[MAX_REPORTS];
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
            FindMaxScore(score,validCount);
            FindMinScore(score,validCount);
            //CountByStatus(reportStatus,);
            //CountByType(reportType,);
            DisplayBasicStatistic(score,validCount);
            displayStatusCount(reportStatus, validCount);
            DisplyTypeCounts(reportType, validCount);
            DisplayHighestPriority();
            DisplayAveragesByPriority();
        }
            else {
            Console.WriteLine("Error there is no any valid rows in file");
        }
        
    }
}